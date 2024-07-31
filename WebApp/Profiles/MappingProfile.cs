using AutoMapper;
using WebApp.API.Models;
using WebApp.API.DTOs;

namespace WebApp.API.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<ProductDTO, Product>();
        }
    }
}
