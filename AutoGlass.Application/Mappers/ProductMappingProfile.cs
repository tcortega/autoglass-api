using AutoGlass.Application.Dtos;
using AutoGlass.Domain.Entities;
using AutoMapper;

namespace AutoGlass.Application.Mappers
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductDto, Product>()
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}