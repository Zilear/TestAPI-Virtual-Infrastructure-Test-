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
    /// <summary>
    /// Repository of methods to Product model
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private TestDbContext _context;
        public ProductRepository(TestDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// This method deletes product in dataset by it's Guid
        /// </summary>
        /// <param name="id">Guid of an object</param>
        /// <returns>none</returns>
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
        /// <summary>
        /// This method returns all products in dataset
        /// </summary>
        /// <returns>List of products</returns>
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return await _context.Products.ToListAsync();
        }
        /// <summary>
        /// This method returns product(-s) by name
        /// </summary>
        /// <param name="name">name of product(-s) </param>
        /// <returns>List of product(-s)</returns>
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
        /// <summary>
        /// This method creates new Product object in dataset
        /// </summary>
        /// <param name="product">New product, which will be added to dataset</param>
        /// <returns>bool result</returns>
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
        /// <summary>
        /// Updates existing object in dataset
        /// </summary>
        /// <param name="id">Id of changing object</param>
        /// <param name="product">Product that will be changed</param>
        /// <returns>bool result</returns>
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
        //private method that checks existence of product in dataset
        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
