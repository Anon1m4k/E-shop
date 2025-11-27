using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class PrintableInvoiceCreator
    {
        private string _templateContent;

        public PrintableInvoiceCreator()
        {
            LoadTemplate();
        }

        private void LoadTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            string resourceName = "E_shopLib1.Resources.invoice_template.html";

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        _templateContent = reader.ReadToEnd();
                    }
                }
            }
        }

        public string GenerateInvoiceHtml(Invoice invoice, List<InvoiceItem> invoiceItems)
        {
            if (invoiceItems == null || !invoiceItems.Any())
            {
                return "Накладная пустая, добавьте позиции в накладную!";
            }

            decimal totalAmount = invoiceItems.Sum(item => item.Price * item.Quantity);

            StringBuilder itemsRows = new StringBuilder();
            foreach (var item in invoiceItems)
            {
                itemsRows.AppendLine($@"
                <tr>
                    <td>{item.Article}</td>
                    <td class=""text-left"">{item.Name}</td>
                    <td>{item.Category}</td>
                    <td>{item.Quantity}</td>
                    <td class=""amount-cell"">{item.Price:N2}</td>
                    <td>{item.Unit}</td>
                    <td class=""amount-cell"">{(item.Price * item.Quantity):N2}</td>
                </tr>");
            }

            string html = _templateContent
                .Replace("{{SERIAL_NUMBER}}", invoice.SerialNumber)
                .Replace("{{INVOICE_DATE}}", invoice.Date.ToString("dd.MM.yyyy"))
                .Replace("{{ITEMS_ROWS}}", itemsRows.ToString())
                .Replace("{{TOTAL_AMOUNT}}", totalAmount.ToString("N2"));

            return html;
        }
       
        public string CreatePdfFromHtml(string htmlContent, string outputPath)
        {
            try
            {
                string tempHtmlFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString() + ".html");
                File.WriteAllText(tempHtmlFile, htmlContent, Encoding.UTF8);
                string wkhtmltopdfPath = GetWkhtmltopdfPath();

                if (!File.Exists(wkhtmltopdfPath))
                {
                    return "wkhtmltopdf не установлен";
                }

                string arguments = $"--encoding UTF-8 --page-size A4 --margin-top 10mm --margin-bottom 10mm --margin-left 10mm --margin-right 10mm \"{tempHtmlFile}\" \"{outputPath}\"";

                Process process = new Process();
                process.StartInfo.FileName = wkhtmltopdfPath;
                process.StartInfo.Arguments = arguments;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;

                process.Start();
                process.WaitForExit(30000);

                if (File.Exists(tempHtmlFile))
                {
                    File.Delete(tempHtmlFile);
                }

                if (process.ExitCode == 0 && File.Exists(outputPath))
                {
                    return "";
                }
                else
                {
                    string error = process.StandardError.ReadToEnd();
                    return $"Ошибка создания PDF: {error}";
                }
            }
            catch (Exception ex)
            {
                return $"Ошибка при создании PDF: {ex.Message}";
            }
        }
        private string GetWkhtmltopdfPath()
        {
            string resourceName = "E_shopLib1.Resources.wkhtmltopdf_x64.exe";
            string tempPath = Path.Combine(Path.GetTempPath(), "wkhtmltopdf.exe");

            try
            {
                if (File.Exists(tempPath))
                    return tempPath;

                var assembly = Assembly.GetExecutingAssembly();

                using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (resourceStream == null)
                        return null;

                    using (var fileStream = File.Create(tempPath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }

                return tempPath;
            }
            catch
            {
                return null;
            }
        }
    }
}
