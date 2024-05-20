using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Services.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;

        private readonly IRoleRepository _roleRepository;

        private readonly IMapperService _mapperService;

        public UserService(IUserRepository userRepository, IGenderRepository genderRepository,
                            IMapperService mapperService, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _genderRepository = genderRepository;
            _mapperService = mapperService;
            _roleRepository = roleRepository;
        }
        public async Task<bool> ChangeUserPassword(int id, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserById(id) ?? throw new Exception("El usuario no existe.");

            var verifyOldPassword = BCrypt.Net.BCrypt.Verify(changePasswordDto.OldPassword, user.Password);
            if (!verifyOldPassword){
                throw new Exception("La contraseña antigua es incorrecta.");
            } 

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string newPasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword, salt);

            var result = await _userRepository.ChangePassword(id, newPasswordHash);
            return result;
        }

        public async Task<bool> EditUserInfo(int id, EditUserDto editUserDto)
        {
            if(editUserDto.GenderId != null && !_genderRepository.ValidateGenderId(int.Parse(editUserDto.GenderId)).Result){
                throw new Exception("El genero no es valido.");
            }

            var mappedInfo = _mapperService.EditUserDtoToEditUserInfo(editUserDto);


            var result = await _userRepository.EditUser(id, mappedInfo);
            return result;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var mappedUsers = _mapperService.MapUsers(users);
            return mappedUsers;
        }

        public async Task<IEnumerable<UserDto>> SearchUsers(string query)
        {
            var users = await _userRepository.SearchUsers(query);
            var mappedUsers = _mapperService.MapUsers(users);
            return mappedUsers;
        }

        public async Task<bool> ChangeUserState(int id, bool newUserState){
            var user = await _userRepository.GetUserById(id) ?? throw new Exception("El Usuario no existe");
            var role = await _roleRepository.GetRoleByName("Admin") ?? throw new Exception("Error del servidor.");
            if(user.RoleId == role.Id){
                throw new Exception("No se puede cambiar el estado de un Administrador.");
            }
            var result = await _userRepository.ChangeUserState(id, newUserState); 
            return result;
        }

        public async Task<IEnumerable<Gender>> GetGenders()
        {
            var genders = await _genderRepository.GetGenders();
            return genders;
        }
    }
}