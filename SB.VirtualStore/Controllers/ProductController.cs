using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SB.VirtualStore.Data.Models;
using SB.VirtualStore.Data.Models.Response;
using SB.VirtualStore.Data.Services;
using SB.VirtualStore.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks; 
 
namespace SB.VirtualStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [EnableCors("CorsPolicy")]
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
        [HttpPut("{id:Guid}")]
        public async Task<ActionResult<ProductDto>> PutAsync(ProductCreateDto product, Guid id)
        {
            var productFromDB = _productService.GetById(id);
            if (productFromDB == null)
            {
                return NotFound(
                        new GlobalResponse
                        {
                            RequestData = product,
                            ResponseData = null,
                            Status = System.Net.HttpStatusCode.NotFound,
                            Message = $"There is not product with Id {id}"
                        }
                       );
            }
            try
            {
                _mapper.Map(product, productFromDB);
                _productService.Update(productFromDB);
                await Task.Run(() =>
                {
                    _productService.SaveChanges();
                });
            }
            catch (DbUpdateConcurrencyException ex)
            { 
            throw;
            }

            //return NoContent();
            return Ok(productFromDB);
        }

    }
}
