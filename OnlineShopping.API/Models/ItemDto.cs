using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Models
{
    public class ItemDto
    {
        public string ItemName { get; set; }
        public decimal Quantity { get; set; }
        public string Comment { get; set; }
    }
}
