using System.Collections.Generic;

namespace E_shopLib1
{
    public interface IInvoiceRepository
    {
        string AddInvoice(Invoice invoice);
        int GetNextInvoiceId();
        Invoice GetInvoiceById(int id);
        List<Invoice> GetAllInvoices();
    }
}