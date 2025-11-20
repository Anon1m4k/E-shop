using System.Collections.Generic;
using System.Linq;
using E_shop;
using E_shopLib;
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
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product validProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            };

            mockRepository.Setup(r => r.ArticleExists("12345")).Returns(false);

            // Act
            string result = catalog.AddProduct(validProduct);

            // Assert
            Assert.AreEqual(string.Empty, result, "При успешном добавлении должна возвращаться пустая строка");
            mockRepository.Verify(r => r.AddProduct(validProduct), Times.Once);
        }

        [TestMethod]
        public void TestAddProductWithInvalidName()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product invalidProduct = new Product
            {
                Article = "12345",
                Name = "", // Пустое наименование
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            };

            // Act
            string result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Наименование товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithInvalidPrice()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = -1000, // Отрицательная цена
                Stock = 10,
                Unit = "шт"
            };

            // Act
            string result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Цена товара должна быть положительной", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithDuplicateArticle()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product newProduct = new Product
            {
                Article = "12345",
                Name = "Ноутбук",
                Price = 5000,
                Stock = 5,
                Unit = "шт"
            };

            mockRepository.Setup(r => r.ArticleExists("12345")).Returns(true);

            // Act
            string result = catalog.AddProduct(newProduct);

            // Assert
            Assert.AreEqual("Товар с артикулом '12345' уже существует", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithEmptyArticle()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product invalidProduct = new Product
            {
                Article = "", // Пустой артикул
                Name = "Смартфон",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            };

            // Act
            string result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Артикул товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithNegativeStock()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Stock = -5, // Отрицательный остаток
                Unit = "шт"
            };

            // Act
            string result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Количество товара не может быть отрицательным", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestAddProductWithEmptyUnit()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product invalidProduct = new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Price = 1000,
                Stock = 10,
                Unit = "" // Пустая единица измерения
            };

            // Act
            string result = catalog.AddProduct(invalidProduct);

            // Assert
            Assert.AreEqual("Единица измерения не может быть пустой", result);
            mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void TestDeleteProductByArticle()
        {
            // Arrange
            Mock<IProductRepository> _mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager _catalog = new ProductCatalogManager(_mockRepository.Object);

            string articleToRemove = "12345";
            Product existingProduct = new Product { Article = articleToRemove, Name = "Test Product" };

            _mockRepository.Setup(r => r.GetProductByArticle(articleToRemove))
                          .Returns(existingProduct);
            _mockRepository.Setup(r => r.DeleteProduct(articleToRemove))
                          .Returns(string.Empty);

            // Act
            string result = _catalog.DeleteProduct(articleToRemove);

            // Assert
            Assert.AreEqual(string.Empty, result, "Успешное удаление должно возвращать пустую строку");
            _mockRepository.Verify(r => r.DeleteProduct(articleToRemove), Times.Once);
        }

        [TestMethod]
        public void TestRemoveProductByNotFoundArticle()
        {
            // Arrange

            Mock<IProductRepository> _mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager _catalog = new ProductCatalogManager(_mockRepository.Object);

            string nonExistentArticle = "999";

            List<Product> initialProducts = new List<Product>
            {
            new Product { Article = "123", Name = "Товар 1" },
            new Product { Article = "456", Name = "Товар 2" }
            };

            List<Product> expectedProducts = new List<Product>
            {
            new Product { Article = "123", Name = "Товар 1" },
            new Product { Article = "456", Name = "Товар 2" }
            };

            _mockRepository.Setup(r => r.GetProductByArticle(nonExistentArticle))
                          .Returns((Product)null);
            _mockRepository.Setup(r => r.GetAllProducts())
                          .Returns(initialProducts);
            // Act
            string result = _catalog.DeleteProduct(nonExistentArticle);

            // Assert
            Assert.AreEqual("Товар с указанным артикулом не найден", result,
            "Для несуществующего товара должно возвращаться информационное сообщение");
            _mockRepository.Verify(r => r.DeleteProduct(nonExistentArticle), Times.Never);

            int productsCount = _mockRepository.Object.GetAllProducts().Count;
            Assert.AreEqual(2, productsCount, "Количество товаров не должно измениться");

            List<Product> actualProducts = _mockRepository.Object.GetAllProducts().ToList();

            // Сравниваем с ожидаемым списком
            CollectionAssert.AreEqual(initialProducts, actualProducts, "Списки товаров должны полностью совпадать после попытки удаления несуществующего товара");

        }

        [TestMethod]
        public void TestRemoveProductByArticleWithEmptyCatalog()
        {
            // Arrange
            Mock<IProductRepository> _mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager _catalog = new ProductCatalogManager(_mockRepository.Object);

            string articleToRemove = "1";

            _mockRepository.Setup(r => r.GetProductByArticle(articleToRemove))
                          .Returns((Product)null);
            _mockRepository.Setup(r => r.GetAllProducts())
                          .Returns(new List<Product>());

            // Act
            string result = _catalog.DeleteProduct(articleToRemove);

            // Assert
            Assert.AreEqual("Товар с указанным артикулом не найден", result,
                "При удалении из пустого каталога должно возвращаться сообщение 'не найден'");

            _mockRepository.Verify(r => r.DeleteProduct(articleToRemove), Times.Never);

            List<Product> products = _mockRepository.Object.GetAllProducts();
            Assert.AreEqual(0, products.Count, "Каталог должен оставаться пустым");
        }
        
        [TestMethod]
        public void TestUpdateProductWithValidData()
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product storedProduct = new Product
            {
                Article = "12345",
                Name = "Исходное название",
                Price = 1000,
                Stock = 5,
                Unit = "шт"
            };

            mockRepository.Setup(r => r.UpdateProduct(It.IsAny<Product>()))
                          .Callback<Product>(newProductData => storedProduct = newProductData)
                          .Returns(string.Empty);

            mockRepository.Setup(r => r.GetProductByArticle("12345"))
                          .Returns(() => storedProduct);

            // Act
            Product updateRequest = new Product
            {
                Article = "12345",
                Name = "Обновленное название",
                Price = 1200,
                Stock = 8,
                Unit = "коробка"
            };

            string operationResult = catalog.UpdateProduct(updateRequest);

            // Assert
            Assert.AreEqual(string.Empty, operationResult);

            Product actualProductInDb = mockRepository.Object.GetProductByArticle("12345");

            Assert.AreEqual(updateRequest.Name, actualProductInDb.Name);
            Assert.AreEqual(updateRequest.Price, actualProductInDb.Price);
            Assert.AreEqual(updateRequest.Stock, actualProductInDb.Stock);
            Assert.AreEqual(updateRequest.Unit, actualProductInDb.Unit);
        }

        [TestMethod]
        [DataRow("12345", "", 1000, 10, "шт", "Наименование товара не может быть пустым")]
        [DataRow("12345", "Смартфон", -500, 10, "шт", "Цена товара должна быть положительной")]
        [DataRow("12345", "Смартфон", 1000, -5, "шт", "Количество товара не может быть отрицательным")]
        [DataRow("12345", "Смартфон", 1000, 10, "", "Единица измерения не может быть пустой")]
        public void TestUpdateProduct_WithInvalidData(string article, string name, int price, int stock, string unit, string expectedError)
        {
            // Arrange
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            ProductCatalogManager catalog = new ProductCatalogManager(mockRepository.Object);

            Product invalidProduct = new Product
            {
                Article = article,
                Name = name,
                Price = price,
                Stock = stock,
                Unit = unit
            };

            // Act
            string result = catalog.UpdateProduct(invalidProduct);

            // Assert
            Assert.AreEqual(expectedError, result);
            mockRepository.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}