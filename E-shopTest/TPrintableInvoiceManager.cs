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
           [DataRow("GIM33", "2025-11-18", "12345", "Смартфон", "Техника", 10, 1000, "шт", "TestData/single_item.html")]
           [DataRow("ABCD425_123*", "2025-11-11", "123", "Молоток отечественный", "Молотки", 10, 100, "шт.",
            "ABCD425_123*", "2025-11-11", "BE425 0", "Топор лесной", "Топоры", 2000, 500, "шт.", "TestData/multiple_items.html")]
           public void Test_Invoice_ShouldMatchExpectedHtml(string serialNumber, string Date, string article, string name,
                  string category, int stock, decimal price, string unit, string FilePath)
           {
              var invoice = new Invoice
              {
                SerialNumber = serialNumber,
                Date = DateTime.Parse(Date),
                Items = new List<Product>
                {
                    new Product
                    {
                        Article = article,
                        Name = name,
                        Category = category,
                        Stock = stock,
                        Price = price,
                        Unit = unit
                    }
                }
              };
                decimal actualTotal = invoice.Items.Sum(item => item.Stock * item.Price);
                Assert.AreEqual(expectedTotal, actualTotal);
            
                string actualHtml = _creator.GenerateInvoiceHtml(invoice);
                string expectedHtml = File.ReadAllText(FilePath, Encoding.UTF8);
                Assert.AreEqual(expectedHtml, actualHtml);
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
           [TestMethod]
           [DataRow("TestData/single_item.html", "test_single.pdf")]
           [DataRow("TestData/multiple_items.html", "test_multiple.pdf")]
           public void Test_CreatePdfFromHtml_ShouldCreatePdfFile(string FilePath, string outputPath)
           {
            string htmlContent = File.ReadAllText(FilePath, Encoding.UTF8);

            string result = _creator.CreatePdfFromHtml(htmlContent, outputPath);

            Assert.IsTrue(File.Exists(outputPath)); 
           }
    }
}
