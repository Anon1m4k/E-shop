using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class Invoice
    {
        public int ID_Invoice { get; set; }
        public DateTime Date { get; set; }
        public List<InvoiceItem> Items { get; set; }
    }
}
