using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Helpers.Validators
{
    public class StockValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var valueString = value?.ToString();

            if (valueString != null)
            {
                if(!int.TryParse(valueString, out int Stock))
                {
                    return new ValidationResult("El Stock debe ser un número.");
                }

                if(Stock < 0){
                    return new ValidationResult("El Stock debe ser un número entero positivo.");
                }

                if(Stock > 100000){
                    return new ValidationResult("El Stock no debe ser mayor que 100.000.");
                }

            }


            return ValidationResult.Success;
        }        
    }
}