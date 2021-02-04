using System;
using System.Collections.Generic;
using SB.VirtualStore.Data.Models;

namespace SB.VirtualStore.Data.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();

        Role GetRoleById(Guid id);

        void UpdateRole( Role role);

        void CreateRole(Role role);

        bool SaveChanges();
    }
}
