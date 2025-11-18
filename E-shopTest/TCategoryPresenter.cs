using E_shop;
using E_shopLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

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
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();

            List<string> expectedCategories = new List<string> { "Электроника", "Одежда", "Книги" };
            mockRepository.Setup(m => m.GetCategories()).Returns(expectedCategories);

            // Act - создание презентера автоматически вызовет LoadCategories()
            var presenter = new SaleCheckPresenter(mockCategoriesView.Object, mockProductsView.Object, mockRepository.Object);

            // Assert - проверяем, что ShowCategories был вызван с правильными данными
            mockCategoriesView.Verify(v => v.ShowCategories(It.Is<List<string>>(categories =>
                categories.Count == 3)), Times.Once);

            // Используем CollectionAssert для проверки содержимого
            mockCategoriesView.Verify(v => v.ShowCategories(It.Is<List<string>>(categories =>
                CollectionAssert.Equals(categories, expectedCategories))), Times.Once);
        }

        [TestMethod]
        [DataRow("Одежда")]
        [DataRow("Электроника")]
        [DataRow("Книги")]
        public void CategorySelected_WhenCategorySelected_ShouldDisplayCorrectProducts(string category)
        {
            // Arrange
            Mock<ICategoriesView> mockCategoriesView = new Mock<ICategoriesView>();
            Mock<IProductsView> mockProductsView = new Mock<IProductsView>();
            Mock<IProductRepository> mockRepository = new Mock<IProductRepository>();

            // Расширенный список товаров с большим количеством книг
            List<Product> allProducts = new List<Product>
            {
                new Product { Article = "1", Name = "Смартфон", Category = "Электроника", Price = 15000, Stock = 10, Unit = "шт" },
                new Product { Article = "2", Name = "Ноутбук", Category = "Электроника", Price = 45000, Stock = 5, Unit = "шт" },
                new Product { Article = "111", Name = "Футболка", Category = "Одежда", Price = 500, Stock = 15, Unit = "шт" },
                new Product { Article = "112", Name = "Джинсы", Category = "Одежда", Price = 1500, Stock = 8, Unit = "шт" },
                new Product { Article = "201", Name = "Роман", Category = "Книги", Price = 400, Stock = 20, Unit = "шт" },
                new Product { Article = "202", Name = "Учебник", Category = "Книги", Price = 1200, Stock = 35, Unit = "шт" },
                new Product { Article = "203", Name = "Детектив", Category = "Книги", Price = 350, Stock = 12, Unit = "шт" },
            };

            mockRepository.Setup(m => m.GetAllProducts()).Returns(allProducts);

            var presenter = new SaleCheckPresenter(mockCategoriesView.Object, mockProductsView.Object, mockRepository.Object);

            // Фильтруем товары по выбранной категории внутри теста
            var expectedProducts = allProducts.Where(p => p.Category == category).ToList();

            // Act
            mockCategoriesView.Raise(v => v.CategorySelected += null, category);

            // Assert - проверяем, что DisplayProducts был вызван с правильным отфильтрованным списком
            mockProductsView.Verify(v => v.DisplayProducts(It.Is<List<Product>>(actualProducts =>
                ProductsAreEqual(actualProducts, expectedProducts)
            )), Times.Once);
        }

        private bool ProductsAreEqual(List<Product> actual, List<Product> expected)
        {
            if (actual.Count != expected.Count)
                return false;

            for (int i = 0; i < actual.Count; i++)
            {
                if (actual[i].Article != expected[i].Article ||
                    actual[i].Name != expected[i].Name ||
                    actual[i].Category != expected[i].Category)
                    return false;
            }
            return true;
        }
    }
}
