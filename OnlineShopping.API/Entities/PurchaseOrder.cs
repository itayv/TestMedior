using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class PurchaseOrder
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(128)]
        public string BusinessPartnerId { get; set; }
        [Required]
        public BusinessPartner BPCode { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        [Required]
        public User CreatedBy { get; set; }

        public User LastUpdatedBy { get; set; }
    }
}
