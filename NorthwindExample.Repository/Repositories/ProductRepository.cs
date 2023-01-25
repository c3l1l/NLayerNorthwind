using Microsoft.EntityFrameworkCore;
using NorthwindExample.Core.DTOs;
using NorthwindExample.Core.Models;
using NorthwindExample.Core.Repositories;
using NorthwindExample.Repository.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            var products=await _context.Products.Include(x=>x.Category).ToListAsync();
            return products;
        }

        public async Task<Product> GetProductWithCategoryAndSupplier(int id)
        {
            var product = await _context.Products.Include(x => x.Category).Include(x=>x.Supplier).Where(x=>x.ProductID==id).FirstOrDefaultAsync();
            return product;
        }
        public async Task<List<Product>> GetProductsWithCategoryAndSupplier()
        {
            var products = await _context.Products.Include(x => x.Category).Include(x=>x.Supplier).ToListAsync();
            return products;
        }
    }
}
