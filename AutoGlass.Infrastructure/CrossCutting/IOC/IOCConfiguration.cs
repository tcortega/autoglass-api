using Autofac;
using AutoGlass.Application;
using AutoGlass.Application.Interfaces;
using AutoGlass.Application.Mappers;
using AutoGlass.Domain.Core.Interfaces.Repositories;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Services;
using AutoGlass.Infrastructure.Data.Repositories;
using AutoMapper;

namespace AutoGlass.Infrastructure.CrossCutting.IOC
{
    public class IOCConfiguration
    {
        public static void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SupplierApplicationService>().As<ISupplierApplicationService>();
            builder.RegisterType<ProductApplicationService>().As<IProductApplicationService>();

            builder.RegisterType<SupplierService>().As<ISupplierService>();
            builder.RegisterType<ProductService>().As<IProductService>();

            builder.RegisterType<SupplierRepository>().As<ISupplierRepository>();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new SupplierMappingProfile());
                cfg.AddProfile(new ProductMappingProfile());
            }));
            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}