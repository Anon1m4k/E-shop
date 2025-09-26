using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using E_shop;
using System.Collections.Generic;

namespace E_shopTest
{
    [TestClass]
    public class TProductCatalogManager
    {
        private Mock<IProductRepository> _mockRepository;
        private Mock<IProductValidator> _mockValidator;
        private ProductCatalogManager _catalog;
        private Product _validProduct;
        private List<Product> _mockProducts;

        /*[TestInitialize]
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
        }*/


        [TestInitialize]
        public void TestInitialize()
        {
            _mockRepository = new Mock<IProductRepository>();
            _mockValidator = new Mock<IProductValidator>();
            _catalog = new ProductCatalogManager(_mockRepository.Object, _mockValidator.Object);

            // Создаем список товаров для имитации базы данных
            _mockProducts = new List<Product>
            {
                new Product { Article = "12345", Name = "Смартфон", Price = 1000 },
                new Product { Article = "67890", Name = "Ноутбук", Price = 2000 }
            };

            _validProduct = _mockProducts[0]; 

            _mockRepository.Setup(r => r.GetProductByArticle(It.IsAny<string>()))
                .Returns<string>(article => _mockProducts.Find(p => p.Article == article));

            _mockRepository.Setup(r => r.ArticleExists(It.IsAny<string>()))
                .Returns<string>(article => _mockProducts.Exists(p => p.Article == article));

            _mockRepository.Setup(r => r.DeleteProduct(It.IsAny<string>()))
                .Returns<string>(article =>
                {
                    var product = _mockProducts.Find(p => p.Article == article);
                    if (product != null)
                    {
                        _mockProducts.Remove(product);
                        return true;
                    }
                    return false;
                });

            _mockRepository.Setup(r => r.AddProduct(It.IsAny<Product>()))
                .Callback<Product>(product => _mockProducts.Add(product));

            _mockRepository.Setup(r => r.GetAllProducts())
                .Returns(_mockProducts);
        }

        [TestMethod]
        public void TestAddProductWithValidData()
        {
            // Arrange
            _mockValidator.Setup(v => v.Validate(_validProduct))
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

            _mockValidator.Setup(v => v.Validate(invalidProduct))
                         .Returns(false);

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

            _mockValidator.Setup(v => v.Validate(invalidProduct))
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
            _mockValidator.Setup(v => v.Validate(_validProduct))
                         .Returns(true);
            _mockRepository.Setup(r => r.ArticleExists("12345")).Returns(true);

            // Act
            var result = _catalog.AddProduct(_validProduct);

            // Assert
            Assert.IsFalse(result);
            _mockRepository.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }

        //##########################################

        [TestMethod]
        public void TestDeleteProductByArticle()
        {
            // Arrange
            var articleToRemove = "12345"; 

            // Act
            var result = _catalog.DeleteProduct(articleToRemove);

            // Assert
            Assert.IsTrue(result, "Удаление существующего товара должно быть успешным");
            _mockRepository.Verify(r => r.DeleteProduct(articleToRemove), Times.Once);

            // Проверяем, что товар действительно удален
            var productAfterDeletion = _mockRepository.Object.GetProductByArticle(articleToRemove);
            Assert.IsNull(productAfterDeletion, "Товар должен быть удален из репозитория");
        }

        [TestMethod]
        public void TestRemoveProductByNotFoundArticle()
        {
            // Arrange
            var nonExistentArticle = "999"; 

            // Act
            var result = _catalog.DeleteProduct(nonExistentArticle);

            // Assert
            Assert.IsFalse(result, "Удаление несуществующего товара должно завершаться неудачей");
            _mockRepository.Verify(r => r.DeleteProduct(nonExistentArticle), Times.Once);

            // Проверяем, что список товаров не изменился
            var productsCount = _mockRepository.Object.GetAllProducts().Count;
            Assert.AreEqual(2, productsCount, "Количество товаров не должно измениться");
        }

        [TestMethod]
        public void TestRemoveProductByArticleWithEmptyCatalog()
        {
            // Arrange
            var articleToRemove = "1";

            // Очищаем список товаров
            _mockProducts.Clear();

            // Act
            var result = _catalog.DeleteProduct(articleToRemove);

            // Assert
            Assert.IsFalse(result, "Удаление из пустого каталога должно завершаться неудачей");
            _mockRepository.Verify(r => r.DeleteProduct(articleToRemove), Times.Once);

            // Проверяем, что список остается пустым
            var productsCount = _mockRepository.Object.GetAllProducts().Count;
            Assert.AreEqual(0, productsCount, "Каталог должен оставаться пустым");
        }
    }
}