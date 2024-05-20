using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace project_dotnet7_api.Src.DTO.User
{
    public class LoggedUserDto
    {
        public required UserDto User { get; set; }
        public required string Token { get; set; }        
    }
}