using BusinessObjects.Model;
using BusinessObjects.Repository;
using BusinessObjects.Repository.Interfaces;
using BusinessObjects.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        /// <summary>
        /// This method deletes product in dataset by it's Guid
        /// </summary>
        /// <param name="id">Guid of an object</param>
        /// <returns>bool result</returns>
        public Task<bool> Delete(Guid id)
        {
            return _productRepository.Delete(id);
        }
        /// <summary>
        /// This method returns all products in dataset
        /// </summary>
        /// <returns>List of products</returns>
        public Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return _productRepository.Get();
        }
        /// <summary>
        /// This method returns product(-s) by name
        /// </summary>
        /// <param name="name">name of product(-s) </param>
        /// <returns>List of product(-s)</returns>
        public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name)
        {
            return await _productRepository.GetByName(name);
        }
        /// <summary>
        /// This method creates new Product object in dataset
        /// </summary>
        /// <param name="product">New product, which will be added to dataset</param>
        /// <returns>bool result</returns>
        public Task<bool> Post(Product product)
        {
            return _productRepository.Post(product);
        }

        /// <summary>
        /// Updates existing object in dataset
        /// </summary>
        /// <param name="id">Id of changing object</param>
        /// <param name="product">Product that will be changed</param>
        /// <returns>bool result</returns>
        public Task<bool> Put(Guid id, Product product)
        {
            return _productRepository.Put(id, product);
        }

    }
}
