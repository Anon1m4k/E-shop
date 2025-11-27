using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class PrintableInvoiceCreator
    {
        public string GenerateInvoiceHtml(Invoice invoice)
        {
            return "Накладная пустая, добавьте позиции в накладную!";
        }
        public string CreatePdfFromHtml(string htmlContent, string outputPath)
        {
            return "";
        }
    }
}
