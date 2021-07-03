using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public interface IGenderService
    {
        IEnumerable<Genre> GetAllGenders();

        Genre GetGenderById(Guid id);

        void Update(Genre gender);

        void Create(Genre gender);

        bool SaveChanges();
    }
}
