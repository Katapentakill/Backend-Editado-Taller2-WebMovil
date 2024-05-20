using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IPurchaseService _purchaseService;
        public UserController(IUserService service, IPurchaseService purchaseService)
        {
            _service = service;
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var valor = _service.GetUsers().Result;
            return Ok(valor);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> SearchUsers([FromQuery] string query)
        {
            var valor = _service.SearchUsers(query).Result;
            return Ok(valor);
        }

        [HttpGet("genders")]
        [Authorize]
        public ActionResult<IEnumerable<Gender>> GetGenders()
        {
            var valor = _service.GetGenders().Result;
            return Ok(valor);
        }

        [HttpGet("{id}/purchases")]
        [Authorize(Roles = "Usuario")]
        public ActionResult<IEnumerable<Purchase>> GetPurchasesByUser(int id)
        {
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var valor = _purchaseService.GetPurchasesByUser(id).Result;
                return Ok(valor);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("purchases")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Purchase>> GetPurchases()
        {
            var valor = _purchaseService.GetPurchases().Result;
            return Ok(valor);
        }

        [HttpGet("purchases/search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Purchase>> SearchPurchases([FromQuery] string query)
        {
            var valor = _purchaseService.SearchPurchases(query).Result;
            return Ok(valor);
        }

        [HttpPut("{id}")]
        [Authorize]
        public ActionResult<string> EditUser(int id, [FromBody] EditUserDto editUserDto)
        {
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var valor = _service.EditUserInfo(id, editUserDto).Result;
                if(!valor){
                    return NotFound("Usuario no encontrado");
                }
                return Ok("Datos editados con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/password")]
        [Authorize]
        public ActionResult<string> ChangeUserPassword(int id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var valor = _service.ChangeUserPassword(id, changePasswordDto).Result;
                if(!valor){
                    return NotFound("Usuario no encontrado");
                }
                return Ok("Contraseña cambiada con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/state")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> ChangeUserState(int id, [FromBody] string newUserState)
        {
            try{
                bool newState = bool.Parse(newUserState);
                var valor = _service.ChangeUserState(id, newState).Result;
                if(!valor){
                    return NotFound("Usuario no encontrado");
                }
                return Ok("Estado cambiado con éxito.");
            }
            catch (FormatException)
            {
                return BadRequest("El estado no es válido.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}