using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class InvoiceManager
    {
        private IInvoiceRepository repository;
        public InvoiceManager(IInvoiceRepository repоsitori_)
        {
            repository = repоsitori_;
        }
        public string AddInvoice(Invoice invoice)
        {

            return "Все отлично";
        }
    }
}
