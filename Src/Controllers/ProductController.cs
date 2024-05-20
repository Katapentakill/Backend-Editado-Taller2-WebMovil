using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using project_dotnet7_api.Src.DTO.Product;
using project_dotnet7_api.Src.Models;
using project_dotnet7_api.Src.Services.Interfaces;

namespace project_dotnet7_api.Src.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService productService)
        {
            _service = productService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var valor = _service.GetProducts().Result;
            return Ok(valor);
        }

        [HttpGet("available/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Usuario")]
        public ActionResult<IEnumerable<ProductDto>> GetAvailableProducts(int pageNumber, int pageSize)
        {
            var valor = _service.GetAvailableProducts(pageNumber, pageSize).Result;
            return Ok(valor);
        }

        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ProductDto>> SearchProducts([FromQuery] string query)
        {
            var valor = _service.SearchProducts(query).Result;
            return Ok(valor);
        }

        [HttpGet("types")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ProductType>> GetProductTypes()
        {
            var valor = _service.GetProductTypes().Result;
            return Ok(valor);
        }

        [HttpGet("available/search")]
        [Authorize(Roles = "Usuario")]
        public ActionResult<IEnumerable<ProductDto>> SearchAvailableProducts([FromQuery] string query)
        {
            var valor = _service.SearchAvailableProducts(query).Result;
            return Ok(valor);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> AddProduct([FromForm] AddProductDto addProductDto)
        {
            try{
                var valor = _service.AddProduct(addProductDto).Result;
                return Ok("Producto agregado con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> EditProduct(int id, [FromForm] EditProductDto editProductDto)
        {
            try{
                var valor = _service.EditProduct(id, editProductDto).Result;
                return Ok("Producto editado con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<string> DeleteProduct(int id)
        {
            try{
                var valor = _service.DeleteProduct(id).Result;
                return Ok("Producto eliminado con éxito.");
            }
            catch(Exception ex){
                return BadRequest(ex.Message);
            }
        }
    }
}