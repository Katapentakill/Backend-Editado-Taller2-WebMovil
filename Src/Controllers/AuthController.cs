using Microsoft.AspNetCore.Mvc;
using project_dotnet7_api.Src.DTO.User;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {

            _authService = authService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="registerUserDto">The user registration details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns the confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
        [HttpPost("register")]
        public async Task<ActionResult<LoggedUserDto>> Register(RegisterUserDto registerUserDto)
        {
            try{
                var response = await _authService.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
        
        /// <summary>
        /// Logs in an existing user.
        /// </summary>
        /// <param name="loginUserDto">The user login details.</param>
        /// <returns>The logged-in user details.</returns>
        /// <response code="200">Returns the logged-in user details.</response>
        /// <response code="400">If there was an error with the request.</response>
        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserDto>> Login(LoginUserDto loginUserDto)
        {
            try{
                var response = await _authService.Login(loginUserDto);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }        
    }
}