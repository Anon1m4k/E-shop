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

            string receiptNumber = check.IdCheck.ToString("D6"); // Формат 000001

            return "";
        }
    }
}
