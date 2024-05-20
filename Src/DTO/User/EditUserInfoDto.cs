using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.DTO.User
{
    public class EditUserInfoDto
    {
        public string? Name { get; set; }

        public DateTime? Birthday { get; set; }

        public int? GenderId { get; set; }        
    }
}