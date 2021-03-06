using AutoGlass.Application.Mappers;
using AutoMapper;
using NUnit.Framework;

namespace AutoGlass.Tests
{
    [TestFixture]
    public class MapperTests
    {
        [Test]
        public void AutoMapperSupplierProfile_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<SupplierMappingProfile>());
            config.AssertConfigurationIsValid();
        }

        [Test]
        public void AutoMapperProductProfile_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
                {
                    cfg.AddProfile<SupplierMappingProfile>();
                    cfg.AddProfile<ProductMappingProfile>();
                });
            config.AssertConfigurationIsValid();
        }
    }
}