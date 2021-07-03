using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SB.VirtualStore.Data.Models
{
    [Table("Product")]
    public partial class Product
    {  
        [Key]
        public Guid Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(500)]
        public string Description { get; set; } 
        public string Image { get; set; }
        public Guid CategoryId { get; set; }

        [StringLength(50)]
        public string Reference { get; set; }

        [StringLength(100)]
        public string Barcode { get; set; }
        [Column(TypeName = "decimal(12, 2)")]
        public decimal? Cost { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal? Price { get; set; }

        [Column(TypeName = "decimal(12, 2)")]
        public decimal? Discount { get; set; }
        public bool? Active { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        public string Color { get; set; }

        public string Talla { get; set; }

        public Guid GenreId { get; set; }


        [ForeignKey(nameof(GenreId))]
        //[InverseProperty("Products")]
        public virtual Genre Gender { get; set; }

        [ForeignKey(nameof(CategoryId))]
        //[InverseProperty("Products")]
        public virtual Category Category { get; set; }
    }
}
