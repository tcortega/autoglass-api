using AutoGlass.Application.Mappers;
using AutoMapper;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
