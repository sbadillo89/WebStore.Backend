using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SB.VirtualStore.Data.Models;
using SB.VirtualStore.Data.Models.Common;
using SB.VirtualStore.Data.Models.Request;
using SB.VirtualStore.Data.Models.Response;
using SB.VirtualStore.Data.Models.Tools;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace SB.VirtualStore.Data.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _appDbContext;
        private readonly AppSettings _appSettings;
        public UserService(AppDbContext appDbContext, IOptions<AppSettings> appSettings)
        {
            _appDbContext = appDbContext;
            _appSettings = appSettings.Value;
        }


        public User GetById(Guid id)
        {
            return _appDbContext.Users.
                    Include(x => x.Rol).
                    FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _appDbContext.Users.Include(u => u.Rol).ToList();
        }

        public UserResponse Auth(AuthRequest authRequest)
        {
            UserResponse userResponse = new UserResponse();

            string passwordEncrypt = Encryp.GetSHA256(authRequest.Password);

            var user = _appDbContext.Users.
                        Where(u => u.Email == authRequest.Email && u.Password == passwordEncrypt)
                        .Include(r=> r.Rol)
                        .FirstOrDefault();

            int validezToken = (_appSettings != null ? _appSettings.HorasValidezToken : 1);
            if (user == null) return null;
            userResponse.Email = user.Email;
            userResponse.Token = GetToken(user);
            userResponse.UserName = user.UserName;
            userResponse.ExpireDate = DateTime.Now.AddHours(validezToken);
            userResponse.IsAdmin = (user.Rol.Name == "Admin" ? true : false);

            return userResponse;
        }

        public void Register(User newUser)
        {
            string customerUser = (_appSettings != null ? _appSettings.UserApp : "User");

            if (newUser == null)
                throw new ArgumentNullException(nameof(newUser));

            if (!newUser.RolId.HasValue)
                newUser.RolId = _appDbContext.Roles.FirstOrDefault(r => r.Name == customerUser).Id;

            if (UserExists(newUser))
                throw new Exception("Ya existe un usuario registrado con este correo.");

            newUser.Password = Encryp.GetSHA256(newUser.Password);
            _appDbContext.Users.Add(newUser);
        }

        public bool SaveChanges()
        {
            return Convert.ToBoolean(_appDbContext.SaveChanges());
        }

        private string GetToken(User usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave = Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString() ),
                        new Claim(ClaimTypes.Email, usuario.Email)
                    }
                    ),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private bool UserExists(User user)
        {
            return _appDbContext.Users.Any(u => u.Email == user.Email);
        }

    }
}
