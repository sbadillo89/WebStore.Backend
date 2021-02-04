using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();

        Product GetById(Guid id);

        void Update(Product product);

        void Create(Product newProduct);

        bool SaveChanges();

    }
}
