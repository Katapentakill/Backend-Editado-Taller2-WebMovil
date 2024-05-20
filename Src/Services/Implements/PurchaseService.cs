using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.DTO.Purchase;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Services.Implements
{
    public class PurchaseService : IPurchaseService
    {
       private readonly IPurchaseRepository _purchaseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProductRepository _productRepository;

        private readonly IMapperService _mapperService;

        public PurchaseService(IPurchaseRepository purchaseRepository, IUserRepository userRepository, 
                                IProductRepository productRepository, IMapperService mapperService)
        {
            _purchaseRepository = purchaseRepository;
            _userRepository = userRepository;
            _productRepository = productRepository;
            _mapperService = mapperService;
        }
        public async Task<IEnumerable<Purchase>> GetPurchases()
        {
            var purchases = await _purchaseRepository.GetPurchases();
            return purchases;
        }

        public async Task<IEnumerable<PurchaseInfoDto>> GetPurchasesByUser(int userId)
        {
            var result = await _userRepository.VerifyUser(userId);
            if(!result){
                throw new Exception("El Usuario no existe.");
            }
            var purchases = await _purchaseRepository.GetPurchasesByUser(userId);
            var mappedPurchases = _mapperService.MapPurchases(purchases);
            return mappedPurchases;
        }

        public async Task<IEnumerable<Purchase>> SearchPurchases(string query)
        {
            var purchases = await _purchaseRepository.SearchPurchases(query);
            return purchases;
        }

        public async Task<PurchaseInfoDto> MakePurchase(PurchaseDto purchaseDto)
        {
            var quantity = int.Parse(purchaseDto.Quantity);
            var result = await _userRepository.VerifyUser(int.Parse(purchaseDto.UserId));
            if(!result){
                throw new Exception("El Usuario no existe.");
            }

            int productId = int.Parse(purchaseDto.ProductId);
            var existingProduct = await _productRepository.GetProductById(productId) ?? throw new Exception("El Producto no existe.");
            
            DateTime purchaseDate = DateTime.Now;

            if(existingProduct.Stock < quantity){
                throw new Exception("No hay Stock suficiente.");
            }
            int totalPrice = quantity * existingProduct.Price;
            int userId = int.Parse(purchaseDto.UserId);
            
            var purchase = new Purchase {
                Purchase_Date = purchaseDate,
                ProductId = existingProduct.Id,
                ProductName = existingProduct.Name,
                ProductType = existingProduct.ProductType.Type,
                ProductPrice = existingProduct.Price,
                Quantity = quantity,
                TotalPrice = totalPrice,
                UserId = userId

            };

            var addResult = await _purchaseRepository.MakePurchase(purchase);

            var productDto = new EditProductInfoDto{
                Stock = existingProduct.Stock - quantity,
            };

            await _productRepository.EditProduct(existingProduct.Id, productDto);

            var mappedPurchase = _mapperService.PurchaseToPurchaseInfoDto(addResult);
            return mappedPurchase;            

        } 
    }
}