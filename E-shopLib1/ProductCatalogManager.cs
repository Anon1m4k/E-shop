using E_shopLib;
using System.Collections.Generic;
using System.Linq;

namespace E_shop
{
    public class ProductCatalogManager
    {
        private IProductRepository repository;

        public ProductCatalogManager(IProductRepository repo)
        {
            repository = repo;
        }

        public Dictionary<string, List<Product>> ProductsByCategory =>
            repository.AllProductsByCategory();

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

            Product product = repository.GetProductByArticle(article);
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
            return repository.UpdateProduct(product);
        }
        public List<string> GetCategories()
        {
            if (repository != null)
            {
                return repository.GetCategories();
            }
            return new List<string>();
        }
        public List<Product> GetAllProducts()
        {
            if (repository != null)
            {
                return repository.GetAllProducts();
            }
            return new List<Product>();
        }
        public List<Product> GetProductsByCategory(string category)
        {
            if (repository != null)
            {
                List<Product> allProducts = repository.GetAllProducts();
                return allProducts.Where(p => p.Category == category).ToList();
            }
            return new List<Product>();
        }
    }
}