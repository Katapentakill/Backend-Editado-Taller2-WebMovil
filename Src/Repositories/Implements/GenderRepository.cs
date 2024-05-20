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
    public class GenderRepository : IGenderRepository
    {
        private readonly DataContext _context;
        public GenderRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Gender>> GetGenders()
        {
            var genders = await _context.Genders.ToListAsync();
            return genders;
        }

        public async Task<bool> ValidateGenderId(int id)
        {
            var existingGender = await _context.Genders.FindAsync(id);
            if(existingGender == null){
                return false;
            }
            return true;
        }
    }
}