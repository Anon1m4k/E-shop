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
            string actualHtml = htmlGenerator.GenerateHtml(saleCheck);

            // Assert
            string expectedHtml = $@"<!DOCTYPE html>
<html lang=""ru"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Чек продажи №1</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Courier New', monospace;
        }}
        
        body {{
            background-color: #f5f5f5;
            padding: 20px;
            display: flex;
            justify-content: center;
        }}
        
        .receipt-container {{
            width: 100%;
            max-width: 400px;
            background-color: white;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            border: 1px solid #ddd;
        }}
        
        .header {{
            text-align: center;
            margin-bottom: 20px;
            padding-bottom: 15px;
            border-bottom: 2px dashed #333;
        }}
        
        .company-name {{
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 5px;
        }}
        
        .receipt-title {{
            font-size: 16px;
            margin-bottom: 10px;
        }}
        
        .receipt-number {{
            font-size: 14px;
            margin-bottom: 5px;
        }}
        
        .info-section {{
            margin-bottom: 15px;
        }}
        
        .info-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 5px;
            font-size: 14px;
        }}
        
        .items-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
            font-size: 12px;
        }}
        
        .items-table th {{
            text-align: left;
            padding: 5px;
            border-bottom: 1px solid #ddd;
            font-weight: bold;
        }}
        
        .items-table td {{
            padding: 5px;
            border-bottom: 1px dashed #eee;
        }}
        
        .qty-col {{
            width: 15%;
            text-align: center;
        }}
        
        .price-col, .total-col {{
            width: 20%;
            text-align: right;
        }}
        
        .item-name {{
            width: 45%;
        }}
        
        .total-section {{
            border-top: 2px dashed #333;
            padding-top: 10px;
            margin-top: 10px;
            text-align: right;
            font-weight: bold;
            font-size: 16px;
        }}
        
        @media print {{
            body {{
                background-color: white;
                padding: 0;
            }}
            
            .receipt-container {{
                box-shadow: none;
                border: none;
                max-width: 100%;
            }}
        }}
    </style>
</head>
<body>
    <div class=""receipt-container"">
        <div class=""header"">
            <div class=""company-name"">МАГАЗИН ""E-Shop""</div>
            <div class=""receipt-title"">КАССОВЫЙ ЧЕК</div>
            <div class=""receipt-number"">№ 000001</div>
        </div>
        
        <div class=""info-section"">
            <div class=""info-row"">
                <span>Дата:</span>
                <span>26.10.2025</span>
            </div>
            <div class=""info-row"">
                <span>Время:</span>
                <span>14:30:25</span>
            </div>
            <div class=""info-row"">
                <span>Клиент:</span>
                <span>Александр И.</span>
            </div>
        </div>
        
        <table class=""items-table"">
            <thead>
                <tr>
                    <th class=""qty-col"">Кол-во</th>
                    <th class=""item-name"">Товар</th>
                    <th class=""price-col"">Цена</th>
                    <th class=""total-col"">Сумма</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td class=""qty-col"">2</td>
                    <td class=""item-name"">Смартфон</td>
                    <td class=""price-col"">1 000.00</td>
                    <td class=""total-col"">2 000.00</td>
                </tr>
                <tr>
                    <td class=""qty-col"">1</td>
                    <td class=""item-name"">Чехол</td>
                    <td class=""price-col"">500.00</td>
                    <td class=""total-col"">500.00</td>
                </tr>
            </tbody>
        </table>
        
        <div class=""total-section"">
            <div class=""info-row"">
                <span>Итого:</span>
                <span>2 500.00 руб.</span>
            </div>                       
        </div>
        
        <div class=""barcode"">*000001*</div>
        
        <div class=""thank-you"">СПАСИБО ЗА ПОКУПКУ!</div>              
    </div>
</body>
</html>";

            Assert.AreEqual(expectedHtml, actualHtml);
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
            string result = saleManager.AddSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Продажа успешно сформирована", result);
            mockRepository.Verify(r => r.AddSaleCheck(It.IsAny<SaleCheck>()), Times.Once);
            mockProductRepo.Verify(r => r.UpdateProduct(It.Is<Product>(p => p.Stock == 5)), Times.Once);
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
            string result = saleManager.AddSaleCheck(saleCheck);

            // Assert
            Assert.AreEqual("Продажа успешно сформирована", result);
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
            var mockRepository = new Mock<ISaleCheckRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var saleManager = new SaleCheckManager(mockRepository.Object, mockProductRepo.Object);

            var saleCheck = new SaleCheck
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