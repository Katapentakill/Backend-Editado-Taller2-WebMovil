using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Services.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _usersRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapperService _mapperService;

        public AuthService(IUserRepository usersRepository, IConfiguration configuration,
                            IMapperService mapperService, IGenderRepository genderRepository,
                            IRoleRepository roleRepository)
        {
            _usersRepository = usersRepository;
            _configuration = configuration;
            _mapperService = mapperService;
            _genderRepository = genderRepository;
            _roleRepository = roleRepository;
        }

        public async Task<LoggedUserDto> Login(LoginUserDto loginUserDto)
        {
            string mensaje = "Credenciales incorrectas, intentalo de nuevo.";

            var user = await _usersRepository.GetUserByEmail(loginUserDto.Email.ToString());
            if (user is null) throw new Exception(mensaje);
            if (!user.IsActive) throw new Exception("Su cuenta está inactiva, contacte a un administrador.");
            
            var result = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password);
            if (!result) throw new Exception(mensaje);

            var token = CreateToken(user);

            var mappedUserDto = _mapperService.UserToUserDto(user);

            var LoggedUserDto = new LoggedUserDto
            {
                User = mappedUserDto,
                Token = token
            };

            return LoggedUserDto;
        }

        public async Task<LoggedUserDto> RegisterUser(RegisterUserDto registerUserDto)
        {
            var mappedUser = _mapperService.RegisterClientDtoToUser(registerUserDto);
            if(!_genderRepository.ValidateGenderId(mappedUser.GenderId).Result){
                throw new Exception("El genero no es valido.");
            }
            if(_usersRepository.VerifyRut(mappedUser.Rut).Result){
                throw new Exception("El rut ingresado ya existe.");
            }
            if(_usersRepository.VerifyEmail(mappedUser.Email).Result){
                throw new Exception("El email ingresado ya existe.");
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password, salt);

            var role = _roleRepository.GetRoleByName("Usuario").Result ?? throw new Exception("Error del servidor, intentelo más tarde.");
            // Ensure fill fields not mapped
            mappedUser.Password = passwordHash;
            mappedUser.RoleId = role.Id; 
            mappedUser.IsActive = true;

            
            await _usersRepository.AddUser(mappedUser);
            var user = await _usersRepository.GetUserByEmail(mappedUser.Email) ?? throw new Exception("Error del servidor, intentelo más tarde.");
            var token = CreateToken(user);

            var mappedUserDto = _mapperService.UserToUserDto(user);

            var LoggedUserDto = new LoggedUserDto
            {
                User = mappedUserDto,
                Token = token
            };

            return LoggedUserDto;
        }

        private string CreateToken(User user)
        {
            var claims = new List<Claim>{
                new ("Id", user.Id.ToString()),
                new ("Email", user.Email),
                new (ClaimTypes.Role, user.Role.Type)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }        
    }
}