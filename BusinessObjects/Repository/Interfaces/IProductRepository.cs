using BusinessObjects.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<ActionResult<IEnumerable<Product>>> Get();
        Task<ActionResult<IEnumerable<Product>>> GetByName(string name);
        Task<bool> Post(Product product);
        Task<bool> Delete(Guid id);
        Task<bool> Put(Guid id, Product product);
    }
}
