
using System.Collections.Generic;
using MongoDB.Driver;
using Moq;
using ProductsApi.Controllers;
using Xunit;
using System.Linq;

namespace ProductsApi.Test
{
    public class ProductControllerTest
    {
        Mock<IProductRepository> _repositoryMock;
        ProductsController _controller;

        public ProductControllerTest()
        {
            //Setup
            _repositoryMock = new Mock<IProductRepository>();
            _controller = new ProductsController(_repositoryMock.Object);

            var _moqData = BuildData();
            //Return all products
            _repositoryMock.Setup(ss => ss.GetAllProducts()).ReturnsAsync(_moqData);

            //Return products by ID
            _repositoryMock.Setup(ss => ss.GetProductsById(It.IsAny<int>())).ReturnsAsync((int id) => _moqData.Where(m => m.Id == id).FirstOrDefault());

            //Get Products by name
            _repositoryMock.Setup(ss => ss.GetProducts(It.IsAny<string>())).ReturnsAsync((string name) => _moqData.Where(m => m.Name == name).FirstOrDefault());

            //Get Products by Minimum Price
            _repositoryMock.Setup(ss => ss.GetProductByMinPrice()).ReturnsAsync(_moqData.OrderBy(m => m.Price).FirstOrDefault());

            //Get Products by Max  Price
            _repositoryMock.Setup(ss => ss.GetProductByMaxPrice()).ReturnsAsync(_moqData.OrderByDescending(m => m.Price).FirstOrDefault());

            //Get Products by Minimum Rating 
            _repositoryMock.Setup(ss => ss.GetProductByMinRating()).ReturnsAsync(_moqData.OrderBy(m => m.attribute.rating.Value).FirstOrDefault());

            //Get Products by Max Price
            _repositoryMock.Setup(ss => ss.GetProductByMaxRating()).ReturnsAsync(_moqData.OrderByDescending(m => m.attribute.rating.Value).FirstOrDefault());

            _repositoryMock.Setup(ss => ss.GetProductByFantastic(It.IsAny<bool>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((bool value, int type, string name) => _moqData.Where(m => m.attribute.fantastic.Value == value && m.attribute.fantastic.Name == name && m.attribute.fantastic.Type == type).ToList());

        }

        [Fact]
        public async void GetProductsByIdTest()
        {
            //Given

            //When
            var result = await _controller.Get(1);
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<Product>(result); // Test type
            Assert.Equal(1, result.Id); // Verify it is the right product
        }

        [Fact]
        public async void GetAllProductsTest()
        {
            //Given

            //When
            var result = await _controller.Get();
            //Then
            Assert.NotNull(result); // Test if null
            Assert.Equal(2, result.Count()); // Verify the correct Number

        }

        [Fact]
        public async void GetProductByNameTest()
        {
            //Given

            //When
            var result = await _controller.Get("Product1");
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<Product>(result); // Test type
            Assert.Equal(1, result.Id); // Verify it is the right product
        }

        [Fact]
        public async void GetProductsByMinPriceTest()
        {
            //Given

            //When
            var result = await _controller.GetByMinPrice();
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<Product>(result); // Test type
            Assert.Equal((decimal)10.5, result.Price); // Verify it is the right product
        }

        [Fact]
        public async void GetProductsByMaxPriceTest()
        {
            //Given

            var result = await _controller.GetByMaxPrice();
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<Product>(result); // Test type
            Assert.Equal((decimal)900.5, result.Price); // Verify it is the right product
        }

        [Fact]
        public async void GetProductsByMinRatingTest()
        {
            //Given

            //When
            var result = await _controller.GetByMinRating();
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<Product>(result); // Test type
            Assert.Equal((decimal)2.5, result.attribute.rating.Value); // Verify it is the right product
        }

        [Fact]
        public async void GetProductsByMaxRatingTest()
        {
            //Given

            //When
            var result = await _controller.GetByMaxRating();
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<Product>(result); // Test type
            Assert.Equal((decimal)5.0, result.attribute.rating.Value); // Verify it is the right product
        }

        [Fact]
        public async void GetProductsByFantasticTest()
        {
            //When
            var result = await _controller.GetByFantastic(false, 1, "fantastic");
            //Then
            Assert.NotNull(result); // Test if null
            Assert.IsType<List<Product>>(result); // Test type
            Assert.Equal(1, result.ToList().Count);
            Assert.Equal("fantastic", result.ToList()[0].attribute.fantastic.Name); // Verify it is the right product
        }

        private IList<Product> BuildData()
        {
            Product p1 = new Product()
            {
                Id = 1,
                Name = "Product1",
                Sku = "170-10-8596",
                Price = (decimal)10.5,
                attribute = new Attribute()
                {
                    fantastic = new Fantastic()
                    {
                        Name = "fantastic",
                        Type = 1,
                        Value = true
                    },
                    rating = new Rating()
                    {
                        Name = "rating",
                        Type = "2",
                        Value = (decimal)2.5
                    }
                }
            };

            Product p2 = new Product()
            {
                Id = 2,
                Name = "Product2",
                Sku = "555-55-8596",
                Price = (decimal)900.5,
                attribute = new Attribute()
                {
                    fantastic = new Fantastic()
                    {
                        Name = "fantastic",
                        Type = 1,
                        Value = false
                    },
                    rating = new Rating()
                    {
                        Name = "rating",
                        Type = "1",
                        Value = (decimal)5.0
                    }
                }
            };

            IList<Product> productList = new List<Product>();

            productList.Add(p1);
            productList.Add(p2);

            return productList;
        }

    }
}