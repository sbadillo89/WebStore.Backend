using SB.VirtualStore.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public class CategoryService : ICategoryService
    {
        public readonly AppDbContext _context;
        public CategoryService(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }
        public void Create(Category category)
        {
            if (category == null)
                throw new ArgumentNullException(nameof(category));

            _context.Categories.Add(category);
        }

        public IEnumerable<Category> GetAll()
        {

            return _context.Categories.Where(c=> c.Active==true).ToList();

        }

        public Category GetById(Guid id)
        {

            return _context.Categories.Where(c => c.Active == true).FirstOrDefault(r => r.Id == id);
        }

        public bool SaveChanges()
        {
            return Convert.ToBoolean(_context.SaveChanges());
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

        }
    }
}
