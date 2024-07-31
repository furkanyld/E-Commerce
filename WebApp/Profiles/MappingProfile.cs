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

            CreateMap<Product, ModifyProductDTO>();
            CreateMap<ModifyProductDTO, Product>();

            // Customer.FirstName = CustomerDTO.Firstname
            SourceMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            DestinationMemberNamingConvention = LowerUnderscoreNamingConvention.Instance;
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
        }
    }
}
