using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SB.VirtualStore.Data.Models
{
    public partial class User
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        public Guid? RolId { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [ForeignKey(nameof(RolId))]
        [InverseProperty(nameof(Role.Users))]
        public virtual Role Rol { get; set; }
    }
}
