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
        public void TestDeleteProductByArticle()
        {
            // Arrange
           var _mockRepository = new Mock<IProductRepository>();
           var _catalog = new ProductCatalogManager(_mockRepository.Object, null);

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
           var _catalog = new ProductCatalogManager(_mockRepository.Object, null);

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
            CollectionAssert.AreEqual( initialProducts, actualProducts,"Списки товаров должны полностью совпадать после попытки удаления несуществующего товара");

        }

        [TestMethod]
        public void TestRemoveProductByArticleWithEmptyCatalog()
        {
            // Arrange
           var _mockRepository = new Mock<IProductRepository>();
           var _catalog = new ProductCatalogManager(_mockRepository.Object, null);

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