using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Helpers.Validators
{
    public class ImageValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (string.IsNullOrEmpty(file.FileName))
                {
                    return new ValidationResult("El nombre de la Imagen está vacío. Verifique que la Imagen se está cargando correctamente.");
                }
                if (file.Length > 10 * 1024 * 1024)
                {
                    return new ValidationResult($"El tamaño de la Imagen no debe exceder los 10 MB.");
                }
                var extension = Path.GetExtension(file.FileName).ToLower();

                if (extension != ".png" && extension != ".jpg")
                {
                    return new ValidationResult($"El formato de la Imagen debe ser png o jpg.");
                }

            }

            return ValidationResult.Success;
        }        
    }
}