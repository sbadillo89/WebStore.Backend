using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class CategoryCreateDto
    {
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public bool? Active { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }
    }
}
