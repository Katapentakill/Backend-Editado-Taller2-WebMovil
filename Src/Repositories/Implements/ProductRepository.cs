using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using project_dotnet7_api.Src.Data;
using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;

namespace project_dotnet7_api.Src.Repositories.Implements
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = await _context.Products.Include(p => p.ProductType)
                                                  .ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetAvailableProducts(int pageNumber, int pageSize)
        {
            var products = await _context.Products.Where(p => p.Stock > 0)
                                                  .Include(p => p.ProductType)
                                                  .Skip((pageNumber - 1)  * pageSize)
                                                  .Take(pageSize)
                                                  .ToListAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> SearchProducts(string query)
        {
            var products = await _context.Products.Where(p => p.Id.ToString().Contains(query)
                                                  || p.Name.Contains(query)
                                                  || p.Price.ToString().Contains(query)
                                                  || p.Stock.ToString().Contains(query)
                                                  || p.ProductType.Type.Contains(query)
                                                  ).Include(p => p.ProductType)
                                                  .ToListAsync();
            return products;
        }
        public async Task<IEnumerable<Product>> SearchAvailableProducts(string query)
        {
            var products = await _context.Products.Where(p => p.Stock > 0 && (p.Id.ToString().Contains(query)
                                                  || p.Name.Contains(query)
                                                  || p.Price.ToString().Contains(query)
                                                  || p.Stock.ToString().Contains(query)
                                                  || p.ProductType.Type.Contains(query))
                                                  ).Include(p => p.ProductType)
                                                  .ToListAsync();
            return products;
        }
        public async Task<Product?> GetProductById(int id)
        {
            var product = await _context.Products.Where(p => p.Id == id).Include(p => p.ProductType).FirstOrDefaultAsync();
            return product;
        }

        public async Task<bool> VerifyProductByNameAndType(string name, int product_TypeId)
        {
            var product = await _context.Products.Where(p => p.Name == name && p.ProductTypeId == product_TypeId).FirstOrDefaultAsync();
            return product != null;
        }

        public async Task<bool> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> EditProduct(int id, EditProductInfoDto product)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if(existingProduct == null){
                return false;
            }

            existingProduct.Name = product.Name ?? existingProduct.Name;
            existingProduct.Price = product.Price ?? existingProduct.Price;
            existingProduct.Stock = product.Stock ?? existingProduct.Stock;
            existingProduct.ImgUrl = product.ImgUrl ?? existingProduct.ImgUrl;
            existingProduct.ImgId = product.ImgId ?? existingProduct.ImgId;
            existingProduct.ProductTypeId = product.ProductTypeId ?? existingProduct.ProductTypeId;

            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existingProduct = await _context.Products.FindAsync(id);
            if(existingProduct == null){
                return false;
            }

            _context.Remove(existingProduct);
            await _context.SaveChangesAsync();

            return true;
        }        
    }
}