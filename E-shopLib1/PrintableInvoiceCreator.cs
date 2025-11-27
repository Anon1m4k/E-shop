using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class PrintableInvoiceCreator
    {
        public string GenerateInvoiceHtml(Invoice invoice, List<InvoiceItem> invoiceItems)
        {
            if (invoiceItems == null || !invoiceItems.Any())
            {
                return "Накладная пустая, добавьте позиции в накладную!";
            }

            decimal totalAmount = invoiceItems.Sum(item => item.Price * item.Quantity);

            StringBuilder html = new StringBuilder();
            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"ru\">");
            html.AppendLine("<head>");
            html.AppendLine("    <meta charset=\"UTF-8\">");
            html.AppendLine("    <title>Приходная накладная</title>");
            html.AppendLine("    <style>");
            html.AppendLine("        body {");
            html.AppendLine("            font-family: 'Times New Roman', serif;");
            html.AppendLine("            margin: 0;");
            html.AppendLine("            padding: 20px;");
            html.AppendLine("            background: white;");
            html.AppendLine("            font-size: 14pt;");
            html.AppendLine("            line-height: 1.3;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .invoice-container {");
            html.AppendLine("            width: 100%;");
            html.AppendLine("            max-width: 210mm;");
            html.AppendLine("            margin: 0 auto;");
            html.AppendLine("            padding: 20px;");
            html.AppendLine("            box-sizing: border-box;");
            html.AppendLine("            background: white;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .invoice-header {");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            margin-bottom: 30px;");
            html.AppendLine("            border-bottom: 2px solid #000;");
            html.AppendLine("            padding-bottom: 20px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .invoice-title {");
            html.AppendLine("            font-size: 18pt;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("            margin: 0 0 15px 0;");
            html.AppendLine("            text-transform: uppercase;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .invoice-number {");
            html.AppendLine("            font-size: 14pt;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("            margin: 10px 0;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .invoice-date {");
            html.AppendLine("            font-size: 12pt;");
            html.AppendLine("            margin: 5px 0;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .items-table {");
            html.AppendLine("            width: 100%;");
            html.AppendLine("            border-collapse: collapse;");
            html.AppendLine("            margin: 25px 0;");
            html.AppendLine("            font-size: 11pt;");
            html.AppendLine("            border: 2px solid #000;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .items-table th {");
            html.AppendLine("            border: 1px solid #000;");
            html.AppendLine("            padding: 8px 4px;");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            background-color: #f0f0f0;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("            font-size: 10pt;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .items-table td {");
            html.AppendLine("            border: 1px solid #000;");
            html.AppendLine("            padding: 8px 4px;");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            font-size: 10pt;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .total-section {");
            html.AppendLine("            text-align: right;");
            html.AppendLine("            font-size: 12pt;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("            margin: 30px 0;");
            html.AppendLine("            padding: 15px 0;");
            html.AppendLine("            border-top: 2px solid #000;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .signature-section {");
            html.AppendLine("            margin-top: 60px;");
            html.AppendLine("            text-align: right;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .signature-line {");
            html.AppendLine("            margin-top: 40px;");
            html.AppendLine("            border-bottom: 1px solid #000;");
            html.AppendLine("            width: 250px;");
            html.AppendLine("            display: inline-block;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .signature-label {");
            html.AppendLine("            margin-top: 5px;");
            html.AppendLine("            font-style: italic;");
            html.AppendLine("            font-size: 10pt;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .amount-cell {");
            html.AppendLine("            white-space: nowrap;");
            html.AppendLine("            text-align: right;");
            html.AppendLine("            padding-right: 10px !important;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .text-left {");
            html.AppendLine("            text-align: left !important;");
            html.AppendLine("        }");
            html.AppendLine("    </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("    <div class=\"invoice-container\">");
            html.AppendLine("        <div class=\"invoice-header\">");
            html.AppendLine("            <h1 class=\"invoice-title\">ПРИХОДНАЯ НАКЛАДНАЯ</h1>");
            html.AppendLine($"            <div class=\"invoice-number\">№ {invoice.SerialNumber}</div>");
            html.AppendLine($"            <div class=\"invoice-date\">Дата: {invoice.Date:dd.MM.yyyy}</div>");
            html.AppendLine("        </div>");
            html.AppendLine("");
            html.AppendLine("        <table class=\"items-table\">");
            html.AppendLine("            <thead>");
            html.AppendLine("                <tr>");
            html.AppendLine("                    <th style=\"width: 12%\">Артикул</th>");
            html.AppendLine("                    <th style=\"width: 25%\">Наименование</th>");
            html.AppendLine("                    <th style=\"width: 15%\">Категория</th>");
            html.AppendLine("                    <th style=\"width: 10%\">Количество</th>");
            html.AppendLine("                    <th style=\"width: 12%\">Цена</th>");
            html.AppendLine("                    <th style=\"width: 10%\">Ед. изм.</th>");
            html.AppendLine("                    <th style=\"width: 16%\">Сумма</th>");
            html.AppendLine("                </tr>");
            html.AppendLine("            </thead>");
            html.AppendLine("            <tbody>");

            foreach (var item in invoiceItems)
            {
                html.AppendLine("                <tr>");
                html.AppendLine($"                    <td>{item.Article}</td>");
                html.AppendLine($"                    <td class=\"text-left\">{item.Name}</td>");
                html.AppendLine($"                    <td>{item.Category}</td>");
                html.AppendLine($"                    <td>{item.Quantity}</td>");
                html.AppendLine($"                    <td class=\"amount-cell\">{item.Price:N2}</td>");
                html.AppendLine($"                    <td>{item.Unit}</td>");
                html.AppendLine($"                    <td class=\"amount-cell\">{(item.Price * item.Quantity):N2}</td>");
                html.AppendLine("                </tr>");
            }

            html.AppendLine("            </tbody>");
            html.AppendLine("        </table>");
            html.AppendLine("");
            html.AppendLine($"        <div class=\"total-section\">");
            html.AppendLine($"            Общая сумма накладной: {totalAmount:N2} руб.");
            html.AppendLine("        </div>");
            html.AppendLine("");
            html.AppendLine("        <div class=\"signature-section\">");
            html.AppendLine("            <div class=\"signature-line\"></div>");
            html.AppendLine("            <div class=\"signature-label\">(подпись)</div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
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
            string architecture = Environment.Is64BitProcess ? "x64" : "x86";

            string repoRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string wkhtmlPath = Path.Combine(repoRoot, "wkhtmltopdf", architecture, "wkhtmltopdf.exe");

            if (File.Exists(wkhtmlPath))
            {
                return wkhtmlPath;
            }
            string localPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"wkhtmltopdf_{architecture}.exe");
            if (File.Exists(localPath))
            {
                return localPath;
            }

            throw new FileNotFoundException($"wkhtmltopdf не найден. Положите файлы в папку: {Path.Combine(repoRoot, "wkhtmltopdf", architecture)}");
        }
    }
}
