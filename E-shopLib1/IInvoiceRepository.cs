namespace E_shopLib1
{
    public interface IInvoiceRepository
    {
        string AddInvoice(Invoice invoice);
        int GetNextInvoiceId();
    }
}