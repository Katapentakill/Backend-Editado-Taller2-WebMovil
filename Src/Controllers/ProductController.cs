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

        /// <summary>
        /// Retrieves all products.
        /// </summary>
        /// <returns>A list of all products.</returns>
        /// <response code="200">Returns a list of all products.</response>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ProductDto>> GetProducts()
        {
            var valor = _service.GetProducts().Result;
            return Ok(valor);
        }

        /// <summary>
        /// Retrieves available products with pagination.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of available products.</returns>
        /// <response code="200">Returns a list of available products.</response>
        [HttpGet("available/{pageNumber}/{pageSize}")]
        [Authorize(Roles = "Usuario")]
        public ActionResult<IEnumerable<ProductDto>> GetAvailableProducts(int pageNumber, int pageSize)
        {
            var valor = _service.GetAvailableProducts(pageNumber, pageSize).Result;
            return Ok(valor);
        }

        /// <summary>
        /// Searches for products based on a query string.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of products matching the search criteria.</returns>
        /// <response code="200">Returns a list of matching products.</response>
        [HttpGet("search")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ProductDto>> SearchProducts([FromQuery] string query)
        {
            var valor = _service.SearchProducts(query).Result;
            return Ok(valor);
        }

        /// <summary>
        /// Retrieves all product types.
        /// </summary>
        /// <returns>A list of product types.</returns>
        /// <response code="200">Returns a list of product types.</response>
        [HttpGet("types")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<ProductType>> GetProductTypes()
        {
            var valor = _service.GetProductTypes().Result;
            return Ok(valor);
        }

        /// <summary>
        /// Searches for available products based on a query string.
        /// </summary>
        /// <param name="query">The search query.</param>
        /// <returns>A list of available products matching the search criteria.</returns>
        /// <response code="200">Returns a list of matching available products.</response>
        [HttpGet("available/search")]
        [Authorize(Roles = "Usuario")]
        public ActionResult<IEnumerable<ProductDto>> SearchAvailableProducts([FromQuery] string query)
        {
            var valor = _service.SearchAvailableProducts(query).Result;
            return Ok(valor);
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="addProductDto">The product details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
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

        /// <summary>
        /// Edits an existing product.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <param name="editProductDto">The updated product details.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>

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

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">The product ID.</param>
        /// <returns>A confirmation message.</returns>
        /// <response code="200">Returns a confirmation message.</response>
        /// <response code="400">If there was an error with the request.</response>
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