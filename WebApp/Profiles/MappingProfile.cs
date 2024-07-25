using AutoMapper;
using WebApp.Models;
using WebApp.DTOs;

namespace WebApp.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
