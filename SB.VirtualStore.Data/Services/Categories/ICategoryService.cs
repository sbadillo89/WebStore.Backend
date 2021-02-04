using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();

        Category GetById(Guid id);

        void Update(Category role);

        void Create(Category role);

        bool SaveChanges();

    }
}
