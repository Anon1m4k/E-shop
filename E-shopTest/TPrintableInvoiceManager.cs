using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_shopLib1;

namespace E_shopTest
{
    [TestClass]

    public class TPrintableInvoiceManager
    {

          private readonly PrintableInvoiceCreator _creator = new PrintableInvoiceCreator();

           [TestMethod]
            public void Test_SingleItemInvoice_ShouldMatchExpectedHtml()
            {
                var invoice = new Invoice
                {
                    SerialNumber = "GIM33",
                    Date = new DateTime(2025, 11, 18),
                    Items = new List<Product>
            {
                new Product
                {
                    Article = "12345",
                    Name = "Смартфон",
                    Category = "Техника",
                    Stock = 10,
                    Price = 1000m,
                    Unit = "шт"
                }
            }
                };

                string actualHtml = _creator.GenerateInvoiceHtml(invoice);
                string expectedHtml = File.ReadAllText(@"C:\Смирнова\1.html", Encoding.UTF8);
                Assert.AreEqual(NormalizeHtml(expectedHtml), NormalizeHtml(actualHtml));
            }
            [TestMethod]
            public void Test_MultipleItemsInvoice_ShouldMatchExpectedHtml()
            {
                var invoice = new Invoice
                {
                    SerialNumber = "ABCD425_123*",
                    Date = new DateTime(2025, 11, 11),
                    Items = new List<Product>
            {
                new Product
                {
                    Article = "123",
                    Name = "Молоток отечественный",
                    Category = "Молотки",
                    Stock = 10,
                    Price = 100m,
                    Unit = "шт."
                },
                new Product
                {
                    Article = "BE425 0",
                    Name = "Топор лесной",
                    Category = "Топоры",
                    Stock = 2000,
                    Price = 500m,
                    Unit = "шт."
                }
            }
                };

                string actualHtml = _creator.GenerateInvoiceHtml(invoice);
                string expectedHtml = File.ReadAllText(@"C:\Смирнова\2.html", Encoding.UTF8);
                Assert.AreEqual(NormalizeHtml(expectedHtml), NormalizeHtml(actualHtml));
            }
            [TestMethod]
            public void Test_EmptyInvoice_ShouldReturnErrorMessage()
            {
                var invoice = new Invoice
                {
                    SerialNumber = "-",
                    Date = DateTime.MinValue,
                    Items = new List<Product>()
                };

                string result = _creator.GenerateInvoiceHtml(invoice);

                Assert.AreEqual("Накладная пустая, добавьте позиции в накладную!", result);
            }
    }
}
