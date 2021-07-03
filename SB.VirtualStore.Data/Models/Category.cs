using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace SB.VirtualStore.Data.Models
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            //Products = new HashSet<Product>(); 
        }

        [Key]
        public Guid Id { get; set; }
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        public bool? Active { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        //[InverseProperty(nameof(Product.Category))]
        //public virtual ICollection<Product> Products { get; set; }
        
    }
}
