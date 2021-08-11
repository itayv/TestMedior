using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Models
{
    public class GetDocumentDto
    {
        public int DocId { get; set; }
        public string DocType { get; set; }//BPType
    }
}
