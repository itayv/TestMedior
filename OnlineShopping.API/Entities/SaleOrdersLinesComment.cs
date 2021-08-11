using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class SaleOrdersLinesComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentLineID { get; set; }

        //[Required]
        //public int SaleOrderId { get; set; }

        [Required]
        public SaleOrder DocID { get; set; }

        [Required]
        public int SaleOrdersLineId { get; set; }

        [Required]
        public SaleOrdersLine Lineid { get; set; }
        [Required]
        public string Comment { get; set; }

    }
}
