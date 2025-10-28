using E_shopLib;

namespace E_shop
{
    public class ProductCatalogManager
    {
        private IProductRepository repository;

        public ProductCatalogManager(IProductRepository repo)
        {
            repository = repo;
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
        public string UpdateProduct(Product product)
        {
            if (string.IsNullOrEmpty(product.Name))
            {
                return "Наименование товара не может быть пустым";
            }
            if (product.Price <= 0)
            {
                return "Цена товара должна быть положительной";
            }
            if (product.Stock < 0)
            {
                return "Количество товара не может быть отрицательным";
            }
            if (string.IsNullOrEmpty(product.Unit))
            {
                return "Единица измерения не может быть пустой";
            }
            repository.UpdateProduct(product);
            return string.Empty;
        }
    }
}