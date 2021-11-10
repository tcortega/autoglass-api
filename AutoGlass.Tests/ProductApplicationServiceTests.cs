using AutoFixture;
using AutoGlass.Application;
using AutoGlass.Application.Dtos;
using AutoGlass.Application.Validators;
using AutoGlass.Domain.Core.Interfaces.Services;
using AutoGlass.Domain.Entities;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AutoGlass.Tests
{
    [TestFixture]
    public class ProductApplicationServiceTests
    {
        private Mock<IProductService> _productServiceStub;
        private Mock<ISupplierService> _supplierServiceStub;
        private Mock<IMapper> _mapperStub;
        private static Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _productServiceStub = new();
            _supplierServiceStub = new();
            _mapperStub = new();

            _fixture = new();
            //SetUpCustomizations();
        }

        [Test]
        public void Create_WithInvalidItem_ThrowsException()
        {
            var productEntity = new Product();
            var productDto = new ProductDto();

            _mapperStub.Setup(x => x.Map<ProductDto>(productEntity)).Returns(productDto);
            var productAppService = new ProductApplicationService(_productServiceStub.Object, _supplierServiceStub.Object, _mapperStub.Object);

            try
            {
                productAppService.Add<ProductDtoValidator>(productDto);
            }
            catch
            {
                Assert.Pass("Throwed exception as expected.");
            }

            Assert.Fail("Did not throw any exceptions.");
        }

        [Test]
        public void GetAll_WithExistingItems_ReturnsExpectedItems()
        {
            // Arrange
            var products = _fixture.Build<Product>()
                .With(c => c.IsActive, true)
                .CreateMany(3);

            var productsDto = _fixture.Build<ProductDto>()
                .With(c => c.IsActive, true)
                .CreateMany(3);

            _productServiceStub.Setup(x => x.GetAll()).Returns(products);
            _mapperStub.Setup(x => x.Map<IEnumerable<ProductDto>>(products)).Returns(productsDto);

            var productAppService = new ProductApplicationService(_productServiceStub.Object, _supplierServiceStub.Object, _mapperStub.Object);

            // Act
            var result = productAppService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            _productServiceStub.VerifyAll();
            _mapperStub.VerifyAll();
        }

        [Test]
        public void Get_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange
            var product = _fixture.Build<Product>()
                .With(c => c.IsActive, true)
                .Create();

            var productSupplierDto = new SupplierDto
            {
                Id = product.Supplier.Id,
                Description = product.Supplier.Description,
                Cnpj = product.Supplier.Cnpj,
                IsActive = product.Supplier.IsActive,
            };

            var productDto = new ProductDto
            {
                Id = product.Id,
                IsActive = product.IsActive,
                Description = product.Description,
                ManufactureDate = product.ManufactureDate,
                ExpirationDate = product.ExpirationDate,
                Supplier = productSupplierDto
            };

            _productServiceStub.Setup(x => x.Get(It.IsAny<int>())).Returns(product);
            _mapperStub.Setup(x => x.Map<ProductDto>(product)).Returns(productDto);

            var productAppService = new ProductApplicationService(_productServiceStub.Object, _supplierServiceStub.Object, _mapperStub.Object);

            // Act
            var result = productAppService.Get(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(productDto, result);
            _productServiceStub.VerifyAll();
            _mapperStub.VerifyAll();
        }

        [Test]
        public void Remove_WithNonExistingItem_ThrowsException()
        {
            // Arrange
            var activeEntity = _fixture.Build<Product>()
                .With(c => c.IsActive, true)
                .Create();

            var deactivatedEntity = _fixture.Build<Product>()
                .With(c => c.IsActive, false)
                .Create();

            _productServiceStub.Setup(x => x.Get(It.IsAny<int>())).Returns(deactivatedEntity);

            var productAppService = new ProductApplicationService(_productServiceStub.Object, _supplierServiceStub.Object, _mapperStub.Object);

            // Act
            try
            {
                productAppService.Remove(activeEntity.Id);
            }
            // Assert
            catch (Exception ex)
            {
                if (ex.Message == "Registros não encontrados!")
                    Assert.Pass("Throws exception");
                else
                    Assert.Fail($"Did not throw proper exception. {ex.Message}");
            }

            _productServiceStub.VerifyAll();
            _mapperStub.VerifyAll();
        }

        [Test]
        public void Update_WithDeactivatedItem_ThrowsException()
        {
            // Arrange
            var deactivatedEntity = _fixture.Build<Product>()
                .With(c => c.IsActive, false)
                .Create();

            var productDto = new ProductDto
            {
                Id = 2,
                IsActive = true,
                Description = "asd",
                ManufactureDate = new DateTime(2020, 07, 12),
                ExpirationDate = DateTime.Now,
                Supplier = new SupplierDto { Id = 2 }
            };

            _productServiceStub.Setup(x => x.Get(It.IsAny<int>())).Returns(deactivatedEntity);
            _mapperStub.Setup(x => x.Map<Product>(productDto)).Returns(deactivatedEntity);

            var productAppService = new ProductApplicationService(_productServiceStub.Object, _supplierServiceStub.Object, _mapperStub.Object);

            // Act
            try
            {
                productAppService.Update<ProductDtoValidator>(productDto);
            }
            // Assert
            catch (Exception ex)
            {
                if (ex.Message == "Não existe nenhum produto cadastrado com este id.")
                    Assert.Pass("Throws expected exception.");
                else
                    Assert.Fail($"Did not throw proper exception. {ex.Message}");
            }
        }
    }
}