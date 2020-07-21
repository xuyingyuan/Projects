using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.VisualBasic;
using Moq;
using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Data.Repositories.Wrapper;
using RousincaShop.Admin.Service;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RousinaShop.TestMoq
{
    public class ProductServiceTest
    {
        private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepositoryMock = new Mock<IProductRepository>();
        private readonly Mock<IloggerService> _loggerMock = new Mock<IloggerService>();


        public ProductServiceTest()
        {
            _sut = new ProductService(_productRepositoryMock.Object, _loggerMock.Object);
        }
        [Fact]
        public void GetProductByIdAsync_ShouldReturnProduct_WhenExists()
        {
            //Arrange
            var productid = 2;
            var tmpProduct = new Product
            {
                Id = productid,
                ProductName="test product",
                ProductDescription ="test product desc",
                SizeScaleId =1,
                Price =(decimal)30.45       
            };
            _productRepositoryMock.Setup(x => x.Get(productid))
                 .Returns(tmpProduct);

          

            //Act
            var product = _sut.Get(productid);
            //Assert
            Assert.Equal(product.Id, productid);
        }


        [Fact]
        public void GetProductByIdAsync_ShouldReturnError_WithIdlessthanOne()
        {
            //Arrange
            var productid = -2;
            var tmpProduct = new Product
            {
                Id = productid,
                ProductName = "test product",
                ProductDescription = "test product desc",
                SizeScaleId = 1,
                Price = (decimal)30.45
            };
            _productRepositoryMock.Setup(x => x.Get(productid))
                 .Returns(()=> null);

            //Act
            var product = _sut.Get(productid);
            //Assert
            Assert.Null(product);
        }

        [Fact]
        public void GetProductByIdAsync_ShouldReturnProduct_WithLogInfo()
        {
            //Arange
            int productid = 4;
            var tmpproduct = new Product
            {
                Id = productid,
                ProductName = "test product",
                ProductDescription = "test product desc",
                SizeScaleId = 1,
                Price = (decimal)30.45
            };
            _productRepositoryMock.Setup(x => x.Get(It.IsAny<int>())).Returns(tmpproduct);

            //Act
            var returnProduct = _sut.Get(productid);
            //Assert
            _loggerMock.Verify(x =>
           x.LogInformation("Retrieved a product with Id: {Id}", productid), Times.Once);
            _loggerMock.Verify(x =>
            x.LogInformation("Unable to Retrieved a product with Id: {Id}", productid), Times.Never);
        }

    }
}
