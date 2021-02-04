using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class UserCreateDto
    { 
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public Guid? RolId { get; set; }  
    }
}
