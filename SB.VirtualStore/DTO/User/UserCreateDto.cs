using SB.VirtualStore.Data.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool Active { get; set; }
    }
}
