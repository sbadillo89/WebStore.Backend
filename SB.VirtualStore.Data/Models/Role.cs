using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SB.VirtualStore.Data.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        [Key]
        public Guid Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [InverseProperty(nameof(User.Rol))]
        public virtual ICollection<User> Users { get; set; }
    }
}
