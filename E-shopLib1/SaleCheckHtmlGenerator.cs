using E_shopLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace E_shopLib1
{
    public class SaleCheckHtmlGenerator
    {
        private static string _template;
        private static readonly object _lockObject = new object();

        public string GenerateHtml(SaleCheck check)
        {
            if (check == null)
                throw new ArgumentNullException(nameof(check));

            // Загружаем шаблон
            string template = LoadTemplate();

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
            return template
                .Replace("{{RECEIPT_NUMBER}}", receiptNumber)
                .Replace("{{DATE}}", check.Date.ToString("dd.MM.yyyy"))
                .Replace("{{TIME}}", check.Date.ToString("HH:mm:ss"))
                .Replace("{{CLIENT}}", EscapeHtml(check.Client ?? ""))
                .Replace("{{ITEMS_ROWS}}", itemsRows.ToString())
                .Replace("{{TOTAL_AMOUNT}}", FormatDecimal(totalAmount));
        }

        private string LoadTemplate()
        {
            if (_template != null)
                return _template;

            lock (_lockObject)
            {
                if (_template != null)
                    return _template;

                var assembly = Assembly.GetExecutingAssembly();
                string resourceName = "E-shopLib1.Resources.SaleCheckTemplate.html";

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                    {
                        throw new FileNotFoundException($"Ресурс шаблона '{resourceName}' не найден. Доступные ресурсы: {string.Join(", ", assembly.GetManifestResourceNames())}");
                    }

                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        _template = reader.ReadToEnd();
                    }
                }

                return _template;
            }
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