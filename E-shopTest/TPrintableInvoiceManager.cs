using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_shopLib;
using E_shopLib1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace E_shopTest
{
    [TestClass]
    public class TPrintableInvoiceManager
    {
        private readonly PrintableInvoiceCreator _creator = new PrintableInvoiceCreator();

        [TestMethod]
        [DataRow("GIM33", "2025-11-18", "12345", "Смартфон", "Техника", 10, 1000, "шт", "TestData/single_item.html", 10000)]
        [DataRow("ABCD425_123*", "2025-11-11", "123", "Молоток отечественный", "Молотки", 10, 100, "шт.",
         "BE425 0", "Топор лесной", "Топоры", 2000, 500, "шт.", "TestData/multiple_items.html", 1001000)]
        public void Test_Invoice_ShouldMatchExpectedHtml(string serialNumber, string Date, string article1, string name1, string category1, int stock1, decimal price1, string unit1,
        string FilePath, decimal expectedTotal,
        string article2 = null, string name2 = null, string category2 = null, int stock2 = 0, decimal price2 = 0, string unit2 = null)
        {
            var invoice = new Invoice
            {
                SerialNumber = serialNumber,
                Date = DateTime.Parse(Date)
            };

            var invoiceItems = new List<InvoiceItem>
            {
                new InvoiceItem
                {
                    Article = article1,
                    Name = name1,
                    Category = category1,
                    Quantity = stock1,
                    Price = price1,
                    Unit = unit1
                }
            };

            if (article2 != null)
            {
                invoiceItems.Add(new InvoiceItem
                {
                    Article = article2,
                    Name = name2,
                    Category = category2,
                    Quantity = stock2,
                    Price = price2,
                    Unit = unit2
                });
            }

            decimal actualTotal = invoiceItems.Sum(item => item.Price * item.Quantity);
            Assert.AreEqual(expectedTotal, actualTotal);

            // Исправленный вызов метода с двумя параметрами
            string actualHtml = _creator.GenerateInvoiceHtml(invoice, invoiceItems);
            string expectedHtml = File.ReadAllText(FilePath, Encoding.UTF8);
            Assert.AreEqual(expectedHtml, actualHtml);
        }

        [TestMethod]
        public void Test_EmptyInvoice_ShouldReturnErrorMessage()
        {
            var invoice = new Invoice
            {
                SerialNumber = "-",
                Date = DateTime.MinValue
            };

            var emptyItems = new List<InvoiceItem>();

            // Исправленный вызов метода с двумя параметрами
            string result = _creator.GenerateInvoiceHtml(invoice, emptyItems);

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
