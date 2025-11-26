using E_shopLib;
using E_shopLib1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
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
            SaleCheckHtmlGenerator htmlGenerator = new SaleCheckHtmlGenerator();

            SaleCheck saleCheck = new SaleCheck
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
            string actualHtml = htmlGenerator.GenerateHtml(saleCheck);

            // Assert
            string expectedHtml = ReadExpectedHtmlFromFile();
            Assert.AreEqual(expectedHtml, actualHtml);
        }

        private string ReadExpectedHtmlFromFile()
        {
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\ExpectedSaleCheckHtml.html"));            
            return File.ReadAllText(filePath);
        }

        [TestMethod]
        public void CreateSaleCheck_ValidData_SuccessfullyCreated()
        {
            // Arrange
            Mock<ISaleCheckRepository> mockRepository = new Mock<ISaleCheckRepository>();
            Mock<IProductRepository> mockProductRepo = new Mock<IProductRepository>();
            SaleCheckManager saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            SaleCheck saleCheck = new SaleCheck
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
                .Returns("");

            // Act
            string result = saleManager.AddSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Once);
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Stock == 5)), Times.Once);
        }
       
        [TestMethod]
        public void CreateSaleCheck_MultipleItems_SuccessfullyCreated()
        {
            // Arrange
            Mock<ISaleCheckRepository> mockRepository = new Mock<ISaleCheckRepository>();
            Mock<IProductRepository> mockProductRepo = new Mock<IProductRepository>();
            SaleCheckManager saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            SaleCheck saleCheck = new SaleCheck
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
                .Returns("");

            // Act
            string result = saleManager.AddSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Once);

            // Проверяем, что остатки обновлены правильно
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Article == "22" && p.Stock == 7)), Times.Once);
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Article == "33" && p.Stock == 5)), Times.Once);
        }

        [TestMethod]
        [DataRow("", 5, "12345", 10, "Смартфон", "Клиент не может быть пустым")] // Пустой клиент
        [DataRow("Александр И.", -10, "12", 8, "Монитор", "Количество товара не может быть отрицательным")] // Отрицательное количество
        [DataRow("Александр И.", 15, "999", 8, "Планшет", "Количество товара 'Планшет' превышает остаток на складе. Доступно: 8")] // Превышение остатка
        public void CreateSaleCheck_InvalidData_ReturnsError(string client, int quantity, string article, int productStock, string productName, string expectedError)
        {
            // Arrange
            Mock<ISaleCheckRepository> mockRepository = new Mock<ISaleCheckRepository>();
            Mock<IProductRepository> mockProductRepo = new Mock<IProductRepository>();
            SaleCheckManager saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            SaleCheck saleCheck = new SaleCheck
            {
                IdCheck = 1,
                Date = new DateTime(2025, 10, 26),
                Client = client,
                Items = new List<InvoiceItem>
        {
            new InvoiceItem
            {
                Article = article,
                Name = productName,
                Category = "Техника",
                Price = 1000,
                Quantity = quantity,
                Unit = "шт"
            }
        }
            };

            // Настройка мока для продукта (если количество положительное - проверяем остаток)
            if (quantity > 0)
            {
                mockProductRepo.Setup(r => r.GetProductByArticle(article))
                    .Returns(new Product { Article = article, Stock = productStock, Name = productName });
            }

            // Act
            string result = saleManager.AddSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual(expectedError, result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Never);
            mockProductRepo.Verify(r => r.UpdateProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}