using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.Services
{
    public interface IProductService:IService<Product>
    {
        Task<List<ProductsWithCategoryDto>> GetProductsWithCategory();
        Task<ProductWithCategoryAndSupplierDto> GetProductWithCategoryAndSupplier(int id);
        Task<List<ProductWithCategoryAndSupplierDto>> GetProductsWithCategoryAndSupplier();
    }
}
