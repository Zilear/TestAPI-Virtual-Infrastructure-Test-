#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.Dataset;
using BusinessObjects.Model;
using BusinessObjects.Service.Interfaces;

namespace TestAPI__Virtual_Infrastructure_Test_.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly TestDbContext _context;
        private readonly IProductService _productService;

        public ProductsController(TestDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }
        [HttpGet]
        //API-get method to get all products
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _productService.Get();
        }
        [HttpGet("{name}")]
        //API-get method to get product by name
        public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name)
        {
            var product = await _productService.GetByName(name);
            if (product == null) return NotFound();
            else return product;
        }
        [HttpPut("{id}")]
        //API-put method to update existing product
        public async Task<IActionResult> PutProduct(Guid id, Product product)
        {
            if (!_context.Products.Any(e => e.Id == id)) return NotFound();
            else if (await _productService.Put(id, product)) return NoContent();
            else return BadRequest();
        }
        [HttpPost]
        //API-post method to create new product
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            if (await _productService.Post(product))
                return CreatedAtAction("GetProduct", new { id = product.Id }, product);
            else return NoContent();
        }
        [HttpDelete("{id}")]
        //API-delete method to delete existing product
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (await _productService.Delete(id))
                return NoContent();
            else return NotFound();
        }

    }
}
