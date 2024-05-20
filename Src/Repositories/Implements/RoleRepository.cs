using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_dotnet7_api.Src.Data;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;

namespace project_dotnet7_api.Src.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;
        public RoleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> ValidateRoleId(int id){
            var role = await _context.Roles.FindAsync(id);
            if(role == null){
                return false;
            }
            return true;
        }

        public async Task<Role?> GetRoleById(int id){
            var role = await _context.Roles.FindAsync(id);
            return role;
        }

        public async Task<Role?> GetRoleByName(string type)
        {
            var role = await _context.Roles.Where(r => r.Type == type).FirstOrDefaultAsync();
            return role;
        }        
    }
}