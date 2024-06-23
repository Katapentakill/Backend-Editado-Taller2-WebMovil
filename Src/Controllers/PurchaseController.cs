using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_dotnet7_api.Src.DTO.Purchase;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchaseController : ControllerBase
    {
        readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Makes a purchase.
        /// </summary>
        /// <param name="purchaseDto">The purchase details.</param>
        /// <returns>Information about the purchase.</returns>
        /// <response code="200">Returns information about the purchase.</response>
        /// <response code="400">If there was an error with the request.</response>
        /// <response code="401">If the user is not authorized.</response>
        [HttpPost]
        [Authorize(Roles = "Usuario")]
        public async Task<ActionResult<PurchaseInfoDto>> MakePurchase([FromBody] PurchaseDto purchaseDto)
        {
            try
            {
                var idClaim = User.Claims.FirstOrDefault(claim => claim.Type == "Id");
                if(idClaim != null && int.Parse(idClaim.Value) != int.Parse(purchaseDto.UserId)){
                    return Unauthorized("Las IDs no coinciden.");
                }
                var result = await _purchaseService.MakePurchase(purchaseDto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}