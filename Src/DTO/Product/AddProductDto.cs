using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Helpers.Validators;

namespace project_dotnet7_api.Src.DTO.Product
{
    public class AddProductDto
    {
        [Required(ErrorMessage = "El Nombre es obligatorio.")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(10, ErrorMessage = "El Nombre debe tener al menos 10 caracteres.")]
        [MaxLength(64, ErrorMessage = "El Nombre debe tener a lo más 64 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Precio es obligatorio.")]
        [PriceValidation]
        public string Price { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Stock es obligatorio.")]
        [StockValidation]
        public string Stock { get; set; } = string.Empty;

        [Required(ErrorMessage = "La Imagen del Producto es obligatoria.")]
        [ImageValidation]
        public required IFormFile Image { get; set; }

        [Required(ErrorMessage = "El Tipo de Producto es necesario.")]
        [ProductTypeValidation]
        public required string ProductTypeId { get; set; }        
    }
}