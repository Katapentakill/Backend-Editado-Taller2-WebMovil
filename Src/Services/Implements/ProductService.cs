using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Repositories.Interfaces;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Services.Implements
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductTypeRepository _productTypeRepository;
        private readonly IPhotoService _photoService;

        private readonly IMapperService _mapperService;

        public ProductService(IProductRepository productRepository, IProductTypeRepository productTypeRepository,
                                IPhotoService photoService, IMapperService mapperService)
        {
            _productRepository = productRepository;
            _productTypeRepository = productTypeRepository;
            _photoService = photoService;
            _mapperService = mapperService;
        }
        
        public async Task<bool> AddProduct(AddProductDto addProductDto)
        {
            if(!_productTypeRepository.VerifyProductType(int.Parse(addProductDto.ProductTypeId)).Result)
            {
                throw new Exception("El Tipo de Producto no es valido.");
            }
            if(_productRepository.VerifyProductByNameAndType(addProductDto.Name, int.Parse(addProductDto.ProductTypeId)).Result){
                throw new Exception("Ya existe un Producto con el mismo nombre y tipo.");
            }
            var result = await _photoService.AddPhoto(addProductDto.Image);

            if (result.Error != null)
            {
                throw new Exception("Error en la subida de imagen, vuelva a intentarlo más tarde.");
            };

            var mappedProduct = _mapperService.AddProductDtoToProduct(addProductDto);
            mappedProduct.ImgUrl = result.SecureUrl.AbsoluteUri;
            mappedProduct.ImgId = result.PublicId;

            var addResult = await _productRepository.AddProduct(mappedProduct);

            return addResult;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var existingProduct = await _productRepository.GetProductById(id) ?? throw new Exception("El Producto no existe.");
            var result = await _photoService.DeletePhoto(existingProduct.ImgId);
            if (result.Error != null)
            {
                throw new Exception("Error en la subida de imagen, vuelva a intentarlo más tarde.");
            };

            var deleteResult = await _productRepository.DeleteProduct(id);
            return deleteResult;
        }

        public async Task<bool> EditProduct(int id, EditProductDto editProductDto)
        {
            if(editProductDto.ProductTypeId != null)
            {
                var result = await _productTypeRepository.VerifyProductType(int.Parse(editProductDto.ProductTypeId));
                if(!result)
                {
                    throw new Exception("El Tipo de Producto no es valido.");
                }
            }
            var existingProduct = await _productRepository.GetProductById(id) ?? throw new Exception("El Producto no existe.");
            if(editProductDto.Name != null && editProductDto.ProductTypeId == null)
            {
                var result = await _productRepository.VerifyProductByNameAndType(editProductDto.Name, existingProduct.ProductTypeId);
                if(result){
                    throw new Exception("Ya existe un Producto con el mismo nombre y tipo.");
                }
            }
            if(editProductDto.Name == null && editProductDto.ProductTypeId != null)
            {
                var result = await _productRepository.VerifyProductByNameAndType(existingProduct.Name, int.Parse(editProductDto.ProductTypeId));
                if(result){
                    throw new Exception("Ya existe un Producto con el mismo nombre y tipo.");
                }
            }
            if(editProductDto.Name != null && editProductDto.ProductTypeId != null)
            {
                var result = await _productRepository.VerifyProductByNameAndType(editProductDto.Name, int.Parse(editProductDto.ProductTypeId));
                if(result){
                    throw new Exception("Ya existe un Producto con el mismo nombre y tipo.");
                }
            }
            var mappedProduct = _mapperService.EditProductDtoToEditProductInfo(editProductDto);

            if(editProductDto.Image != null)
            {
                var result = await _photoService.AddPhoto(editProductDto.Image);

                if (result.Error != null)
                {
                    throw new Exception("Error en la subida de imagen, vuelva a intentarlo más tarde.");
                };

                mappedProduct.ImgUrl = result.SecureUrl.AbsoluteUri;
                mappedProduct.ImgId = result.PublicId;
                
                var deleteResult = await _photoService.DeletePhoto(existingProduct.ImgId);
                if (deleteResult.Error != null)
                {
                    throw new Exception("Error en la subida de imagen, vuelva a intentarlo más tarde.");
                };

            }

            
            var editResult = await _productRepository.EditProduct(id, mappedProduct);
            return editResult;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            var mappedProducts = _mapperService.MapProducts(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductDto>> GetAvailableProducts(int pageNumber, int pageSize)
        {
            var products = await _productRepository.GetAvailableProducts(pageNumber, pageSize);
            var mappedProducts = _mapperService.MapProducts(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductDto>> SearchProducts(string query)
        {
            var products =  await _productRepository.SearchProducts(query);
            var mappedProducts = _mapperService.MapProducts(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductDto>> SearchAvailableProducts(string query)
        {
            var products =  await _productRepository.SearchAvailableProducts(query);
            var mappedProducts = _mapperService.MapProducts(products);
            return mappedProducts;
        }

        public async Task<IEnumerable<ProductType>> GetProductTypes()
        {
            var productTypes = await _productTypeRepository.GetProductTypes();
            return productTypes;
        }
    }
}