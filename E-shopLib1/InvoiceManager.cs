using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class InvoiceManager
    {
        private IInvoiceRepository repository_;
        public InvoiceManager(IInvoiceRepository repоsitori)
        {
            repository_ = repоsitori;
        }
        public string AddInvoice(Invoice invoice)
        {

            return "Все отлично";
        }
    }
}
