using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class BPType
    {
        [Key]
        [MaxLength(1)]
        public string TypeCode { get; set; }
   
        [Required]
        [MaxLength(20)]
        public string TypeName { get; set; }
    }
}
