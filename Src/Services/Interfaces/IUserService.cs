using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Services.Interfaces
{
    public interface IUserService
    {
        Task<bool> ChangeUserPassword(int id, ChangePasswordDto changePasswordDto);

        Task<bool> EditUserInfo(int id, EditUserDto editUserDto);

        Task<IEnumerable<UserDto>> GetUsers();

        Task<IEnumerable<Gender>> GetGenders();

        Task<IEnumerable<UserDto>> SearchUsers(string query);

        Task<bool> ChangeUserState(int id, bool newUserState);        
    }
}