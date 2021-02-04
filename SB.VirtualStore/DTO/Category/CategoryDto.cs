using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class CategoryDto
    {  
        public Guid Id { get; set; } 
        public string Name { get; set; }
        public bool? Active { get; set; } 
        public DateTime? CreatedDate { get; set; } 
    }
}
