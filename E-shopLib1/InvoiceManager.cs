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
        public Invoice CreateNewInvoice()
        {
            Invoice invoice = new Invoice();
            int Id = repository_.GetNextInvoiceId();
            invoice.SetId(Id);
            invoice.Date = DateTime.Now;
            return invoice;
        }
        public string AddInvoice(Invoice invoice)
        {
            if (invoice == null)
                return "Накладная не может быть null";

            if (invoice.Items == null || invoice.Items.Count == 0)
                return "Накладная должна содержать хотя бы одну позицию";

            foreach (var product in invoice.Items)
            {
                if (string.IsNullOrWhiteSpace(product.Article))
                    return "Артикул товара не может быть пустым";

                if (string.IsNullOrWhiteSpace(product.Name))
                    return $"Наименование товара с артикулом '{product.Article}' не может быть пустым";

                if (product.Stock <= 0)
                    return $"Количество товара '{product.Article}' должно быть больше 0";

                if (product.Price <= 0)
                    return $"Цена товара '{product.Article}' должна быть больше 0";
            }

            try
            {
                return repository_.AddInvoice(invoice);
            }
            catch (Exception ex)
            {
                return $"Ошибка при сохранении накладной: {ex.Message}";
            }

        }

    }
}
