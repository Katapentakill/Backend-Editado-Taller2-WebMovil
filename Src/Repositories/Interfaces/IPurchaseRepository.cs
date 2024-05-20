using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Repositories.Interfaces
{
    public interface IPurchaseRepository
    {
        Task<IEnumerable<Purchase>> GetPurchases();

        Task<IEnumerable<Purchase>> GetPurchasesByUser(int userId);

        Task<IEnumerable<Purchase>> SearchPurchases(string query);

        Task<Purchase> MakePurchase(Purchase purchase);        
    }
}