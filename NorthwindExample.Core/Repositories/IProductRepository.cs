using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.Repositories
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
        Task<Product> GetProductWithCategoryAndSupplier(int id);
        Task<List<Product>> GetProductsWithCategoryAndSupplier();
    }
}
