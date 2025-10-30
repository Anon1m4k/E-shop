using E_shopLib;
using System.Collections.Generic;
using System;

namespace E_shopLib1
{
    public class Invoice
    {
        private int _idInvoice;

        public int ID_Invoice
        {
            get { return _idInvoice; }
        }

        public DateTime Date { get; set; }
        public List<Product> Items { get; set; }

        public Invoice()
        {
            Date = DateTime.Now;
            Items = new List<Product>();
        }
        public void SetId(int id)
        {
            _idInvoice = id;
        }
    }
}