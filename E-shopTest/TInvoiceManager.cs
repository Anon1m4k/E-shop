using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopTest
{
    public class TInvoiceManager
    {
        [TestMethod]
        public void TestAddInvoiceWithValidData()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var validInvoice = new Invoice
            {
                ID_Invoice = 1,
                Date = new DateTime(2025, 10, 26),
                Items = new List<InvoiceItem>
            {
            new InvoiceItem
            {
                Article = "12345",
                Name = "Смартфон",
                Category = "Техника",
                Price = 1000,
                Quantity = 10,
                Unit = "шт"
            }
            }
            };

            mockRepository.Setup(r => r.AddInvoice(validInvoice)).Returns("Приходная накладная успешно добавлена");

            var result = manager.AddInvoice(validInvoice);

            Assert.AreEqual("Приходная накладная успешно добавлена", result);
            mockRepository.Verify(r => r.AddInvoice(validInvoice), Times.Once);
        }
    }
}
