using BusinessObjects.Dataset;
using BusinessObjects.Model;
using BusinessObjects.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TestWebApp.Controllers
{
    public class ProductsController : Controller
    {
        private readonly TestDbContext _context;
        private readonly IProductService _productService;

        public ProductsController(TestDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        // GET: ProductController/Details/5
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _productService.Get();
        }
        public async Task<ActionResult<Product>> GetByName(string name)
        {
            var product = await _productService.GetByName(name);
            if (product == null) return NotFound();
            else return product;
        }
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (!_context.Products.Any(e => e.Id == id)) return NotFound();
            else if (await _productService.Put(id, product)) return NoContent();
            else return BadRequest();
        }
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (await _productService.Post(product))
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            else return NoContent();
        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (await _productService.Delete(id))
                return NoContent();
            else return NotFound();
        }

    }
}
