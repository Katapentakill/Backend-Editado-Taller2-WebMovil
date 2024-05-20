using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int Price { get; set; } = 0;

        public int Stock { get; set; } = 0;

        public string ImgUrl { get; set; } = string.Empty;

        public string ImgId { get; set;} = string.Empty;



        // Relaciones
        public int ProductTypeId { get; set; }

        public ProductType ProductType { get; set; } = null!;
    }
}