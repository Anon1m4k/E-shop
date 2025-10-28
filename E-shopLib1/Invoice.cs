using E_shopLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class Invoice
    {
        private int IdInvoice_;
        public int IdInvoice
        {
            get { return IdInvoice_; }
           
        }
        public DateTime Date { get; set; }
        public List<Product> Items { get; set; }
    }
}
