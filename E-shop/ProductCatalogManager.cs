using System.Collections.Generic;

namespace E_shop
{
    public class ProductCatalogManager
    {
        private IProductRepository repository;
        private IProductValidator validator;
        private List<Product> products;

        public ProductCatalogManager()
        {
            products = new List<Product>();
        }

        public ProductCatalogManager(IProductRepository repo, IProductValidator valid)
        {
            repository = repo;
            validator = valid;
            products = new List<Product>();
        }

        public string AddProduct(Product product)
        {
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

            if (validator != null)
            {
                bool isValid = validator.Validate(product);
                if (!isValid)
                {
                    return "Ошибка валидации данных товара";
                }
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

        public string DeleteProduct(string article)
        {
            if (repository == null)
                return "Репозиторий недоступен";

            var product = repository.GetProductByArticle(article);
            if (product == null)
            {
                return "Товар с указанным артикулом не найден";
            }

            return repository.DeleteProduct(article);
        }
    }
}