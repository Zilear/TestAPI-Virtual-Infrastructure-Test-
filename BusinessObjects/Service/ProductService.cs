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

        public Task<bool> Delete(Guid id)
        {
            return _productRepository.Delete(id);
        }

        public Task<ActionResult<IEnumerable<Product>>> Get()
        {
            return _productRepository.Get();
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetByName(string name)
        {
            return await _productRepository.GetByName(name);
        }

        public Task<bool> Post(Product product)
        {
            return _productRepository.Post(product);
        }

        public Task<bool> Put(Guid id, Product product)
        {
            return _productRepository.Put(id, product);
        }

    }
}
