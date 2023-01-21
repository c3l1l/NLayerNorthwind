using Microsoft.AspNetCore.Http;
using NorthwindExample.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.DTOs
{
    public class CategoryAddDto
    {   
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
       
    }
}
