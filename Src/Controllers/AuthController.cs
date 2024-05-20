using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost("register")]
        public async Task<ActionResult<string>> Register(RegisterUserDto registerUserDto)
        {
            try{
                var response = await _authService.RegisterUser(registerUserDto);
                return Ok(response);
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoggedUserDto>> Login(LoginUserDto loginUserDto)
        {
            try{
                Thread.Sleep(500);
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