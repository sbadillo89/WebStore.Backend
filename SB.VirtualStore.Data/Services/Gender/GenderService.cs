using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SB.VirtualStore.Data.Services
{
    public class GenderService : IGenderService
    {
        public readonly AppDbContext _context;

        public GenderService(AppDbContext context)
        {
            _context = context;
        }

        public GenderService()
        {

        }

        public void Create(Genre gender)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Genre> GetAllGenders()
        {
            return _context.Genders.ToList();
        }

        public Genre GetGenderById(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public void Update(Genre gender)
        {
            throw new NotImplementedException();
        }
    }
}
