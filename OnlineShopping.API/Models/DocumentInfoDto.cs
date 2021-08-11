using OnlineShopping.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Models
{
    public class DocumentInfoDto
    {
        public string FullName_createdBy { get; set; }
        public string FullName_updateBy { get; set; }
        public string BPName { get; set; }
        public bool BPActive { get; set; }
        //public string ItemName { get; set; }

        public SaleOrder SaleOrder { get; set; }
        public List<SaleOrdersLine> SaleOrdersLines { get; set; }
        public List<SaleOrdersLinesComment> SaleOrdersLinesComments { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; }
        public List<PurchaseOrdersLine> PurchaseOrdersLines { get; set; }




    }
}
