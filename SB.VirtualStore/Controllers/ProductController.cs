using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SB.VirtualStore.Data.Models;
using SB.VirtualStore.Data.Services;
using SB.VirtualStore.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SB.VirtualStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        public readonly IProductService _productService;
        public readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            IEnumerable<Product> products = null;

            await Task.Run(() =>
             {
                 products = _productService.GetAll();
             });

            return Ok(_mapper.Map<IEnumerable<ProductDto>>(products));
        }

        // GET api/<ProductController>/5
        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> GetById(Guid id)
        {
            Product product = null;
            await Task.Run(() =>
            {
                product = _productService.GetById(id);
            });

            if (product == null)
            {
                return NotFound();
            }

            return _mapper.Map<ProductDto>(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult<ProductDto>> Post(ProductCreateDto productCreateDto)
        {
            Product newProduct = null;
            try
            { 
                newProduct = _mapper.Map<Product>(productCreateDto);
                newProduct.Id = Guid.NewGuid();
                newProduct.Active = true;
                _productService.Create(newProduct);

                await Task.Run(() =>
                {
                    _productService.SaveChanges();
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(Utils.GenerateResponse(productCreateDto, null, System.Net.HttpStatusCode.BadRequest, ex.Message));
            }

            return CreatedAtAction("GetById", new { id = newProduct.Id }, newProduct);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
         
    }
}
