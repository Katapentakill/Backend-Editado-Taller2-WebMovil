using AutoMapper;
using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.DTO.Purchase;
using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Services.Implements
{
    public class MapperService
    {
        private readonly IMapper _mapper;

        public MapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users)
        {
            var mappedUsers = users.Select(u => _mapper.Map<UserDto>(u)).ToList();
            return mappedUsers;
        }

        public User RegisterClientDtoToUser(RegisterUserDto registerUserDto)
        {
            var mappedUser = _mapper.Map<User>(registerUserDto);
            return mappedUser;
        }

        public EditUserInfoDto EditUserDtoToEditUserInfo(EditUserDto editUserDto)
        {
            var mappedUser = _mapper.Map<EditUserInfoDto>(editUserDto);
            return mappedUser;
        }

        public UserDto UserToUserDto(User user)
        {
            var mappedUser = _mapper.Map<UserDto>(user);
            return mappedUser;
        }

        public IEnumerable<ProductDto> MapProducts(IEnumerable<Product> products)
        {
            var mappedProducts = products.Select(p => _mapper.Map<ProductDto>(p)).ToList();
            return mappedProducts;
        }

        public Product AddProductDtoToProduct(AddProductDto addProductDto)
        {
            var mappedProduct = _mapper.Map<Product>(addProductDto);
            return mappedProduct;
        }

        public EditProductInfoDto EditProductDtoToEditProductInfo(EditProductDto editProductDto)
        {
            var mappedInfo = _mapper.Map<EditProductInfoDto>(editProductDto);
            return mappedInfo;
        }

        public PurchaseInfoDto PurchaseToPurchaseInfoDto(Purchase purchase)
        {
            var mappedPurchase = _mapper.Map<PurchaseInfoDto>(purchase);
            return mappedPurchase;
        }

        public IEnumerable<PurchaseInfoDto> MapPurchases(IEnumerable<Purchase> purchases)
        {
            var mappedPurchases = purchases.Select(p => _mapper.Map<PurchaseInfoDto>(p)).ToList();
            return mappedPurchases;
        }
    }
}