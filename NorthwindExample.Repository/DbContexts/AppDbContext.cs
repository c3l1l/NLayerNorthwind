using Microsoft.EntityFrameworkCore;
using NorthwindExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Repository.DbContexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
       public DbSet<Category> Categories { get; set; }
       public  DbSet<Product> Products { get; set;}
       public  DbSet<Supplier> Suppliers { get; set; }
    }
}
