using E_shopLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shopLib1
{
    public class SaleCheckManager
    {
        private readonly ISaleCheckRepository _repository;
        private readonly IProductRepository _productRepository;

        public SaleCheckManager(ISaleCheckRepository repository, IProductRepository productRepository)
        {
            _repository = repository;
            _productRepository = productRepository;
        }

        public string AddSaleCheck(SaleCheck check)
        {
            // Валидация
            if (string.IsNullOrWhiteSpace(check.Client))
                return "Клиент не может быть пустым";

            // Проверяем наличие товаров и остатки
            foreach (InvoiceItem item in check.Items)
            {
                if (item.Quantity <= 0)
                    return "Количество товара не может быть отрицательным";

                Product product = _productRepository.GetProductByArticle(item.Article);
                if (product == null)
                    return $"Товар с артикулом '{item.Article}' не найден";

                if (product.Stock < item.Quantity)
                    return $"Количество товара '{item.Name}' превышает остаток на складе. Доступно: {product.Stock}";
            }

            // Сохранение чека
            string result = _repository.AddSaleCheck(check);
            if (!string.IsNullOrEmpty(result))
                return result;

            // Обновление остатков (только поля Stock)
            foreach (InvoiceItem item in check.Items)
            {
                Product product = _productRepository.GetProductByArticle(item.Article);
                int newStock = product.Stock - item.Quantity;

                string updateResult = _productRepository.UpdateStock(item.Article, newStock);
                if (!string.IsNullOrEmpty(updateResult))
                {
                    // Если не удалось обновить остаток, возвращаем ошибку
                    return $"Ошибка при обновлении остатка товара '{item.Name}': {updateResult}";
                }
            }

            return string.Empty;
        }
    }
}