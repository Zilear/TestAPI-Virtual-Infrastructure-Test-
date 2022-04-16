using BusinessObjects.Dataset;
using BusinessObjects.Model;
using BusinessObjects.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Repository
{
    public class ProductRepository : IProductRepository
    {
        private TestDbContext _context;
        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }
        public async Task<bool> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name)
        {
            List<Product> result = new List<Product>();
            foreach(var product in _context.Products.ToArray())
            {
                if (product.Name == name) result.Add(product);
            }

            if (result.Count == 0)
                return null;
            return result;
        }

        public async Task<bool> Post(Product product)
        {
            
            try
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
            return true;
        }

        public async Task<bool> Put(Guid id, Product product)
        {
            if (id != product.Id)
            {
                return false;
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
        public bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
