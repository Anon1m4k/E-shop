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

        [TestMethod]
        public void TestAddInvoiceWithNegativePrice()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice
            {
                ID_Invoice = 2,
                Date = new DateTime(2025, 10, 26),
                Items = new List<InvoiceItem>
            {
            new InvoiceItem
            {
                Article = "123",
                Name = "Компьютер",
                Category = "Техника",
                Price = -1000,
                Quantity = 10,
                Unit = "шт"
            }
            }
            };

            var result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual("Цена товара должна быть положительной", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
        [TestMethod]
        public void TestAddInvoiceWithNegativeQuantity()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice
            {
                ID_Invoice = 3,
                Date = new DateTime(2025, 10, 26),
                Items = new List<InvoiceItem>
            {
            new InvoiceItem
            {
                Article = "12",
                Name = "Монитор",
                Category = "Техника",
                Price = 1000,
                Quantity = -10,
                Unit = "шт"
            }
            }
            };

            var result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual("Количество товара не может быть отрицательным", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
        [TestMethod]
        public void TestAddInvoiceWithEmptyArticle()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice
            {
                ID_Invoice = 4,
                Date = new DateTime(2025, 10, 26),
                Items = new List<InvoiceItem>
            {
            new InvoiceItem
            {
                Article = "",
                Name = "Мышка",
                Category = "Техника",
                Price = 1000,
                Quantity = 10,
                Unit = "шт"
            }
            }
            };
 
            var result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual("Артикул товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
    }
}
