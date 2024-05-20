using project_dotnet7_api.Src.DTO.User;

namespace project_dotnet7_api.Src.Services.Interfaces
{
    public interface IAuthService
    {
        Task<LoggedUserDto> RegisterUser(RegisterUserDto registerUserDto);

        Task<LoggedUserDto> Login(LoginUserDto loginUserDto);        
    }
}