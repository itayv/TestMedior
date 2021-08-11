using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class Item
    {
        [Key]
        [MaxLength(128)]
        public string ItemCode { get; set; }

        [Required]
        [MaxLength(254)]
        public string ItemName { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
