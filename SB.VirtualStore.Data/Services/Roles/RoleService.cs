using Microsoft.EntityFrameworkCore;
using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public class RoleService : IRoleService
    {

        private readonly AppDbContext _context;

        public RoleService( AppDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Role> GetAllRoles()
        {
            return _context.Roles.Where(c => c.Active == true).ToList();
        }

        public Role GetRoleById(Guid id)
        {
            return _context.Roles.Where(c => c.Active == true).FirstOrDefault(r => r.Id == id);
        }

        public void CreateRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException(nameof(role));

            _context.Roles.Add(role);
        }

        public void UpdateRole( Role role)
        {
            _context.Entry(role).State = EntityState.Modified;
        }

        public bool SaveChanges()
        {
            return Convert.ToBoolean(_context.SaveChanges());
        }
    }
}
