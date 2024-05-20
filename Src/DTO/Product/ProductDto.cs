using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Models;

namespace project_dotnet7_api.Src.DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Price { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public string ImgUrl { get; set; } = string.Empty;

        public ProductType ProductType { get; set; } = null!;        
    }
}