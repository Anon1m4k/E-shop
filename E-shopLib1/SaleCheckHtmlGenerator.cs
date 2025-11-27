using E_shopLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace E_shopLib1
{
    public class SaleCheckHtmlGenerator
    {
        private const string HTML_TEMPLATE = @"
<!DOCTYPE html>
<html lang=""ru"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Чек продажи №{{RECEIPT_NUMBER}}</title>
    <style>
        * {
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Courier New', monospace;
        }
        
        body {
            background-color: #f5f5f5;
            padding: 20px;
            display: flex;
            justify-content: center;
        }
        
        .receipt-container {
            width: 100%;
            max-width: 400px;
            background-color: white;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            border: 1px solid #ddd;
        }
        
        .header {
            text-align: center;
            margin-bottom: 20px;
            padding-bottom: 15px;
            border-bottom: 2px dashed #333;
        }
        
        .company-name {
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 5px;
        }
        
        .receipt-title {
            font-size: 16px;
            margin-bottom: 10px;
        }
        
        .receipt-number {
            font-size: 14px;
            margin-bottom: 5px;
        }
        
        .info-section {
            margin-bottom: 15px;
        }
        
        .info-row {
            display: flex;
            justify-content: space-between;
            margin-bottom: 5px;
            font-size: 14px;
        }
        
        .items-table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
            font-size: 12px;
        }
        
        .items-table th {
            text-align: left;
            padding: 5px;
            border-bottom: 1px solid #ddd;
            font-weight: bold;
        }
        
        .items-table td {
            padding: 5px;
            border-bottom: 1px dashed #eee;
        }
        
        .qty-col {
            width: 15%;
            text-align: center;
        }
        
        .price-col, .total-col {
            width: 20%;
            text-align: right;
        }
        
        .item-name {
            width: 45%;
        }
        
        .total-section {
            border-top: 2px dashed #333;
            padding-top: 10px;
            margin-top: 10px;
            text-align: right;
            font-weight: bold;
            font-size: 16px;
        }
        
        .footer {
            margin-top: 20px;
            text-align: center;
            font-size: 12px;
            color: #666;
            padding-top: 15px;
            border-top: 1px dashed #ddd;
        }
        
        .barcode {
            text-align: center;
            margin: 15px 0;
            font-family: 'Courier New', monospace;
            font-size: 36px;
        }
        
        .thank-you {
            text-align: center;
            margin: 15px 0;
            font-weight: bold;
        }
        
        @media print {
            body {
                background-color: white;
                padding: 0;
            }
            
            .receipt-container {
                box-shadow: none;
                border: none;
                max-width: 100%;
            }
        }
    </style>
</head>
<body>
    <div class=""receipt-container"">
        <div class=""header"">
            <div class=""company-name"">МАГАЗИН ""E-Shop""</div>
            <div class=""receipt-title"">КАССОВЫЙ ЧЕК</div>
            <div class=""receipt-number"">№ {{RECEIPT_NUMBER}}</div>
        </div>
        
        <div class=""info-section"">
            <div class=""info-row"">
                <span>Дата:</span>
                <span>{{DATE}}</span>
            </div>
            <div class=""info-row"">
                <span>Время:</span>
                <span>{{TIME}}</span>
            </div>
            <div class=""info-row"">
                <span>Клиент:</span>
                <span>{{CLIENT}}</span>
            </div>
        </div>
        
        <table class=""items-table"">
            <thead>
                <tr>
                    <th class=""qty-col"">Кол-во</th>
                    <th class=""item-name"">Товар</th>
                    <th class=""price-col"">Цена</th>
                    <th class=""total-col"">Сумма</th>
                </tr>
            </thead>
            <tbody>
                {{ITEMS_ROWS}}
            </tbody>
        </table>
        
        <div class=""total-section"">
            <div class=""info-row"">
                <span>Итого:</span>
                <span>{{TOTAL_AMOUNT}} руб.</span>
            </div>
        </div>
        
        <div class=""barcode"">*{{RECEIPT_NUMBER}}*</div>
        
        <div class=""thank-you"">СПАСИБО ЗА ПОКУПКУ!</div>
        
        <div class=""footer"">
            <div>Телефон: +7 (999) 123-45-67</div>
            <div>e-shop@example.com</div>
        </div>
    </div>
</body>
</html>";

        public string GenerateHtml(SaleCheck check)
        {
            if (check == null)
                throw new ArgumentNullException(nameof(check));

            // Форматирование чисел с пробелами для тысяч
            string FormatDecimal(decimal value) => value.ToString("N2").Replace(",", " ");

            string receiptNumber = check.IdCheck.ToString("D6");
            decimal totalAmount = check.Items?.Sum(item => item.Price * item.Quantity) ?? 0;

            // Генерация строк товаров
            var itemsRows = new StringBuilder();
            if (check.Items != null)
            {
                foreach (var item in check.Items)
                {
                    itemsRows.AppendLine($@"
                <tr>
                    <td class=""qty-col"">{item.Quantity}</td>
                    <td class=""item-name"">{EscapeHtml(item.Name)}</td>
                    <td class=""price-col"">{FormatDecimal(item.Price)}</td>
                    <td class=""total-col"">{FormatDecimal(item.Total)}</td>
                </tr>");
                }
            }

            // Замена плейсхолдеров в шаблоне
            return HTML_TEMPLATE
                .Replace("{{RECEIPT_NUMBER}}", receiptNumber)
                .Replace("{{DATE}}", check.Date.ToString("dd.MM.yyyy"))
                .Replace("{{TIME}}", check.Date.ToString("HH:mm:ss"))
                .Replace("{{CLIENT}}", EscapeHtml(check.Client ?? ""))
                .Replace("{{ITEMS_ROWS}}", itemsRows.ToString())
                .Replace("{{TOTAL_AMOUNT}}", FormatDecimal(totalAmount));
        }

        private string EscapeHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            return text
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("\"", "&quot;")
                .Replace("'", "&#39;");
        }
    }
}