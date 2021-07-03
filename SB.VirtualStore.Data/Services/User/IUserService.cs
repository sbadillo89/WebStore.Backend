using SB.VirtualStore.Data.Models;
using SB.VirtualStore.Data.Models.Request;
using SB.VirtualStore.Data.Models.Response;
using System;
using System.Collections.Generic;

namespace SB.VirtualStore.Data.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();

        User GetById(Guid id);
        UserResponse Auth(AuthRequest authRequest);
        void Register(User registerRequest);
        void Create(User user);
        void Update(User user);
        bool SaveChanges();
    }
}
