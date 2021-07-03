using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SB.VirtualStore.Data.Models;
using SB.VirtualStore.Data.Models.Request;
using SB.VirtualStore.Data.Models.Response;
using SB.VirtualStore.Data.Services;
using SB.VirtualStore.DTO;

namespace SB.VirtualStore.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController] 
    [EnableCors("CorsPolicy")]
    public class UserController : ControllerBase
    {
        private IMapper _mapper;
        private IUserService _userService;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllAsync()
        {
            IEnumerable<User> users = null;

            await Task.Run(() =>
            {
                users = _userService.GetAll();
            });
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(usersDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByIdAsync(Guid id)
        {
            User user = null;

            await Task.Run(() =>
            {
                user = _userService.GetById(id);
            });
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<GlobalResponse>> Login([FromBody] AuthRequest model)
        {
            GlobalResponse response = new GlobalResponse();
            UserResponse userResponse = null;
            await Task.Run(() =>
            {
                userResponse = _userService.Auth(model);
            });

            response.Status = System.Net.HttpStatusCode.OK;
            response.RequestData = model;
            response.ResponseData = userResponse;
            response.Message = "Complete Process";
            if (userResponse == null)
            {
                response.Status = System.Net.HttpStatusCode.BadRequest;
                response.Message = "Incorrect User or Password";
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostAsync(UserCreateDto userCreate)
        {
            User newUser;
            try
            {
                newUser = _mapper.Map<User>(userCreate);
                newUser.Id = Guid.NewGuid();
                _userService.Create(newUser);

                await Task.Run(() =>
                {
                    return _userService.SaveChanges();
                });
            }
            catch (DbUpdateException ex)
            {
                return BadRequest(Utils.GenerateResponse(userCreate, null, System.Net.HttpStatusCode.BadRequest, ex.Message));
            }
            return Ok(newUser);
        }

        // PUT: api/User/5
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutASync(Guid id, UserCreateDto userUpdate)
        {
            var userFromDB = _userService.GetById(id);
            if (userFromDB == null)
            {
                return NotFound(
                                    new GlobalResponse
                                    {
                                        RequestData = userUpdate,
                                        ResponseData = null,
                                        Status = System.Net.HttpStatusCode.NotFound,
                                        Message = $"There is no user with Id {id}"
                                    }
                                );
            }
            _mapper.Map(userUpdate, userFromDB);
            _userService.Update(userFromDB);

            try
            {
                await Task.Run(() =>
                {
                    _userService.SaveChanges();
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok(userUpdate);
        }


        [HttpPost("Register")]
        public async Task<ActionResult<GlobalResponse>> Register(UserCreateDto newUserDto)
        {
            GlobalResponse response = new GlobalResponse();
            User newUser = new User();
            try
            {
                bool success = false;
                newUser = _mapper.Map<User>(newUserDto);
                newUser.Id = Guid.NewGuid();
                newUser.CreatedDate = DateTime.Now;
                newUser.Active = true;

                _userService.Register(newUser);
                await Task.Run(() =>
                {
                    success = _userService.SaveChanges();
                });

                response.Status = System.Net.HttpStatusCode.OK;
                response.RequestData = newUserDto;
                response.ResponseData = newUser;
                response.Message = "Complete Process";
                if (!success)
                {
                    response.Status = System.Net.HttpStatusCode.BadRequest;
                    response.Message = "Error registering user";
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(Utils.GenerateResponse(newUserDto, null, System.Net.HttpStatusCode.BadRequest, ex.Message));
            }

            //return CreatedAtAction("GetById", new { id = newUser.Id }, newUser);
        }
    }
}
