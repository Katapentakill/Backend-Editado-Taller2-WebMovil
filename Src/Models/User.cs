using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Rut { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public DateTime Birthday { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsActive { get; set; }


        //Relaciones
        public int RoleId { get; set; }

        public Role Role { get; set; } = null!;

        public int GenderId { get; set; }

        public Gender Gender { get; set;} = null!;
    }
}