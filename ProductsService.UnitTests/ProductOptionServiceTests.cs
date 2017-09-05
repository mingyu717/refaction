using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ProductsService.Contract;
using ProductsService.Contract.Models;
using ProductsService.Implementation;

namespace ProductsService.UnitTests
{
    [TestFixture]
    public class ProductOptionServiceTests
    {
        private ProductOptionService _underTest;
        private Mock<IProductOptionsRepository> _productOptionsRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _productOptionsRepositoryMock = new Mock<IProductOptionsRepository>();
            _underTest = new ProductOptionService(_productOptionsRepositoryMock.Object);
        }

        [Test]
        public void Save_Test_ProductOptionsRepositorySaveShouldBeCalled()
        {
            _productOptionsRepositoryMock.Setup(m => m.Save(It.IsAny<ProductOption>()));

            _underTest.Save(new Guid(), new ProductOption());

            _productOptionsRepositoryMock.Verify(m=>m.Save(It.IsAny<ProductOption>()), Times.Once);
        }

        [Test]
        public void Get_Test_ProductOptionsRepositoryGetShouldBeCalled()
        {
            var guid = new Guid();

            _productOptionsRepositoryMock.Setup(m => m.Get(guid)).Returns(new ProductOption());

            _underTest.Get(guid);

            _productOptionsRepositoryMock.Verify(m => m.Get(guid), Times.Once);
        }

        [Test]
        public void Create_Test_ProductOptionsRepositoryCreateShouldBeCalled()
        {
            var guid = new Guid();

            _productOptionsRepositoryMock.Setup(m => m.Create(It.IsAny<ProductOption>()));

            _underTest.Create(guid, new ProductOption());

            _productOptionsRepositoryMock.Verify(m => m.Create(It.IsAny<ProductOption>()), Times.Once);
        }

        [Test]
        public void Update_Test_ProductOptionsRepositoryUpdateAndGetShouldBeCalled()
        {
            var guid = new Guid();

            _productOptionsRepositoryMock.Setup(m => m.Update(It.IsAny<ProductOption>()));
            _productOptionsRepositoryMock.Setup(m => m.Get(guid)).Returns(new ProductOption());

            _underTest.Update(guid, new ProductOption());

            _productOptionsRepositoryMock.Verify(m => m.Get(guid), Times.Once);
            _productOptionsRepositoryMock.Verify(m => m.Update(It.IsAny<ProductOption>()), Times.Once);
        }

        [Test]
        public void Delete_Test_ProductOptionsRepositoryDeleteShouldBeCalled()
        {
            var guid = new Guid();

            _productOptionsRepositoryMock.Setup(m => m.Delete(guid));

            _underTest.Delete(guid);

            _productOptionsRepositoryMock.Verify(m => m.Delete(guid), Times.Once);
        }
    }
}
