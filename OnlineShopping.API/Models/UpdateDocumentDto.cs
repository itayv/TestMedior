using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Models
{
    public class UpdateDocumentDto
    {
        public int DocId { get; set; }
        public string DocType { get; set; }
        public string BPCode { get; set; }
        public string UserName { get; set; }
        public List<ItemDto> Items { get; set; }
    }
}
