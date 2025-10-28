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

    }
}
