using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Models
{
    public class DocumentDto
    {
        public string BPCode { get; set; }
        public string BPType { get; set; }
        public string CreatedBy { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
