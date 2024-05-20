using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using project_dotnet7_api.Src.Helpers.Validators;

namespace project_dotnet7_api.Src.DTO.User
{
    public class EditUserDto
    {
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El Nombre solo puede contener caracteres del abecedario español.")]
        [MinLength(8, ErrorMessage = "El nombre debe tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre debe tener a lo más 255 caracteres.")]
        public string? Name { get; set; }

        [DataType(DataType.Date)]
        [DateValidation]   
        public string? Birthday { get; set; }

        [GenderValidation]
        public string? GenderId { get; set; }        
    }
}