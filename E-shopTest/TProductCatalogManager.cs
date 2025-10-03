using System.Collections.Generic;
using System.Linq;
using E_shop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace E_shopTest
{
    [TestClass]
    public class TProductCatalogManager
    {
        private Mock<IProductRepository> _mockRepository;
        private Mock<IProductValidator> _mockValidator;
        private ProductCatalogManager _catalog;
        private Product _validProduct;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockValidator = new Mock<IProductValidator>();
            _catalog = new ProductCatalogManager(_mockRepository.Object, _mockValidator.Object);

            _validProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Category = "Электроника",
                Stock = 10,
                Unit = "шт"
            };
        }

        [TestMethod]
        public void TestAddProductWithValidData()
        {
            // Arrange
            string errorMessage;
            _mockValidator.Setup(v => v.Validate(_validProduct, out errorMessage))
                         .Returns(true);
            _mockRepository.Setup(r => r.ArticleExists("12345")).Returns(false);

            // Act
            var result = _catalog.AddProduct(_validProduct);

            // Assert
            Assert.IsTrue(result);
            _mockRepository.Verify(r => r.AddProduct(_validProduct), Times.Once);
        }

        [TestMethod]
        public void TestAddProductWithInvalidName()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "", // Пустое наименование
                Price = 1000
            };

            string errorMessage = "Наименование товара не может быть пустым";
            _mockValidator.Setup(v => v.Validate(invalidProduct, out errorMessage))
                         .Returns(false);

            _mockRepository.Setup(r => r.GetProductByArticle(nonExistentArticle))
                          .Returns((Product)null);
            _mockRepository.Setup(r => r.GetAllProducts())
                          .Returns(initialProducts);
            // Act
            var result = _catalog.AddProduct(invalidProduct);

            // Assert
            Assert.IsFalse(result);
            _mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithInvalidPrice()
        {
            // Arrange
            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = -1000 // Отрицательная цена
            };

            string errorMessage = "Цена товара должна быть положительной";
            _mockValidator.Setup(v => v.Validate(invalidProduct, out errorMessage))
                         .Returns(false);

            // Act
            var result = _catalog.AddProduct(invalidProduct);

            // Assert
            Assert.IsFalse(result);
            _mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithDuplicateArticle()
        {
            // Arrange
            string errorMessage;
            _mockValidator.Setup(v => v.Validate(_validProduct, out errorMessage))
                         .Returns(true);
            _mockRepository.Setup(r => r.ArticleExists("12345")).Returns(true);

            // Act
            var result = _catalog.AddProduct(_validProduct);

            // Assert
            Assert.IsFalse(result);
            _mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}