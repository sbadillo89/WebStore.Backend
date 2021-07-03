using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public Guid? CategoryId { get; set; }
        public string Reference { get; set; }
        public string Barcode { get; set; }
        public decimal? Cost { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public bool? Active { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string Color { get; set; }

        public string Talla { get; set; }

        public Guid? GenreId { get; set; }

        public virtual Genre Gender { get; set; }

        public virtual Category Category  { get; set; }
    }
}
