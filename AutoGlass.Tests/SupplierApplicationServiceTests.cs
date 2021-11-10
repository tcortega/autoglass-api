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
    public class SupplierApplicationServiceTests
    {
        private Mock<ISupplierService> _supplierServiceStub;
        private Mock<IProductService> _productServiceStub;
        private Mock<IMapper> _mapperStub;
        private static Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _supplierServiceStub = new();
            _productServiceStub = new();
            _mapperStub = new();

            _fixture = new();
        }

        [Test]
        public void Create_WithInvalidItem_ThrowsException()
        {
            var supplierEntity = new Supplier();
            var supplierDto = new SupplierDto();

            _mapperStub.Setup(x => x.Map<SupplierDto>(supplierEntity)).Returns(supplierDto);
            var supplierAppService = new SupplierApplicationService(_supplierServiceStub.Object, _productServiceStub.Object, _mapperStub.Object);

            try
            {
                supplierAppService.Add<SupplierDtoValidator>(supplierDto);
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
            var suppliers = _fixture.Build<Supplier>()
                .With(c => c.IsActive, true)
                .CreateMany(3);

            var suppliersDto = _fixture.Build<SupplierDto>()
                .With(c => c.IsActive, true)
                .CreateMany(3);

            _supplierServiceStub.Setup(x => x.GetAll()).Returns(suppliers);
            _mapperStub.Setup(x => x.Map<IEnumerable<SupplierDto>>(suppliers)).Returns(suppliersDto);

            var supplierAppService = new SupplierApplicationService(_supplierServiceStub.Object, _productServiceStub.Object, _mapperStub.Object);

            // Act
            var result = supplierAppService.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            _supplierServiceStub.VerifyAll();
            _mapperStub.VerifyAll();
        }

        [Test]
        public void Get_WithExistingItem_ReturnsExpectedItem()
        {
            // Arrange
            var supplier = _fixture.Build<Supplier>()
                .With(c => c.IsActive, true)
                .Create();

            var supplierDto = new SupplierDto
            {
                Id = supplier.Id,
                IsActive = supplier.IsActive,
                Cnpj = supplier.Cnpj,
                Description = supplier.Description,
            };

            _supplierServiceStub.Setup(x => x.Get(It.IsAny<int>())).Returns(supplier);
            _mapperStub.Setup(x => x.Map<SupplierDto>(supplier)).Returns(supplierDto);

            var supplierAppService = new SupplierApplicationService(_supplierServiceStub.Object, _productServiceStub.Object, _mapperStub.Object);

            // Act
            var result = supplierAppService.Get(It.IsAny<int>());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(supplierDto, result);
            _supplierServiceStub.VerifyAll();
            _mapperStub.VerifyAll();
        }

        [Test]
        public void Remove_WithNonExistingItem_ThrowsException()
        {
            // Arrange
            var activeEntity = _fixture.Build<Supplier>()
                .With(c => c.IsActive, true)
                .Create();

            var deactivatedEntity = _fixture.Build<Supplier>()
                .With(c => c.IsActive, false)
                .Create();

            _supplierServiceStub.Setup(x => x.Get(It.IsAny<int>())).Returns(deactivatedEntity);

            var supplierAppService = new SupplierApplicationService(_supplierServiceStub.Object, _productServiceStub.Object, _mapperStub.Object);

            // Act
            try
            {
                supplierAppService.Remove(activeEntity.Id);
            }
            // Assert
            catch (Exception ex)
            {
                if (ex.Message == "Registros não encontrados!")
                    Assert.Pass("Throws expected exception.");
                else
                    Assert.Fail($"Did not throw proper exception. {ex.Message}");
            }

            _supplierServiceStub.VerifyAll();
            _mapperStub.VerifyAll();
        }

        [Test]
        public void Update_WithDeactivatedItem_ThrowsException()
        {
            // Arrange
            var deactivatedEntity = _fixture.Build<Supplier>()
                .With(c => c.IsActive, false)
                .Create();

            var supplierDto = _fixture.Build<SupplierDto>()
                .With(c => c.IsActive, true)
                .Create();

            _supplierServiceStub.Setup(x => x.Get(It.IsAny<int>())).Returns(deactivatedEntity);
            _mapperStub.Setup(x => x.Map<Supplier>(supplierDto)).Returns(deactivatedEntity);

            var supplierAppService = new SupplierApplicationService(_supplierServiceStub.Object, _productServiceStub.Object, _mapperStub.Object);

            // Act
            try
            {
                supplierAppService.Update<SupplierDtoValidator>(supplierDto);
            }
            // Assert
            catch (Exception ex)
            {
                if (ex.Message == "Registros não encontrados!")
                    Assert.Pass("Throws exception");
                else
                    Assert.Fail($"Did not throw proper exception. {ex.Message}");
            }
        }
    }
}