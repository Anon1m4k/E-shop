using E_shopLib;
using E_shopLib1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace E_shopTest
{
    [TestClass]
    public class TSaleCheckManager
    {
        [TestMethod]
        public void GenerateHtmlForSaleCheck_ValidData_CreatesCorrectHtml()
        {
            // Arrange
            var htmlGenerator = new SaleCheckHtmlGenerator();

            var saleCheck = new SaleCheck
            {
                IdCheck = 1,
                Date = new DateTime(2025, 10, 26, 14, 30, 25),
                Client = "Александр И.",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem { Article = "12345", Name = "Смартфон", Quantity = 2, Price = 1000 },
                    new InvoiceItem { Article = "67890", Name = "Чехол", Quantity = 1, Price = 500 }
                }
            };

            // Act
            string html = htmlGenerator.GenerateHtml(saleCheck);

            // Assert - проверяем структуру HTML и наличие всех данных
            Assert.IsFalse(string.IsNullOrEmpty(html));
            Assert.IsTrue(html.Contains("<!DOCTYPE html>"));
            Assert.IsTrue(html.Contains("<html lang=\"ru\">"));
            Assert.IsTrue(html.Contains("</html>"));

            // Проверяем заголовок и номер чека
            Assert.IsTrue(html.Contains("Чек продажи №1"));
            Assert.IsTrue(html.Contains("№ 000001"));

            // Проверяем данные чека
            Assert.IsTrue(html.Contains("26.10.2025"));
            Assert.IsTrue(html.Contains("14:30:25"));
            Assert.IsTrue(html.Contains("Александр И."));

            // Проверяем товары
            Assert.IsTrue(html.Contains("Смартфон"));
            Assert.IsTrue(html.Contains("Чехол"));

            // Проверяем общую структуру таблицы
            Assert.IsTrue(html.Contains("items-table"));
            Assert.IsTrue(html.Contains("Кол-во"));
            Assert.IsTrue(html.Contains("Товар"));
            Assert.IsTrue(html.Contains("Цена"));
            Assert.IsTrue(html.Contains("Сумма"));
        }

        [TestMethod]
        public void CreateSaleCheck_ValidData_SuccessfullyCreated()
        {
            // Arrange
            var mockRepository = new Mock<ISaleCheckRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            var saleCheck = new SaleCheck
            {
                IdCheck = 1,
                Date = new DateTime(2025, 10, 26),
                Client = "Александр И.",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        Article = "12345",
                        Name = "Смартфон",
                        Category = "Техника",
                        Price = 1000,
                        Quantity = 5,
                        Unit = "шт"
                    }
                }
            };

            // Настройка мока для продукта
            mockProductRepo.Setup(r => r.GetProductByArticle("12345"))
                .Returns(new Product { Article = "12345", Stock = 10, Name = "Смартфон" });
            mockRepository.Setup(r => r.AddSaleCheck(It.IsAny<SaleCheck>()))
                .Returns("Продажа успешно сформирована");

            // Act
            string result = saleManager.CreateSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Продажа успешно сформирована", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Once);
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Stock == 5)), Times.Once);
        }

        [TestMethod]
        public void CreateSaleCheck_NegativeQuantity_ReturnsError()
        {
            // Arrange
            var mockRepository = new Mock<ISaleCheckRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            var saleCheck = new SaleCheck
            {
                IdCheck = 3,
                Date = new DateTime(2025, 10, 26),
                Client = "Александр И.",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        Article = "12",
                        Name = "Монитор",
                        Category = "Техника",
                        Price = 1000,
                        Quantity = -10,
                        Unit = "шт"
                    }
                }
            };

            mockProductRepo.Setup(r => r.GetProductByArticle("12"))
                .Returns(new Product { Article = "12", Stock = 8, Name = "Монитор" });

            // Act
            string result = saleManager.CreateSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Количество товара не может быть отрицательным", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Never);
            mockProductRepo.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void CreateSaleCheck_EmptyClient_ReturnsError()
        {
            // Arrange
            var mockRepository = new Mock<ISaleCheckRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            var saleCheck = new SaleCheck
            {
                IdCheck = 1,
                Date = new DateTime(2025, 10, 26),
                Client = "",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        Article = "12345",
                        Name = "Смартфон",
                        Category = "Техника",
                        Price = 1000,
                        Quantity = 5,
                        Unit = "шт"
                    }
                }
            };

            mockProductRepo.Setup(r => r.GetProductByArticle("12345"))
                .Returns(new Product { Article = "12345", Stock = 10, Name = "Смартфон" });

            // Act
            string result = saleManager.CreateSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Клиент не может быть пустым", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Never);
            mockProductRepo.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }

        [TestMethod]
        public void CreateSaleCheck_MultipleItems_SuccessfullyCreated()
        {
            // Arrange
            var mockRepository = new Mock<ISaleCheckRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            var saleCheck = new SaleCheck
            {
                IdCheck = 5,
                Date = new DateTime(2025, 10, 26),
                Client = "Александр И.",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        Article = "22",
                        Name = "Мышка компьютерная",
                        Category = "Техника",
                        Price = 1000,
                        Quantity = 3,
                        Unit = "шт"
                    },
                    new InvoiceItem
                    {
                        Article = "33",
                        Name = "Коврик для мышки",
                        Category = "Аксессуары",
                        Price = 500,
                        Quantity = 2,
                        Unit = "шт"
                    }
                }
            };

            // Настройка моков для продуктов
            mockProductRepo.Setup(r => r.GetProductByArticle("22"))
                .Returns(new Product { Article = "22", Stock = 10, Name = "Мышка компьютерная" });
            mockProductRepo.Setup(r => r.GetProductByArticle("33"))
                .Returns(new Product { Article = "33", Stock = 7, Name = "Коврик для мышки" });
            mockRepository.Setup(r => r.AddSaleCheck(It.IsAny<SaleCheck>()))
                .Returns("Продажа успешно сформирована");

            // Act
            string result = saleManager.CreateSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Продажа успешно сформирована", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Once);

            // Проверяем, что остатки обновлены правильно
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Article == "22" && p.Stock == 7)), Times.Once);
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Article == "33" && p.Stock == 5)), Times.Once);
        }

        [TestMethod]
        public void CreateSaleCheck_QuantityExceedsStock_ReturnsError()
        {
            // Arrange
            var mockRepository = new Mock<ISaleCheckRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            var saleCheck = new SaleCheck
            {
                IdCheck = 9,
                Date = new DateTime(2025, 10, 26),
                Client = "Александр И.",
                Items = new List<InvoiceItem>
                {
                    new InvoiceItem
                    {
                        Article = "999",
                        Name = "Планшет",
                        Category = "Техника",
                        Price = 1500,
                        Quantity = 15,
                        Unit = "шт"
                    }
                }
            };

            mockProductRepo.Setup(r => r.GetProductByArticle("999"))
                .Returns(new Product { Article = "999", Stock = 8, Name = "Планшет" });

            // Act
            string result = saleManager.CreateSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Количество товара 'Планшет' превышает остаток на складе. Доступно: 8", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Never);
            mockProductRepo.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}