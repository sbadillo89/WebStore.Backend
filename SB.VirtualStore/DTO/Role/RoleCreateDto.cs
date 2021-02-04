using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class RoleCreateDto
    { 
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }  
    }
}
