using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class InvoiceItem
    {
        [DisplayName("Артикул")]
        public string Article { get; set; }
        [DisplayName("Наименование")]
        public string Name { get; set; }
        [DisplayName("Категория")]
        public string Category { get; set; }
        [DisplayName("Единица измерения")]
        public string Unit { get; set; }
        [DisplayName("Количество")]
        public int Quantity { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        [DisplayName("Сумма")]
        public decimal Total => Price * Quantity;
    }
}
