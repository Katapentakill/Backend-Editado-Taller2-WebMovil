using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Helpers.Validators
{
    public class QuantityValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value?.ToString();

            if (valueString != null)
            {
                if(!int.TryParse(valueString, out int Quantity))
                {
                    return new ValidationResult("La Cantidad debe ser un número.");
                }

                if(Quantity <= 0){
                    return new ValidationResult("La Cantidad debe ser un número entero positivo y mayor a 0.");
                }
            }


            return ValidationResult.Success;
        }
    }       
}