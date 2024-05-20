using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Helpers.Validators;

namespace project_dotnet7_api.Src.DTO.Purchase
{
    public class PurchaseDto
    {
        [Required(ErrorMessage = "La Cantidad es obligatoria.")]
        [QuantityValidation]
        public string Quantity { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Producto es obligatorio.")]
        [IdValidation]
        public string ProductId { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Usuario es obligatorio.")]
        [IdValidation]
        public string UserId { get; set; } = string.Empty;        
    }
}