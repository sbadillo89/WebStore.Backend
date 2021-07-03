using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.VirtualStore.Data.Models;
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
    public class GenderController : ControllerBase
    {
        private IMapper _mapper;
        private IGenderService _genderService;

        public GenderController(IMapper mapper, IGenderService genderService)
        {
            _mapper = mapper;
            _genderService = genderService;
        }
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAllAsync()
        {
            IEnumerable<Genre> genders = null;

            await Task.Run(() =>
            {
                genders = _genderService.GetAllGenders();
            });
            var gendersDto = _mapper.Map<IEnumerable<GenreDto>>(genders);
            return Ok(gendersDto);  
        } 
          
    }
}
