using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SB.VirtualStore.Data.Models
{
    [Table("Provider")]
    public partial class Provider
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(100)]
        public string ContactName { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
