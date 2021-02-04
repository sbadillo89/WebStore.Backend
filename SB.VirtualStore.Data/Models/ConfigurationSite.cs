using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SB.VirtualStore.Data.Models
{
    [Table("ConfigurationSite")]
    public partial class ConfigurationSite
    {
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(250)]
        public string Slogan { get; set; }
        [MaxLength(1)]
        public byte[] Logo { get; set; }
        [StringLength(100)]
        public string ContactName { get; set; }
        [StringLength(100)]
        public string ContactNumber { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(200)]
        public string Email { get; set; }

        public bool Active { get; set; }
    }
}
