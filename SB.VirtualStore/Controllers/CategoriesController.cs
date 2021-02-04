using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SB.VirtualStore.Data.Models;
using SB.VirtualStore.Data.Models.Response;
using SB.VirtualStore.Data.Services;
using SB.VirtualStore.DTO;

namespace SB.VirtualStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryService _categoryService;
        public CategoriesController(AppDbContext context, IMapper mapper, ICategoryService categoryService)
        {
            _context = context;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            IEnumerable<Category> categories = null;
            await Task.Run(() =>
            {
                categories = _categoryService.GetAll();
            });
            return Ok(_mapper.Map<IEnumerable<CategoryDto>>(categories));
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(Guid id)
        {
            Category category = null;
            await Task.Run(() =>
            {
                category = _categoryService.GetById(id);
            });

            if (category == null)
            {
                return NotFound();
            }

            return _mapper.Map<CategoryDto>(category);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(Guid id, CategoryCreateDto categoryUpdate)
        {
            var categoryFromDB = _categoryService.GetById(id);
            if (categoryFromDB == null)
            {
                return NotFound(
                        new GlobalResponse
                        {
                            RequestData = categoryUpdate,
                            ResponseData = null,
                            Status = System.Net.HttpStatusCode.NotFound,
                            Message = $"There is no category with Id {id}"
                        }
                       );
            }
            try
            { 
                categoryUpdate.CreatedDate = categoryFromDB.CreatedDate;
                _mapper.Map(categoryUpdate, categoryFromDB);
                await Task.Run(() =>
                {
                    _categoryService.SaveChanges(); ;
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            //return NoContent();
            return Ok(categoryUpdate);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryCreateDto categoryCreate)
        {
            Category category = null;
            try
            {
                category = _mapper.Map<Category>(categoryCreate);
                category.Id = Guid.NewGuid();
                _categoryService.Create(category);

                await Task.Run(() =>
                {
                    _categoryService.SaveChanges();
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(Utils.GenerateResponse(categoryCreate, null, System.Net.HttpStatusCode.BadRequest, ex.Message));
            }

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Category>> DeleteCategory(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            if (category.Name == "Admin")
            {
                throw new Exception($"The [{category.Name}] role cannot be removed");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return category;
        }

        private bool CategoryExists(Guid id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
