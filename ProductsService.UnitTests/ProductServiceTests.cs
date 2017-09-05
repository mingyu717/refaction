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
    public class ProductServiceTests
    {
        private ProductService _underTest;
        private Mock<IProductsRepository> _productRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _productRepositoryMock = new Mock<IProductsRepository>();
            _underTest = new ProductService(_productRepositoryMock.Object);
        }

        [Test]
        public void Save_Test_ProductsRepositorySaveShouldBeCalled()
        {
            _productRepositoryMock.Setup(m => m.Save(It.IsAny<Product>()));

            _underTest.Save(new Product());

            _productRepositoryMock.Verify(m => m.Save(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void Get_Test_ProductsRepositoryGetShouldBeCalled()
        {
            var guid = new Guid();

            _productRepositoryMock.Setup(m => m.Get(guid)).Returns(new Product());

            _underTest.GetById(guid);

            _productRepositoryMock.Verify(m => m.Get(guid), Times.Once);
        }

        [Test]
        public void GetByName_Test_ProductsRepositoryGetShouldBeCalled()
        {
            const string name = "testName";

            _productRepositoryMock.Setup(m => m.Get(name)).Returns(new List<Product>());

            _underTest.GetByName(name);

            _productRepositoryMock.Verify(m => m.Get(name), Times.Once);
        }


        [Test]
        public void GetAll_Test_ProductsRepositoryGetShouldBeCalled()
        {
            _productRepositoryMock.Setup(m => m.GetAll()).Returns(new List<Product>());

            _underTest.GetAll();

            _productRepositoryMock.Verify(m => m.GetAll(), Times.Once);
        }

        [Test]
        public void Create_Test_ProductsRepositoryCreateShouldBeCalled()
        {
            _productRepositoryMock.Setup(m => m.Create(It.IsAny<Product>()));

            _underTest.Create(new Product());

            _productRepositoryMock.Verify(m => m.Create(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void Update_Test_ProductsRepositoryUpdateAndGetShouldBeCalled()
        {
            var guid = new Guid();

            _productRepositoryMock.Setup(m => m.Update(It.IsAny<Product>()));
            _productRepositoryMock.Setup(m => m.Get(guid)).Returns(new Product());

            _underTest.Update(guid, new Product());

            _productRepositoryMock.Verify(m => m.Get(guid), Times.Once);
            _productRepositoryMock.Verify(m => m.Update(It.IsAny<Product>()), Times.Once);
        }

        [Test]
        public void Delete_Test_ProductsRepositoryDeleteShouldBeCalled()
        {
            var guid = new Guid();

            _productRepositoryMock.Setup(m => m.Delete(guid));

            _underTest.Delete(guid);

            _productRepositoryMock.Verify(m => m.Delete(guid), Times.Once);
        }
    }
}
