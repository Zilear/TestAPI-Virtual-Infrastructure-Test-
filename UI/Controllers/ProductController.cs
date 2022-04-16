using BusinessObjects.Dataset;
using BusinessObjects.Model;
using BusinessObjects.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UI.Models;

namespace UI.Controllers
{
    public class ProductController : Controller
    {
        private ILogger<ProductController> _logger;
        private readonly TestDbContext _context;
        private readonly IProductService _productService;

        public ProductController(TestDbContext context, ILogger<ProductController> logger, IProductService productService)
        {
            _context = context;
            _logger = logger;
            _productService = productService;
        }
        
        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 2;

            var count = _productService.Get().Result.Value.Count();
            var items = _productService.Get().Result.Value.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);
            var viewModel = new ProductViewModel
            {
                CurrentPageProducts = items,
                PageViewModel = pageViewModel,
                Products = _productService.Get().Result.Value
            };
            return View(viewModel);
        }
        public async Task<IActionResult> GetByName(string name)
        {
            var product = await _productService.GetByName(name);
            if (product == null)
                return View(null);
            else return View(product.Value);
        }
        public IActionResult PutProduct(string? id)
        {
            if (id != null)
            {
                Product product = _context.Products.FirstOrDefault(p => p.Id == new Guid(id));
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        [HttpPost]
        public  IActionResult PutProduct(Product product)
        {
            if (!_context.Products.Any(e => e.Id == product.Id)) return BadRequest();
            else if (_productService.Put(product.Id, product).Result) return RedirectToAction(nameof(Index));
            else return BadRequest();
        }
        public async Task<IActionResult> PostProduct( string name, string description)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(description))
            {
                Guid id = Guid.NewGuid();
                var product = new Product()
                {
                    Id = id,
                    Name = name,
                    Description = description
                };
                if (await _productService.Post(product))
                    return View(Ok());
                else return View(NoContent());
            }
            else
            {
                return View(null);
            }
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(string? id)
        {
            if (id != null)
            {
                Product product = await _context.Products.FirstOrDefaultAsync(p => p.Id == new Guid(id));
                if (product != null)
                    return View(product);
            }
            return NotFound();
        }
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            if (id != null)
            {
                if (await _productService.Delete(id))
                    return RedirectToAction(nameof(Index));
                else return View(NotFound());
            }
            return RedirectToAction(nameof(Index));
        }
    }
    
}