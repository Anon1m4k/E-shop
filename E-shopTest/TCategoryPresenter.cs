using E_shop;
using E_shopLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace E_shopTest
{
    [TestClass]
    public class TCategoryPresenter
    {
        [TestMethod]
        public void LoadCategories_WhenCalled_ShouldReturnThreeCategories()
        {
            // Arrange
            Mock<ICategoriesView> mockCategoriesView = new Mock<ICategoriesView>();
            Mock<IProductsView> mockProductsView = new Mock<IProductsView>();

            List<string> expectedCategories = new List<string> { "Электроника", "Одежда", "Книги" };

            // Act
            SaleCheckPresenter presenter = new SaleCheckPresenter(mockCategoriesView.Object, mockProductsView.Object);

            // Assert
            mockCategoriesView.Verify(v => v.ShowCategories(It.Is<List<string>>(cats =>
                cats.Count == 3 &&
                cats.Contains("Электроника") &&
                cats.Contains("Одежда") &&
                cats.Contains("Книги"))), Times.Once);
        }

        [TestMethod]
        [DataRow("Одежда", new string[] { "111", "112" }, new string[] { "Футболка", "Джинсы" })]
        [DataRow("Электроника", new string[] { "1", "2" }, new string[] { "Смартфон", "Ноутбук" })]
        [DataRow("Книги", new string[] { "111", "222" }, new string[] { "Роман", "Учебник" })]
        public void CategorySelected_WhenCategorySelected_ShouldDisplayCorrectProducts(string category, string[] expectedArticles, string[] expectedNames)
        {
            // Arrange
            Mock<ICategoriesView> mockCategoriesView = new Mock<ICategoriesView>();
            Mock<IProductsView> mockProductsView = new Mock<IProductsView>();
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();
            Mock<ProductCatalogManager> mockCatalogManager = new Mock<ProductCatalogManager>(mockRepository.Object);

            List<Product> allProducts = new List<Product>
            {
                new Product { Article = "1", Name = "Смартфон", Category = "Электроника", Price = 15000, Stock = 10, Unit = "шт" },
                new Product { Article = "2", Name = "Ноутбук", Category = "Электроника", Price = 45000, Stock = 5, Unit = "шт" },
                new Product { Article = "111", Name = "Футболка", Category = "Одежда", Price = 500, Stock = 15, Unit = "шт" },
                new Product { Article = "112", Name = "Джинсы", Category = "Одежда", Price = 1500, Stock = 8, Unit = "шт" },
                new Product { Article = "111", Name = "Роман", Category = "Книги", Price = 400, Stock = 20, Unit = "шт" },
                new Product { Article = "222", Name = "Учебник", Category = "Книги", Price = 1200, Stock = 35, Unit = "шт" }
            };

            mockCatalogManager.Setup(m => m.GetAllProducts()).Returns(allProducts);

            SaleCheckPresenter presenter = new SaleCheckPresenter(mockCategoriesView.Object, mockProductsView.Object);

            // Act
            mockCategoriesView.Raise(v => v.CategorySelected += null, category);

            // Assert
            mockProductsView.Verify(v => v.DisplayProducts(It.Is<List<Product>>(products =>
                products.Count == expectedArticles.Length &&
                CheckProducts(products, expectedArticles, expectedNames))), Times.Once);
        }

        private bool CheckProducts(List<Product> products, string[] expectedArticles, string[] expectedNames)
        {
            for (int i = 0; i < expectedArticles.Length; i++)
            {
                if (products[i].Article != expectedArticles[i] || products[i].Name != expectedNames[i])
                    return false;
            }
            return true;
        }
    }
}
