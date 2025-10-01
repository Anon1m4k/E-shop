using System.Collections.Generic;

namespace E_shop
{
    public class ProductCatalogManager
    {
        private IProductRepository repository;
        private List<Product> products;

        public ProductCatalogManager()
        {
            products = new List<Product>();
        }

        public ProductCatalogManager(IProductRepository repo)
        {
            repository = repo;
            products = new List<Product>();
        }

        public string AddProduct(Product product)
        {
            // Валидация артикула
            if (string.IsNullOrEmpty(product.Article))
            {
                return "Артикул товара не может быть пустым";
            }

            // Валидация наименования
            if (string.IsNullOrEmpty(product.Name))
            {
                return "Наименование товара не может быть пустым";
            }

            // Валидация цены
            if (product.Price <= 0)
            {
                return "Цена товара должна быть положительной";
            }

            // Валидация остатка
            if (product.Stock < 0)
            {
                return "Количество товара не может быть отрицательным";
            }

            // Валидация единицы измерения
            if (string.IsNullOrEmpty(product.Unit))
            {
                return "Единица измерения не может быть пустой";
            }

            if (repository != null)
            {
                if (repository.ArticleExists(product.Article))
                {
                    return $"Товар с артикулом '{product.Article}' уже существует";
                }
                repository.AddProduct(product);
            }
            return string.Empty; // Успешное добавление
        }
    }   
}