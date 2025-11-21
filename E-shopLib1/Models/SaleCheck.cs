using E_shopLib1;
using System;
using System.Collections.Generic;

namespace E_shopLib
{
    public class SaleCheck
    {
        public int IdCheck { get; set; }
        public DateTime Date { get; set; }
        public string Client { get; set; }
        public List<InvoiceItem> Items { get; set; }

        public SaleCheck()
        {
            Items = new List<InvoiceItem>();
            Date = DateTime.Now;
        }
    }
}