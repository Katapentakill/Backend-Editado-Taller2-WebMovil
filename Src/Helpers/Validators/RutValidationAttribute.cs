using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Helpers.Validators
{
    public class RutValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value != null){
                string rut = (string)value;

                if(rut == "") return false;

                int rutNumber = int.Parse(rut.Split('-')[0]);
                char digitoVerificador = rut.Split('-')[1].ToLowerInvariant()[0];

                int[] coefficients = { 2, 3, 4, 5, 6, 7 };
                int sum = 0;
                int index = 0;

                while (rutNumber != 0)
                {
                    sum += rutNumber % 10 * coefficients[index];
                    rutNumber /= 10;
                    index = (index + 1) % 6;
                }

                int result = 11 - (sum % 11);
                char verificador;
                if(result == 10){
                    verificador = 'k';
                }
                else{
                    verificador = result.ToString()[0];
                }

                return verificador == digitoVerificador;

                
               
            }
            return false;
        }
    }        
    
}