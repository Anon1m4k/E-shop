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
        [DataRow(1, "12345", "Смартфон", "Техника", 1000.0, 10, "шт")]
        [DataRow(2, "22", "Мышка компьютерная", "Техника", 1000.0, 10, "шт",
           "33", "Коврик для мышки", "Аксессуары", 500.0, 5, "шт")]
        public void TestAddInvoiceWithValidData(int id, string article1, string name1, string category1, double price1, int stock1, string unit1,
                                         string article2 = null, string name2 = null, string category2 = null, double price2 = 0, int stock2 = 0, string unit2 = null)
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var validInvoice = new Invoice(id);
            validInvoice.Date = new DateTime(2025, 10, 26);
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

            mockRepository.Setup(r => r.AddInvoice(validInvoice))
                         .Returns("Приходная накладная успешно добавлена");

            string result = manager.AddInvoice(validInvoice);

            Assert.AreEqual("Приходная накладная успешно добавлена", result);
            mockRepository.Verify(r => r.AddInvoice(validInvoice), Times.Once);
        }
        [TestMethod]
        [DataRow("123", "Компьютер", "Техника", "шт", -1000.0, 10, "Цена товара должна быть положительной")] // невалидная цена
        [DataRow("12", "Монитор", "Техника", "шт", 1000.0, -10, "Количество товара не может быть отрицательным")] // невалидное количество 
        [DataRow("", "Мышка", "Техника", "шт", 1000.0, 10, "Артикул товара не может быть пустым")] // отсутствие артикула
        [DataRow("567", "", "Техника", "шт", 1000.0, 10, "Наименование товара не может быть пустым")] // отсутствие наименования
        [DataRow("568", "Клавиатура", "", "шт", 1000.0, 10, "Категория должна быть заполнена")] // отсутствие категории
        [DataRow("657", "Ноутбук", "Техника", "", 1000.0, 10, "Единица измерения должна быть заполнена")] // отсутствие единицы измерения
        public void TestAddInvoiceWithInvalidData(string article, string name, string category, string unit, double price, int stock, string expectedErrorMessage)
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice(1);
            invalidInvoice.Date = new DateTime(2025, 10, 26);
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

            string result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual(expectedErrorMessage, result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }

    }
}
