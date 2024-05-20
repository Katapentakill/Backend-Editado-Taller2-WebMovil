using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<bool> ValidateRoleId(int id);

        Task<Role?> GetRoleById(int id);

        Task<Role?> GetRoleByName(string type);
    }
}