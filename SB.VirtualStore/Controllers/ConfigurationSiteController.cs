using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ConfigurationSiteController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfigurationService _configurationService;
        public ConfigurationSiteController(AppDbContext context, IMapper mapper, IConfigurationService configurationService)
        {
            _context = context;
            _mapper = mapper;
            _configurationService = configurationService;
        }

        // GET: api/ConfigurationSite
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConfigurationSiteDto>>> GetConfigurations()
        {
            IEnumerable<ConfigurationSite> configurationSites = null;
            await Task.Run(() =>
            {
                configurationSites = _configurationService.GetAll();
            });
            return Ok(_mapper.Map<IEnumerable<ConfigurationSiteDto>>(configurationSites));
        }

        // GET: api/ConfigurationSite/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConfigurationSiteDto>> GetConfiguration(Guid id)
        {
            ConfigurationSite configuration = null;
            await Task.Run(() =>
            {
                configuration = _configurationService.GetById(id);
            });

            if (configuration == null)
            {
                return NotFound();
            }

            return _mapper.Map<ConfigurationSiteDto>(configuration);
        }

        // PUT: api/ConfigurationSite/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutConfiguration(Guid id, ConfigurationSiteCreateDto configurationSiteUpdateDto)
        {
            var configurationFromDB = _configurationService.GetById(id);
            if (configurationFromDB == null)
            {
                return NotFound(
                        new GlobalResponse
                        {
                            RequestData = configurationSiteUpdateDto,
                            ResponseData = null,
                            Status = System.Net.HttpStatusCode.NotFound,
                            Message = $"There is no category with Id {id}"
                        }
                       );
            }
            try
            { 
                _mapper.Map(configurationSiteUpdateDto, configurationFromDB);
                await Task.Run(() =>
                {
                    _configurationService.SaveChanges(); ;
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
            //return Ok(configurationSiteUpdateDto);
        }

        [HttpPost]
        public async Task<ActionResult<ConfigurationSiteDto>> PostConfiguration(ConfigurationSiteCreateDto configurationSiteDto)
        {
            ConfigurationSite configuration = null;
            try
            {
                configuration = _mapper.Map<ConfigurationSite>(configurationSiteDto);
                configuration.Id = Guid.NewGuid();
                configuration.Active = true;

                _configurationService.Create(configuration);

                await Task.Run(() =>
                {
                    _configurationService.SaveChanges();
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(Utils.GenerateResponse(configurationSiteDto, null, System.Net.HttpStatusCode.BadRequest, ex.Message));
            }

            return CreatedAtAction("GetConfigurations", new { id = configuration.Id }, configuration);
        }

    }
}
