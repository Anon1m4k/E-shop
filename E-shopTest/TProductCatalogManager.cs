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

        [TestMethod]
        public void TestDeleteProductByArticle()
        {
            // Arrange
            var _mockRepository = new Mock<IProductRepository>();
            var _catalog = new ProductCatalogManager(_mockRepository.Object);

            var articleToRemove = "12345";
            var existingProduct = new Product { Article = articleToRemove, Name = "Test Product" };

            _mockRepository.Setup(r => r.GetProductByArticle(articleToRemove))
                          .Returns(existingProduct);
            _mockRepository.Setup(r => r.DeleteProduct(articleToRemove))
                          .Returns(string.Empty);

            // Act
            var result = _catalog.DeleteProduct(articleToRemove);

            // Assert
            Assert.AreEqual(string.Empty, result, "Успешное удаление должно возвращать пустую строку");
            _mockRepository.Verify(r => r.DeleteProduct(articleToRemove), Times.Once);
        }

        [TestMethod]
        public void TestRemoveProductByNotFoundArticle()
        {
            // Arrange

            var _mockRepository = new Mock<IProductRepository>();
            var _catalog = new ProductCatalogManager(_mockRepository.Object);

            var nonExistentArticle = "999";

            var initialProducts = new List<Product>
            {
            new Product { Article = "123", Name = "Товар 1" },
            new Product { Article = "456", Name = "Товар 2" }
            };

            var expectedProducts = new List<Product>
            {
            new Product { Article = "123", Name = "Товар 1" },
            new Product { Article = "456", Name = "Товар 2" }
            };

            _mockRepository.Setup(r => r.GetProductByArticle(nonExistentArticle))
                          .Returns((Product)null);
            _mockRepository.Setup(r => r.GetAllProducts())
                          .Returns(initialProducts);
            // Act
            var result = _catalog.DeleteProduct(nonExistentArticle);

            // Assert
            Assert.AreEqual("Товар с указанным артикулом не найден", result,
            "Для несуществующего товара должно возвращаться информационное сообщение");
            _mockRepository.Verify(r => r.DeleteProduct(nonExistentArticle), Times.Never);

            var productsCount = _mockRepository.Object.GetAllProducts().Count;
            Assert.AreEqual(2, productsCount, "Количество товаров не должно измениться");

            var actualProducts = _mockRepository.Object.GetAllProducts().ToList();

            // Сравниваем с ожидаемым списком
            CollectionAssert.AreEqual(initialProducts, actualProducts, "Списки товаров должны полностью совпадать после попытки удаления несуществующего товара");

        }

        [TestMethod]
        public void TestRemoveProductByArticleWithEmptyCatalog()
        {
            // Arrange
            var _mockRepository = new Mock<IProductRepository>();
            var _catalog = new ProductCatalogManager(_mockRepository.Object);

            var articleToRemove = "1";

            _mockRepository.Setup(r => r.GetProductByArticle(articleToRemove))
                          .Returns((Product)null);
            _mockRepository.Setup(r => r.GetAllProducts())
                          .Returns(new List<Product>());

            // Act
            var result = _catalog.DeleteProduct(articleToRemove);

            // Assert
            Assert.AreEqual("Товар с указанным артикулом не найден", result,
                "При удалении из пустого каталога должно возвращаться сообщение 'не найден'");

            _mockRepository.Verify(r => r.DeleteProduct(articleToRemove), Times.Never);

            var products = _mockRepository.Object.GetAllProducts();
            Assert.AreEqual(0, products.Count, "Каталог должен оставаться пустым");
        }
    }
}