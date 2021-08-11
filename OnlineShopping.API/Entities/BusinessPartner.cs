using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class BusinessPartner
    {
        [Key]
        [MaxLength(128)]
        public string BPCode { get; set; }

        [Required]
        [MaxLength(254)]
        public string BPName { get; set; }

        [Required]
        [MaxLength(1)]
        public string BPTypeId { get; set; }

        [Required]
        public BPType BPType { get; set; }

        [Required]
        public bool Active { get; set; }

    }
}
