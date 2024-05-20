using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<IEnumerable<Product>> GetAvailableProducts(int pageNumber, int pageSize);

        Task<IEnumerable<Product>> SearchProducts(string query);

        Task<IEnumerable<Product>> SearchAvailableProducts(string query);

        Task<Product?> GetProductById(int id);

        Task<bool> VerifyProductByNameAndType(string name, int product_TypeId);

        Task<bool> AddProduct(Product product);

        Task<bool> EditProduct(int id, EditProductInfoDto product);

        Task<bool> DeleteProduct(int id);        
    }
}