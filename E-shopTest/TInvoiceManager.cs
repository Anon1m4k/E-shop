using E_shopLib;
using E_shopLib1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                Items = new List<Product>
            {
            new Product
            {
                Article = "12345",
                Name = "Смартфон",
                Category = "Техника",
                Price = 1000,
                Stock = 10,
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
                Items = new List<Product>
            {
            new Product
            {
                Article = "123",
                Name = "Компьютер",
                Category = "Техника",
                Price = -1000,
                Stock = 10,
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
                Items = new List<Product>
            {
            new Product
            {
                Article = "12",
                Name = "Монитор",
                Category = "Техника",
                Price = 1000,
                Stock = -10,
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
                Items = new List<Product>
            {
            new Product
            {
                Article = "",
                Name = "Мышка",
                Category = "Техника",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            }
            }
            };
 
            var result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual("Артикул товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
        [TestMethod]
        public void TestAddInvoiceWithMultipleItems()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var validInvoice = new Invoice
            {
                ID_Invoice = 5,
                Date = new DateTime(2025, 10, 26),
                Items = new List<Product>
            {
            new Product
            {
                Article = "22",
                Name = "Мышка компьютерная",
                Category = "Техника",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            },
            new Product
            {
                Article = "33",
                Name = "Коврик для мышки",
                Category = "Аксессуары",
                Price = 1000,
                Stock = 10,
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
        public void TestAddInvoiceWithEmptyName()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice
            {
                ID_Invoice = 6,
                Date = new DateTime(2025, 10, 26),
                Items = new List<Product>
            {
            new Product
            {
                Article = "567",
                Name = "",
                Category = "Техника",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            }
            }
            };

            var result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual("Наименование товара не может быть пустым", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
        [TestMethod]
        public void TestAddInvoiceWithEmptyCategory()
        {
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice
            {
                ID_Invoice = 7,
                Date = new DateTime(2025, 10, 26),
                Items = new List<Product>
            {
            new Product
            {
                Article = "567",
                Name = "Клавиатура",
                Category = "",
                Price = 1000,
                Stock = 10,
                Unit = "шт"
            }
            }
            };

            var result = manager.AddInvoice(invalidInvoice);

            Assert.AreEqual("Категория должна быть заполнена", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
        [TestMethod]
        public void TestAddInvoiceWithEmptyUnit()
        {
            // Arrange
            var mockRepository = new Mock<IInvoiceRepository>();
            var manager = new InvoiceManager(mockRepository.Object);

            var invalidInvoice = new Invoice
            {
                ID_Invoice = 8,
                Date = new DateTime(2025, 10, 26),
                Items = new List<Product>
            {
            new Product
            {
                Article = "657",
                Name = "Ноутбук",
                Category = "Техника",
                Price = 1000,
                Stock = 10,
                Unit = ""
            }
            }
            };

            // Act
            var result = manager.AddInvoice(invalidInvoice);

            // Assert
            Assert.AreEqual("Единица измерения должна быть заполнена", result);
            mockRepository.Verify(r => r.AddInvoice(It.IsAny<Invoice>()), Times.Never);
        }
    }
}
