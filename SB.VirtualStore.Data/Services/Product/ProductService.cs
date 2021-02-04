using Microsoft.EntityFrameworkCore;
using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public class ProductService : IProductService
    {
        public readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Product newProduct)
        {
            if (newProduct == null)
                throw new ArgumentNullException(nameof(newProduct));

            _context.Products.Add(newProduct);
        }

        public IEnumerable<Product> GetAll()
        { 
            return _context.Products.
                    Include( p=> p.Category).ToList();
        }

        public Product GetById(Guid id)
        {
            return _context.Products
                    .Include(x=> x.Category)
                    .FirstOrDefault(x => x.Id == id);
        }

        public bool SaveChanges()
        {
            return Convert.ToBoolean(_context.SaveChanges());
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
    }
}
