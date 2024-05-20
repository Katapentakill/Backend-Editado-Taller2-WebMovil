using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();

        Task<IEnumerable<ProductDto>> GetAvailableProducts(int pageNumber, int pageSize);

        Task<IEnumerable<ProductType>> GetProductTypes();
        
        Task<IEnumerable<ProductDto>> SearchProducts(string query);

        Task<IEnumerable<ProductDto>> SearchAvailableProducts(string query);

        Task<bool> AddProduct(AddProductDto addProductDto);

        Task<bool> EditProduct(int id, EditProductDto editProductDto);

        Task<bool> DeleteProduct(int id);        
    }
}