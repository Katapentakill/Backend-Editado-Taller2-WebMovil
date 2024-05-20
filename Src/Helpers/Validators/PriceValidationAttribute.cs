using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Helpers.Validators
{
    public class PriceValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value?.ToString();

            if (valueString != null)
            {
                if(!int.TryParse(valueString, out int Price))
                {
                    return new ValidationResult("El Precio debe ser un número.");
                }

                if(Price < 0){
                    return new ValidationResult("El Precio debe ser un número entero positivo.");
                }

                if(Price > 100000000){
                    return new ValidationResult("El Precio no debe ser mayor que $100.000.000.");
                }
            }


            return ValidationResult.Success;
        }        
    }
}