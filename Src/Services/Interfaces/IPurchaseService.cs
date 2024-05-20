using project_dotnet7_api.Src.DTO.Purchase;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Services.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetPurchases();

        Task<IEnumerable<PurchaseInfoDto>> GetPurchasesByUser(int userId);

        Task<IEnumerable<Purchase>> SearchPurchases(string query);

        Task<PurchaseInfoDto> MakePurchase(PurchaseDto purchaseDto);        
    }
}