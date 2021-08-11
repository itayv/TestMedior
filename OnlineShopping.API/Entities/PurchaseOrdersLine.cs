using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class PurchaseOrdersLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineID { get; set; }
        [Required]
        public int PurchaseOrderId { get; set; }
        [Required]
        public PurchaseOrder DocID { get; set; }
        [Required]
        [MaxLength(128)]
        public string ItemId { get; set; }
        [Required]
        public Item Itemcode { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        [Required]
        public User CreatedBy { get; set; }
        public User LastUpdatedBy { get; set; }
    }
}
