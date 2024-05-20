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
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly DataContext _context;

        public PurchaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetPurchases()
        {
            var purchases = await _context.Purchases.Include(p => p.User)
                                                    .ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetPurchasesByUser(int userId)
        {
            var purchases = await _context.Purchases.Where(p => p.UserId == userId).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> SearchPurchases(string query)
        {
            var purchases = await _context.Purchases.Where(p => p.Id.ToString().Contains(query)
                                                    || p.Purchase_Date.ToString().Contains(query)
                                                    || p.ProductName.Contains(query)
                                                    || p.ProductType.Contains(query)
                                                    || p.ProductPrice.ToString().Contains(query)
                                                    || p.Quantity.ToString().Contains(query)
                                                    || p.TotalPrice.ToString().Contains(query)
                                                    || p.User.Name.Contains(query)
                                                    )
                                                    .Include(p => p.User)
                                                    .ToListAsync();
            return purchases;
        }

        public async Task<Purchase> MakePurchase(Purchase purchase)
        {
            await _context.Purchases.AddAsync(purchase);
            await _context.SaveChangesAsync();
            return purchase;
        }        
    }
}