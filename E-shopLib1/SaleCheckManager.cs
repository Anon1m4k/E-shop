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

            foreach (var item in check.Items)
            {
                if (item.Quantity <= 0)
                    return "Количество товара не может быть отрицательным";

                var product = _productRepository.GetProductByArticle(item.Article);
                if (product == null || product.Stock < item.Quantity)
                    return $"Количество товара '{item.Name}' превышает остаток на складе. Доступно: {product?.Stock ?? 0}";
            }

            // Сохранение и обновление остатков
            var result = _repository.AddSaleCheck(check);
            if (result == "Продажа успешно сформирована")
            {
                foreach (var item in check.Items)
                {
                    var product = _productRepository.GetProductByArticle(item.Article);
                    product.Stock -= item.Quantity;
                    _productRepository.UpdateProduct(product);
                }
            }
            return result;
        }
    }
}
