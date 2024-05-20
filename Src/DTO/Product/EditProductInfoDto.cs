using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.DTO.Product
{
    public class EditProductInfoDto
    {
        public string? Name { get; set; }

        public int? Price { get; set; }

        public int? Stock { get; set; }

        public string? ImgUrl { get; set; }

        public string? ImgId { get; set; }

        public int? ProductTypeId { get; set; }        
    }
}