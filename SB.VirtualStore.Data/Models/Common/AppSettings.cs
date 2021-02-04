using System;
using System.Collections.Generic;
using System.Text;

namespace SB.VirtualStore.Data.Models.Common
{
    public class AppSettings
    {
        public string Secreto { get; set; }
        public int HorasValidezToken { get; set; }
        public string UserApp { get; set; }
    }
}
