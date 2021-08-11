using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.API.Entities
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(1024)]
        public string FullName { get; set; }

        [Required]
        [MaxLength(254)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool Active { get; set; }
    }
}
