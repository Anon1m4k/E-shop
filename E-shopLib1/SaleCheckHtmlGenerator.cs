using E_shopLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class SaleCheckHtmlGenerator
    {
        public string GenerateHtml(SaleCheck check)
        {
            // Форматирование чисел с пробелами для тысяч
            string FormatDecimal(decimal value)
            {
                return value.ToString("N2").Replace(",", " ");
            }

            string receiptNumber = check.IdCheck.ToString("D6"); // Формат 000001

            StringBuilder html = new StringBuilder();

            html.AppendLine("<!DOCTYPE html>");
            html.AppendLine("<html lang=\"ru\">");
            html.AppendLine("<head>");
            html.AppendLine("    <meta charset=\"UTF-8\">");
            html.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
            html.AppendLine($"    <title>Чек продажи №{receiptNumber}</title>");
            html.AppendLine("    <style>");
            html.AppendLine("        * {");
            html.AppendLine("            margin: 0;");
            html.AppendLine("            padding: 0;");
            html.AppendLine("            box-sizing: border-box;");
            html.AppendLine("            font-family: 'Courier New', monospace;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        body {");
            html.AppendLine("            background-color: #f5f5f5;");
            html.AppendLine("            padding: 20px;");
            html.AppendLine("            display: flex;");
            html.AppendLine("            justify-content: center;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .receipt-container {");
            html.AppendLine("            width: 100%;");
            html.AppendLine("            max-width: 400px;");
            html.AppendLine("            background-color: white;");
            html.AppendLine("            padding: 20px;");
            html.AppendLine("            box-shadow: 0 0 10px rgba(0,0,0,0.1);");
            html.AppendLine("            border: 1px solid #ddd;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .header {");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            margin-bottom: 20px;");
            html.AppendLine("            padding-bottom: 15px;");
            html.AppendLine("            border-bottom: 2px dashed #333;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .company-name {");
            html.AppendLine("            font-size: 18px;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("            margin-bottom: 5px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .receipt-title {");
            html.AppendLine("            font-size: 16px;");
            html.AppendLine("            margin-bottom: 10px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .receipt-number {");
            html.AppendLine("            font-size: 14px;");
            html.AppendLine("            margin-bottom: 5px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .info-section {");
            html.AppendLine("            margin-bottom: 15px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .info-row {");
            html.AppendLine("            display: flex;");
            html.AppendLine("            justify-content: space-between;");
            html.AppendLine("            margin-bottom: 5px;");
            html.AppendLine("            font-size: 14px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .items-table {");
            html.AppendLine("            width: 100%;");
            html.AppendLine("            border-collapse: collapse;");
            html.AppendLine("            margin-bottom: 15px;");
            html.AppendLine("            font-size: 12px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .items-table th {");
            html.AppendLine("            text-align: left;");
            html.AppendLine("            padding: 5px;");
            html.AppendLine("            border-bottom: 1px solid #ddd;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .items-table td {");
            html.AppendLine("            padding: 5px;");
            html.AppendLine("            border-bottom: 1px dashed #eee;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .qty-col {");
            html.AppendLine("            width: 15%;");
            html.AppendLine("            text-align: center;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .price-col, .total-col {");
            html.AppendLine("            width: 20%;");
            html.AppendLine("            text-align: right;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .item-name {");
            html.AppendLine("            width: 45%;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .total-section {");
            html.AppendLine("            border-top: 2px dashed #333;");
            html.AppendLine("            padding-top: 10px;");
            html.AppendLine("            margin-top: 10px;");
            html.AppendLine("            text-align: right;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("            font-size: 16px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .footer {");
            html.AppendLine("            margin-top: 20px;");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            font-size: 12px;");
            html.AppendLine("            color: #666;");
            html.AppendLine("            padding-top: 15px;");
            html.AppendLine("            border-top: 1px dashed #ddd;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .barcode {");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            margin: 15px 0;");
            html.AppendLine("            font-family: 'Courier New', monospace;");
            html.AppendLine("            font-size: 36px;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        .thank-you {");
            html.AppendLine("            text-align: center;");
            html.AppendLine("            margin: 15px 0;");
            html.AppendLine("            font-weight: bold;");
            html.AppendLine("        }");
            html.AppendLine("        ");
            html.AppendLine("        @media print {");
            html.AppendLine("            body {");
            html.AppendLine("                background-color: white;");
            html.AppendLine("                padding: 0;");
            html.AppendLine("            }");
            html.AppendLine("            ");
            html.AppendLine("            .receipt-container {");
            html.AppendLine("                box-shadow: none;");
            html.AppendLine("                border: none;");
            html.AppendLine("                max-width: 100%;");
            html.AppendLine("            }");
            html.AppendLine("        }");
            html.AppendLine("    </style>");
            html.AppendLine("</head>");
            html.AppendLine("<body>");
            html.AppendLine("    <div class=\"receipt-container\">");
            html.AppendLine("        <div class=\"header\">");
            html.AppendLine("            <div class=\"company-name\">МАГАЗИН \"E-Shop\"</div>");
            html.AppendLine("            <div class=\"receipt-title\">КАССОВЫЙ ЧЕК</div>");
            html.AppendLine($"            <div class=\"receipt-number\">№ {receiptNumber}</div>");
            html.AppendLine("        </div>");
            html.AppendLine("        ");
            html.AppendLine("        <div class=\"info-section\">");
            html.AppendLine("            <div class=\"info-row\">");
            html.AppendLine("                <span>Дата:</span>");
            html.AppendLine($"                <span>{check.Date:dd.MM.yyyy}</span>");
            html.AppendLine("            </div>");
            html.AppendLine("            <div class=\"info-row\">");
            html.AppendLine("                <span>Время:</span>");
            html.AppendLine($"                <span>{check.Date:HH:mm:ss}</span>");
            html.AppendLine("            </div>");
            html.AppendLine("            <div class=\"info-row\">");
            html.AppendLine("                <span>Клиент:</span>");
            html.AppendLine($"                <span>{check.Client}</span>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("        ");
            html.AppendLine("        <table class=\"items-table\">");
            html.AppendLine("            <thead>");
            html.AppendLine("                <tr>");
            html.AppendLine("                    <th class=\"qty-col\">Кол-во</th>");
            html.AppendLine("                    <th class=\"item-name\">Товар</th>");
            html.AppendLine("                    <th class=\"price-col\">Цена</th>");
            html.AppendLine("                    <th class=\"total-col\">Сумма</th>");
            html.AppendLine("                </tr>");
            html.AppendLine("            </thead>");
            html.AppendLine("            <tbody>");

            // Добавляем строки с товарами
            foreach (var item in check.Items)
            {
                html.AppendLine("                <tr>");
                html.AppendLine($"                    <td class=\"qty-col\">{item.Quantity}</td>");
                html.AppendLine($"                    <td class=\"item-name\">{item.Name}</td>");
                html.AppendLine($"                    <td class=\"price-col\">{FormatDecimal(item.Price)}</td>");
                html.AppendLine($"                    <td class=\"total-col\">{FormatDecimal(item.Total)}</td>");
                html.AppendLine("                </tr>");
            }

            html.AppendLine("            </tbody>");
            html.AppendLine("        </table>");
            html.AppendLine("        ");
            html.AppendLine("        <div class=\"total-section\">");
            html.AppendLine("            <div class=\"info-row\">");
            html.AppendLine("                <span>Итого:</span>");
            html.AppendLine($"                <span>{FormatDecimal(check.Total)} руб.</span>");
            html.AppendLine("            </div>");
            html.AppendLine("        </div>");
            html.AppendLine("        ");
            html.AppendLine($"        <div class=\"barcode\">*{receiptNumber}*</div>");
            html.AppendLine("        ");
            html.AppendLine("        <div class=\"thank-you\">СПАСИБО ЗА ПОКУПКУ!</div>");
            html.AppendLine("        ");
            html.AppendLine("        <div class=\"footer\">");
            html.AppendLine("            <div>Телефон: +7 (999) 123-45-67</div>");
            html.AppendLine("            <div>e-shop@example.com</div>");
            html.AppendLine("        </div>");
            html.AppendLine("    </div>");
            html.AppendLine("</body>");
            html.AppendLine("</html>");

            return html.ToString();
        }
    }
}