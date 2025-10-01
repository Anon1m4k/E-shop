using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E_shop;

namespace E_shopTest
{
    [TestClass]
    public class TProductCatalogManager
    {
        [TestMethod]
        public void TestAddProductWithValidData()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var validProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            };

            mockRepository.Setup(r => r.ArticleExists("12345")).Returns(false);

            // Act
            var result = catalog.AddProduct(validProduct);

            // Assert
            Assert.AreEqual(string.Empty, result, "При успешном добавлении должна возвращаться пустая строка");
            mockRepository.Verify(r => r.AddProduct(validProduct), Times.Once);
        }

        [TestMethod]
        public void TestAddProductWithInvalidName()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "", // Пустое наименование
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            };

            // Act
            var result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Наименование товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithInvalidPrice()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = -1000, // Отрицательная цена
                Stock = 10,
                Unit = "шт"
            };

            // Act
            var result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Цена товара должна быть положительной", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithDuplicateArticle()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var newProduct = new Product
            {
                Article = "12345",
                Name = "Ноутбук",
                Price = 5000,
                Stock = 5,
                Unit = "шт"
            };

            mockRepository.Setup(r => r.ArticleExists("12345")).Returns(true);

            // Act
            var result = catalog.AddProduct(newProduct);

            // Assert
            Assert.AreEqual("Товар с артикулом '12345' уже существует", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithEmptyArticle()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var invalidProduct = new Product
            {
                Article = "", // Пустой артикул
                Name = "Смартфон",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            };

            // Act
            var result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Артикул товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithNegativeStock()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Stock = -5, // Отрицательный остаток
                Unit = "шт"
            };

            // Act
            var result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Количество товара не может быть отрицательным", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithEmptyUnit()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var catalog = new ProductCatalogManager(mockRepository.Object);

            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Stock = 10,
                Unit = "" // Пустая единица измерения
            };

            // Act
            var result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Единица измерения не может быть пустой", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}