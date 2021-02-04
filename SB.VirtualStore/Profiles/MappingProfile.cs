
using AutoMapper;
using SB.VirtualStore.Data.Models;
using SB.VirtualStore.DTO;
using System.Collections.Generic;

namespace SB.VirtualStore.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        { 
            CreateMap<Role, RoleDto>();
            CreateMap<RoleCreateDto, Role>();

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryCreateDto, Category>();

            CreateMap<ConfigurationSite, ConfigurationSiteDto>();
            CreateMap<ConfigurationSiteCreateDto, ConfigurationSite>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductCreateDto, Product>();

            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();
        }
    }
}
