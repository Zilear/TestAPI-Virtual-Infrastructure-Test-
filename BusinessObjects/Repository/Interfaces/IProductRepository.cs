using BusinessObjects.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Repository.Interfaces
{
    /// <summary>
    /// Interface of repository of methods to Product model
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// This method returns all products in dataset
        /// </summary>
        /// <returns>List of products</returns>
        Task<ActionResult<IEnumerable<Product>>> Get();
        /// <summary>
        /// This method returns product(-s) by name
        /// </summary>
        /// <param name="name">name of product(-s) </param>
        /// <returns>List of product(-s)</returns>
        Task<ActionResult<IEnumerable<Product>>> GetByName(string name);
        /// <summary>
        /// This method creates new Product object in dataset
        /// </summary>
        /// <param name="product">New product, which will be added to dataset</param>
        /// <returns>bool result</returns>
        Task<bool> Post(Product product);
        /// <summary>
        /// This method deletes product in dataset by it's Guid
        /// </summary>
        /// <param name="id">Guid of an object</param>
        /// <returns>bool result</returns>
        Task<bool> Delete(Guid id);
        /// <summary>
        /// Updates existing object in dataset
        /// </summary>
        /// <param name="id">Id of changing object</param>
        /// <param name="product">Product that will be changed</param>
        /// <returns>bool result</returns>
        Task<bool> Put(Guid id, Product product);
    }
}
