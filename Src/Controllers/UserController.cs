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


        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// <response code="200">Returns a list of all users.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var valor = _service.GetUsers().Result;
            return Ok(valor);
        }

        /// <summary>
        /// Searches for users based on a query string.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of users matching the search criteria.</returns>
        /// <response code="200">Returns a list of matching users.</response>
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<UserDto>> SearchUsers([FromQuery] string query)
        {
            var valor = _service.SearchUsers(query).Result;
            return Ok(valor);
        }

        /// <summary>
        /// Retrieves all genders.
        /// </summary>
        /// <returns>A list of genders.</returns>
        /// <response code="200">Returns a list of genders.</response>
        [HttpGet("genders")]
        [Authorize]
        public ActionResult<IEnumerable<Gender>> GetGenders()
        {
            var valor = _service.GetGenders().Result;
            return Ok(valor);
        }

        /// <summary>
        /// Retrieves all purchases made by a specific user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>A list of purchases made by the user.</returns>
        /// <response code="200">Returns a list of purchases made by the user.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="401">If the user is not authorized.</response>
        [HttpGet("{id}/purchases")]
        [Authorize(Roles = "Usuario")]
        public async Task<ActionResult<IEnumerable<Purchase>>> GetPurchasesByUser(int id)
        {
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var valor = await _purchaseService.GetPurchasesByUser(id);
                return Ok(valor);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieves all purchases.
        /// </summary>
        /// <returns>A list of all purchases.</returns>
        /// <response code="200">Returns a list of all purchases.</response>        
        [HttpGet("purchases")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Purchase>> GetPurchases()
        {
            var valor = _purchaseService.GetPurchases().Result;
            return Ok(valor);
        }

        /// <summary>
        /// Searches for purchases based on a query string.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of purchases matching the search criteria.</returns>
        /// <response code="200">Returns a list of matching purchases.</response>
        [HttpGet("purchases/search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Purchase>> SearchPurchases([FromQuery] string query)
        {
            var valor = _purchaseService.SearchPurchases(query).Result;
            return Ok(valor);
        }

        /// <summary>
        /// Edits a user's information.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="editUserDto">The updated user details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<string>> EditUser(int id, [FromBody] EditUserDto editUserDto)
        {
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var valor = await _service.EditUserInfo(id, editUserDto);
                if(!valor){
                    return NotFound("Usuario no encontrado");
                }
                return Ok("Datos editados con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Changes a user's password.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="changePasswordDto">The new password details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="401">If the user is not authorized.</response>
        /// <response code="404">If the user is not found.</response>
        [HttpPut("{id}/password")]
        [Authorize]
        public async Task<ActionResult<string>> ChangeUserPassword(int id, [FromBody] ChangePasswordDto changePasswordDto)
        {
            try{
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != id){
                    return Unauthorized("Las IDs no coinciden.");
                }

                var valor = await _service.ChangeUserPassword(id, changePasswordDto);
                if(!valor){
                    return NotFound("Usuario no encontrado");
                }
                return Ok("Contraseña cambiada con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Changes the state of a user.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <param name="newUserState">The new state of the user.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If the state is not valid or there was an error with the request.</response>
        /// <response code="404">If the user was not found.</response>
        [HttpPut("{id}/state")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<string>> ChangeUserState(int id, [FromBody] string newUserState)
        {
            try{
                bool newState = bool.Parse(newUserState);
                var valor = await _service.ChangeUserState(id, newState);
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