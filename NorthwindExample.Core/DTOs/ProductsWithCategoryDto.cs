using NorthwindExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.DTOs
{
    public class ProductsWithCategoryDto:ProductDto
    {
       public CategoryDto Category { get; set; }
    }
}
