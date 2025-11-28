using E_shopLib;
using E_shopLib1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopTest
{
    [TestClass]

    public class TInvoiceManager
    {
        [TestMethod]
        [DataRow("12345", "Смартфон", "Техника", 1000.0, 10, "шт")]
        [DataRow("22", "Мышка компьютерная", "Техника", 1000.0, 10, "шт",
                   "33", "Коврик для мышки", "Аксессуары", 500.0, 5, "шт")]
        public void TestAddInvoiceWithValidData(string article1, string name1, string category1, double price1, int stock1, string unit1,
                                                 string article2 = null, string name2 = null, string category2 = null, double price2 = 0, int stock2 = 0, string unit2 = null)
        {
            // Arrange
            Mock<IInvoiceRepository> mockRepository = new Mock<IInvoiceRepository>();
            InvoiceManager manager = new InvoiceManager(mockRepository.Object);

            Invoice validInvoice = new Invoice();
            validInvoice.Date = DateTime.Now.Date;
            validInvoice.Items = new List<Product>
            {
                new Product
                {
                    Article = article1,
                    Name = name1,
                    Category = category1,
                    Price = (decimal)price1,
                    Stock = stock1,
                    Unit = unit1
                }
            };

            if (article2 != null)
            {
                validInvoice.Items.Add(new Product
                {
                    Article = article2,
                    Name = name2,
                    Category = category2,
                    Price = (decimal)price2,
                    Stock = stock2,
                    Unit = unit2
                });
            }

            // Настраиваем мок для успешного добавления
            mockRepository.Setup(r => r.AddInvoice(It.IsAny<Invoice>()))
                         .Returns(string.Empty); // Репозиторий возвращает пустую строку при успехе

            // Act
            string result = manager.AddInvoice(validInvoice);

            // Assert
            Assert.AreEqual(string.Empty, result, "При успешном добавлении должна возвращаться пустая строка");
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Once);
        }

        [TestMethod]
        [DataRow("123", "Компьютер", "Техника", "шт", -1000.0, 10, "Цена товара '123' должна быть больше 0")] // невалидная цена
        [DataRow("12", "Монитор", "Техника", "шт", 1000.0, -10, "Количество товара '12' должно быть больше 0")] // невалидное количество 
        [DataRow("", "Мышка", "Техника", "шт", 1000.0, 10, "Артикул товара не может быть пустым")] // отсутствие артикула
        [DataRow("567", "", "Техника", "шт", 1000.0, 10, "Наименование товара с артикулом '567' не может быть пустым")] // отсутствие наименования
        [DataRow("568", "Клавиатура", "", "шт", 1000.0, 10, "Категория не может быть пустой")] // отсутствие категории
        [DataRow("657", "Ноутбук", "Техника", "", 1000.0, 10, "Единица измерения не может быть пустой")] // отсутствие единицы измерения
        public void TestAddInvoiceWithInvalidData(string article, string name, string category, string unit, double price, int stock, string expectedErrorMessage)
        {
            // Arrange
            Mock<IInvoiceRepository> mockRepository = new Mock<IInvoiceRepository>();
            InvoiceManager manager = new InvoiceManager(mockRepository.Object);

            Invoice invalidInvoice = new Invoice();
            invalidInvoice.Date = DateTime.Now.Date;
            invalidInvoice.Items = new List<Product>
            {
                new Product
                {
                    Article = article,
                    Name = name,
                    Category = category,
                    Price = (decimal)price,
                    Stock = stock,
                    Unit = unit
                }
            };

            // Act
            string result = manager.AddInvoice(invalidInvoice);

            // Assert
            Assert.AreEqual(expectedErrorMessage, result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
    }
}
