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
                return value.ToString("N2").Replace(",", " "); // Формат 25 990.00
            }

            decimal total = check.Items.Sum(item => item.Price * item.Quantity);
            string receiptNumber = check.IdCheck.ToString("D6"); // Формат 000001

            return $@"
<!DOCTYPE html>
<html lang=""ru"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Чек продажи №{check.IdCheck}</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
            font-family: 'Courier New', monospace;
        }}
        
        body {{
            background-color: #f5f5f5;
            padding: 20px;
            display: flex;
            justify-content: center;
        }}
        
        .receipt-container {{
            width: 100%;
            max-width: 400px;
            background-color: white;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            border: 1px solid #ddd;
        }}
        
        .header {{
            text-align: center;
            margin-bottom: 20px;
            padding-bottom: 15px;
            border-bottom: 2px dashed #333;
        }}
        
        .company-name {{
            font-size: 18px;
            font-weight: bold;
            margin-bottom: 5px;
        }}
        
        .receipt-title {{
            font-size: 16px;
            margin-bottom: 10px;
        }}
        
        .receipt-number {{
            font-size: 14px;
            margin-bottom: 5px;
        }}
        
        .info-section {{
            margin-bottom: 15px;
        }}
        
        .info-row {{
            display: flex;
            justify-content: space-between;
            margin-bottom: 5px;
            font-size: 14px;
        }}
        
        .items-table {{
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 15px;
            font-size: 12px;
        }}
        
        .items-table th {{
            text-align: left;
            padding: 5px;
            border-bottom: 1px solid #ddd;
            font-weight: bold;
        }}
        
        .items-table td {{
            padding: 5px;
            border-bottom: 1px dashed #eee;
        }}
        
        .qty-col {{
            width: 15%;
            text-align: center;
        }}
        
        .price-col, .total-col {{
            width: 20%;
            text-align: right;
        }}
        
        .item-name {{
            width: 45%;
        }}
        
        .total-section {{
            border-top: 2px dashed #333;
            padding-top: 10px;
            margin-top: 10px;
            text-align: right;
            font-weight: bold;
            font-size: 16px;
        }}
        
        @media print {{
            body {{
                background-color: white;
                padding: 0;
            }}
            
            .receipt-container {{
                box-shadow: none;
                border: none;
                max-width: 100%;
            }}
        }}
    </style>
</head>
<body>
    <div class=""receipt-container"">
        <div class=""header"">
            <div class=""company-name"">МАГАЗИН ""E-Shop""</div>
            <div class=""receipt-title"">КАССОВЫЙ ЧЕК</div>
            <div class=""receipt-number"">№ {receiptNumber}</div>
        </div>
        
        <div class=""info-section"">
            <div class=""info-row"">
                <span>Дата:</span>
                <span>{check.Date:dd.MM.yyyy}</span>
            </div>
            <div class=""info-row"">
                <span>Время:</span>
                <span>{check.Date:HH:mm:ss}</span>
            </div>
            <div class=""info-row"">
                <span>Клиент:</span>
                <span>{check.Client}</span>
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
                {string.Join("", check.Items.Select(item => $@"
                <tr>
                    <td class=""qty-col"">{item.Quantity}</td>
                    <td class=""item-name"">{item.Name}</td>
                    <td class=""price-col"">{FormatDecimal(item.Price)}</td>
                    <td class=""total-col"">{FormatDecimal(item.Price * item.Quantity)}</td>
                </tr>"))}
            </tbody>
        </table>
        
        <div class=""total-section"">
            <div class=""info-row"">
                <span>Итого:</span>
                <span>{FormatDecimal(total)} руб.</span>
            </div>                       
        </div>
        
        <div class=""barcode"">*{receiptNumber}*</div>
        
        <div class=""thank-you"">СПАСИБО ЗА ПОКУПКУ!</div>              
    </div>
</body>
</html>";
        }
    }
}
