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

            int id = repository_.GetNextInvoiceId();
            invoice.SetId(id);
            string result = repository_.AddInvoice(invoice);

            var savedInvoice = repository_.GetInvoiceById(id);
            if (savedInvoice == null || savedInvoice.ID_Invoice != id)
            {
                return "Ошибка при сохранении накладной";
            }

            return result;
            
        }

    }
}
