using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public interface IInvoiceRepository
    {
        string AddInvoice(Invoice invoice);
    }
}
