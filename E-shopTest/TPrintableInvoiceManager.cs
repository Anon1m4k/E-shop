using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopTest
{
    [TestClass]

    public class TPrintableInvoiceManager
    {
        public class InvoicePrintTests
        {
            private readonly E_shopLib1.PrintableInvoiceCreator _creator = new E_shopLib1.PrintableInvoiceCreator();

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
        }
}
