using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.DTO.Purchase;
using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<RegisterUserDto, User>();
            CreateMap<EditUserDto, EditUserInfoDto>();
            CreateMap<Product, ProductDto>();
            CreateMap<AddProductDto, Product>();
            CreateMap<EditProductDto, EditProductInfoDto>();
            CreateMap<Purchase, PurchaseInfoDto>();
        }       
    }
}