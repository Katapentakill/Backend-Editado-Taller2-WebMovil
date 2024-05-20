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
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly DataContext _context;
        public ProductTypeRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            var productTypes = await _context.ProductTypes.ToListAsync();
            return productTypes;
        }

        public async Task<bool> VerifyProductType(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            return productType != null;
        }
    }        
    
}