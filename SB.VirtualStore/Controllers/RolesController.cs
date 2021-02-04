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
    public class RolesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;
        public RolesController(AppDbContext context, IMapper mapper, IRoleService roleService)
        {
            _context = context;
            _mapper = mapper;
            _roleService = roleService;
        }

        // GET: api/Roles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            IEnumerable<Role> roles = null;
            await Task.Run(() =>
                              {
                                  roles = _roleService.GetAllRoles();
                              });
            var rolesDto = _mapper.Map<IEnumerable<RoleDto>>(roles);
            return Ok(rolesDto);
        }

        // GET: api/Roles/5
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<RoleDto>> GetRole(Guid id)
        {
            Role role = null;
            await Task.Run(() =>
                                 {
                                     role = _roleService.GetRoleById(id);
                                 });

            if (role == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RoleDto>(role));
        }

        // PUT: api/Roles/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutRole(Guid id, RoleCreateDto roleUpdateDto)
        {
            var roleFromDB = _roleService.GetRoleById(id);
            if (roleFromDB == null)
            {
                return NotFound(
                                    new GlobalResponse
                                    {
                                        RequestData = roleUpdateDto,
                                        ResponseData = null,
                                        Status = System.Net.HttpStatusCode.NotFound,
                                        Message = $"There is no role with Id {id}"
                                    }
                                );
            }
            roleUpdateDto.CreatedDate = roleFromDB.CreatedDate;
            _mapper.Map(roleUpdateDto, roleFromDB);
            _roleService.UpdateRole(roleFromDB);

            try
            {
                await Task.Run(() =>
                {
                    _roleService.SaveChanges();
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
        }

        // POST: api/Roles
        [HttpPost]
        public async Task<ActionResult<RoleDto>> PostRole(RoleCreateDto role)
        {
            Role newRole;
            try
            {
                newRole = _mapper.Map<Role>(role);
                newRole.Id = Guid.NewGuid();
                _roleService.CreateRole(newRole);

                await Task.Run(() =>
                {
                    _roleService.SaveChanges();
                });
            }
            catch (DbUpdateException ex)
            { 
                return BadRequest(Utils.GenerateResponse(role, null, System.Net.HttpStatusCode.BadRequest, ex.Message));
            }
            return CreatedAtAction("GetRole", new { id = newRole.Id }, newRole);
        }

        // DELETE: api/Roles/5
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<RoleDto>> DeleteRole(Guid id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return _mapper.Map<RoleDto>(role);
        }

        private bool RoleExists(Guid id)
        {
            return _context.Roles.Any(e => e.Id == id);
        }
    }
}
