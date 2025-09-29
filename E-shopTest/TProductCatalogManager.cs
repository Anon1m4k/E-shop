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
        private ProductCatalogManager _catalog;

        [TestMethod]
        public void TestAddProductWithValidData()
        {
            // Arrange
            var mockRepository = new Mock<IProductRepository>();
            var mockValidator = new Mock<IProductValidator>();
            var catalog = new ProductCatalogManager(mockRepository.Object, mockValidator.Object);

            var validProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000
            };

            mockValidator.Setup(v => v.Validate(validProduct)).Returns(true);
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
            var mockValidator = new Mock<IProductValidator>();
            var catalog = new ProductCatalogManager(mockRepository.Object, mockValidator.Object);

            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "", // Пустое наименование
                Price = 1000
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
            var mockValidator = new Mock<IProductValidator>();
            var catalog = new ProductCatalogManager(mockRepository.Object, mockValidator.Object);

            var invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = -1000 // Отрицательная цена
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
            var mockValidator = new Mock<IProductValidator>();
            var catalog = new ProductCatalogManager(mockRepository.Object, mockValidator.Object);

            var newProduct = new Product
            {
                Article = "12345",
                Name = "Ноутбук",
                Price = 5000
            };

            mockValidator.Setup(v => v.Validate(newProduct)).Returns(true);
            mockRepository.Setup(r => r.ArticleExists("12345")).Returns(true);

            // Act
            var result = catalog.AddProduct(newProduct);

            // Assert
            Assert.AreEqual("Товар с артикулом '12345' уже существует", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestDeleteProductByArticle()
        {
            // Arrange
            _mockRepository = new Mock<IProductRepository>();
            _catalog = new ProductCatalogManager(_mockRepository.Object, null);

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

            _mockRepository = new Mock<IProductRepository>();
            _catalog = new ProductCatalogManager(_mockRepository.Object, null);

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
            CollectionAssert.AreEqual(
                expectedProducts.Select(p => p.Article).ToList(),
                actualProducts.Select(p => p.Article).ToList(),
                "Списки товаров должны совпадать после попытки удаления несуществующего товара");     
        }

        [TestMethod]
        public void TestRemoveProductByArticleWithEmptyCatalog()
        {
            // Arrange
            _mockRepository = new Mock<IProductRepository>();
            _catalog = new ProductCatalogManager(_mockRepository.Object, null);

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