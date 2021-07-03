using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SB.VirtualStore.Data.Models
{

    [Table("Genre")]
    public class Genre
    {
        public Genre()
        {
           // Products = new HashSet<Product>();
        }

        [Key]
        public Guid Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }


        //[InverseProperty(nameof(Product.Gender))]
        //public virtual ICollection<Product> Products { get; set; }
         
    }
}
