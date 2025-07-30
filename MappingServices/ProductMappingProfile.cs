using AutoMapper;
using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Models.Dtos;

namespace technical_tests_backend_ssr.MappingServices
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
