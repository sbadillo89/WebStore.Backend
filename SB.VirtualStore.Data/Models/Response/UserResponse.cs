using System;
using System.Collections.Generic;
using System.Text;

namespace SB.VirtualStore.Data.Models.Response
{
    public class UserResponse
    {
        public Guid UserId { get; set; }

        public string UserName { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }

        public DateTime ExpireDate { get; set; }

        public bool IsAdmin { get; set; }
    }
}
