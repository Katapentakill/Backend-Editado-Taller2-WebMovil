using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();

        Task<IEnumerable<User>> SearchUsers(string query);

        Task<User?> GetUserById(int id);

        Task<User?> GetUserByEmail(string email);

        Task<bool> VerifyRut(string rut);

        Task<bool> VerifyEmail(string email);

        Task<bool> VerifyUser(int id);

        Task<bool> AddUser(User user);

        Task<bool> EditUser(int id, EditUserInfoDto user);

        Task<bool> ChangeUserState(int id, bool newUserState);

        Task<bool> ChangePassword(int id, string newPassword);
    }
}