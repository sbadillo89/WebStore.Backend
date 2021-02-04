using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SB.VirtualStore.DTO
{
    public class ConfigurationSiteDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    
        public string Slogan { get; set; }
    
        public byte[] Logo { get; set; }

        public string ContactName { get; set; }
      
        public string ContactNumber { get; set; }
     
        public string Address { get; set; }
      
        public string Email { get; set; }

        public bool Active { get; set; }

    }
}
