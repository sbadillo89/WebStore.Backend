using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class ConfigurationSiteCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Slogan { get; set; }
       
        public string Logo { get; set; }
        [Required]
        [StringLength(100)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(100)]
        public string ContactNumber { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Email { get; set; }

    }
}
