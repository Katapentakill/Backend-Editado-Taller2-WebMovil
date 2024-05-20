using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Helpers.Validators;

namespace project_dotnet7_api.Src.DTO.Product
{
    public class EditProductDto
    {
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(10, ErrorMessage = "El Nombre debe tener al menos 10 caracteres.")]
        [MaxLength(64, ErrorMessage = "El Nombre debe tener a lo más 64 caracteres.")]
        public string? Name { get; set; }

        [PriceValidation]
        public string? Price { get; set; } 

        [StockValidation]
        public string? Stock { get; set; }

        [ImageValidation]
        public IFormFile? Image { get; set; }

        [ProductTypeValidation]
        public string? ProductTypeId { get; set; }        
    }
}