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
                // Eliminar puntos y guión del rut
                rut = rut.Replace(".", "").Replace("-", "");

                // Extraer dígito verificador
                char dv = char.ToUpper(rut[^1]);
                rut = rut.Substring(0, rut.Length - 1);

                // Calcular dígito verificador esperado
                int m = 0, s = 1;
                for (; rut != ""; rut = rut.Substring(0, rut.Length - 1))
                    s = (s + int.Parse(rut[rut.Length - 1].ToString()) * (9 - m++ % 6)) % 11;

                char calculatedDv = (char)((s > 0 ? s - 1 : 75) + '0');

                return dv == calculatedDv;
            }
            return false;
        }
    }        
    
}