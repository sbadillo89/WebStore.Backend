using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class UserDto
    { 
        public Guid Id { get; set; } 
        public string UserName { get; set; } 
        public string Email { get; set; } 
        public string Password { get; set; }
        public Guid RolId { get; set; }
        public bool Active { get; set; } 
        public DateTime? CreatedDate { get; set; }
        public  RoleDto Rol { get; set; }
    }
}
