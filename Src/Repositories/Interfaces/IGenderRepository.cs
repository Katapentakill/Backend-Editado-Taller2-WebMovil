using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Repositories.Interfaces
{
    public interface IGenderRepository
    {
        Task<IEnumerable<Gender>> GetGenders();

        Task<bool> ValidateGenderId(int id);        
    }
}