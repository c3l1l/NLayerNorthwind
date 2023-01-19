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
        DbSet<Category> Categories { get; set; }
        DbSet<Product> Products { get; set;}
        DbSet<Supplier> Suppliers { get; set; }
    }
}
