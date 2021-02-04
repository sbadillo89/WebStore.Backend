using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class ProductCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [MaxLength(1)]
        public byte[] Image { get; set; }

        [Required]
        public Guid? CategoryId { get; set; }
        [StringLength(50)]

        public string Reference { get; set; }
        [StringLength(100)]
        public string Barcode { get; set; }

        [Required]
        public decimal? Cost { get; set; }

        [Required]
        public decimal? Price { get; set; } 
        public decimal? Discount { get; set; } 
         
        public virtual Category Category { get; set; }

    }
}
