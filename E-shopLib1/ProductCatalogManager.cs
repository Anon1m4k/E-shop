using E_shopLib;
using System.Collections.Generic;
using System.Linq;

namespace E_shop
{
    public class ProductCatalogManager
    {
        private IProductRepository repository;
        private Dictionary<string, List<Product>> productsByCategory_;

        public ProductCatalogManager(IProductRepository repo)
        {
            repository = repo;
            RefreshProductsByCategory();
        }

        public Dictionary<string, List<Product>> ProductsByCategory => productsByCategory_;

        public void RefreshProductsByCategory()
        {
            productsByCategory_ = repository.AllProductsByCategory();
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

                string result = repository.AddProduct(product);
                if (string.IsNullOrEmpty(result))
                {
                    RefreshProductsByCategory(); // Обновляем кэш после добавления
                }
                return result;
            }
            return "Репозиторий недоступен";
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

            string result = repository.DeleteProduct(article);
            if (string.IsNullOrEmpty(result))
            {
                RefreshProductsByCategory(); // Обновляем кэш после удаления
            }
            return result;
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

            string result = repository.UpdateProduct(product);
            if (string.IsNullOrEmpty(result))
            {
                RefreshProductsByCategory(); // Обновляем кэш после обновления
            }
            return result;
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
            RefreshProductsByCategory();
            return productsByCategory_.Values.SelectMany(x => x).ToList();
        }
        public List<Product> GetProductsByCategory(string category)
        {
            RefreshProductsByCategory(); // Обновляем данные перед возвратом

            if (productsByCategory_.ContainsKey(category))
            {
                return productsByCategory_[category];
            }
            return new List<Product>();
        }        
    }
}