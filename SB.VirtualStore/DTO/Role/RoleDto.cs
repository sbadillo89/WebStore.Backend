using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class RoleDto
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }  
    }
}
