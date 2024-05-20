using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.DTO.Purchase;
using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Services.Interfaces
{
    public interface IMapperService
    {
        public IEnumerable<UserDto> MapUsers(IEnumerable<User> users);

        public User RegisterClientDtoToUser(RegisterUserDto registerUserDto);

        public UserDto UserToUserDto(User user);

        public EditUserInfoDto EditUserDtoToEditUserInfo(EditUserDto editUserDto);

        public IEnumerable<ProductDto> MapProducts(IEnumerable<Product> products);

        public Product AddProductDtoToProduct(AddProductDto addProductDto);

        public EditProductInfoDto EditProductDtoToEditProductInfo(EditProductDto editProductDto);

        public PurchaseInfoDto PurchaseToPurchaseInfoDto(Purchase purchase);

        public IEnumerable<PurchaseInfoDto> MapPurchases(IEnumerable<Purchase> purchases);        
    }
}