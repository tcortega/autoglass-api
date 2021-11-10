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
                .ReverseMap();
        }
    }
}