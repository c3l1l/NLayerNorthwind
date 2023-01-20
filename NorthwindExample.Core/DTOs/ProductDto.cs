using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindExample.Core.DTOs
{
    public class ProductDto:BaseDto
    {
        public string ProductName { get; set; }       
        public int CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }           
        public bool Discontinued { get; set; }
    }
}
