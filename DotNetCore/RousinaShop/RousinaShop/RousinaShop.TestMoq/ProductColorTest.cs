using Moq;
using RousincaShop.Admin.Data.Entities;
using RousincaShop.Admin.Data.Repositories.Interfaces;
using RousincaShop.Admin.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RousinaShop.TestMoq
{
    public class ProductColorTest
    {
        private readonly ProductService _sut;
        private readonly Mock<IProductRepository> _productRepoMock = new Mock<IProductRepository>();
        private readonly Mock<IloggerService> _loggerMock = new Mock<IloggerService>();

        public ProductColorTest()
        {
            _sut = new ProductService(_productRepoMock.Object, _loggerMock.Object);
        }

        [Fact]
        public  void GetProductbyId_ShouldReturnProduct_WhenExists()
        {
            //arrange:
            var productid = 2;
            var tempProduct = new Product
            {
                Id = productid,
                ProductName = "test product",
                ProductDescription = "test produect desc",
                Price = (decimal)43.3
            };

            _productRepoMock.Setup(x => x.Get(productid)).Returns(tempProduct);

            //act: 
            var product =  _sut.Get(productid);

            //assert: 

            Assert.NotNull(product); // Test if null
            Assert.IsType<Product>(product); // Test type        
            Assert.Equal(product.Id, productid);
        }

        [Fact]
        public void GetProduct_ShouldReturnProduct_WithLoggerResult()
        {
            //arrange
            int productid = 0;           
            _productRepoMock.Setup(x => x.Get(productid)).Returns(() => null);
            //act
            var product = _sut.Get(productid);
            //assert
            Assert.Null(product);
            _loggerMock.Verify(x => x.LogInformation("Error: id should bigger than one"), Times.Once);
            _loggerMock.Verify(x => x.LogInformation("success"), Times.Never);
          //  _loggerMock.Verify(x => x.LogInformation("done"), Times.AtLeastOnce);
        }


        [Fact]
        public void GetProducts_shouldReturnProducts_MoreThanOne()
        {
            //arrange: 
            List<Product> tempProducts = new List<Product>();
            tempProducts.Add
                (new Product {             
                Id = 1,
                ProductName = "test product",
                ProductDescription = "test produect desc",
                Price = (decimal)43.3
                });

            tempProducts.Add
                (new Product
                {
                    Id = 2,
                    ProductName = "test product",
                    ProductDescription = "test produect desc",
                    Price = (decimal)43.3
                });

            tempProducts.Add
                (new Product
                {
                    Id = 3,
                    ProductName = "test product",
                    ProductDescription = "test produect desc",
                    Price = (decimal)43.3
                });

            _productRepoMock.Setup(x => x.GetAll()).Returns(tempProducts);
            //act
            var products = _sut.GetAll();

            //assert
            Assert.IsType<List<Product>>(products);
            Assert.True(products.Count() > 0);
        }
    }
}
