using AutoGlass.Application.Dtos;
using AutoGlass.Domain.Entities;
using AutoMapper;

namespace AutoGlass.Application.Mappers
{
    public class SupplierMappingProfile : Profile
    {
        public SupplierMappingProfile()
        {
            CreateMap<SupplierDto, Supplier>()
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}