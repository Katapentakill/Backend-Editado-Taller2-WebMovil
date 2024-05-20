using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Helpers.Validators
{
    public class ProductTypeValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value?.ToString();

            if (valueString != null){
                if(!int.TryParse(valueString, out int ProductTypeId))
                {
                    return new ValidationResult("El Tipo de Producto no es valido");
                }
            }    

            return ValidationResult.Success;
        }        
    }
}