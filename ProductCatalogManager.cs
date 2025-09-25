using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_shop
{
    public class ProductCatalogManager
    {
        private List<Product> _products = new List<Product>();
        public bool AddProduct(Product product)
        {
            _products.Add(product);
            return true;
        }
        public bool DeleteProduct(Product product)
        {
            return true;
        }

        public Product GetProduct(string article)
        {
            return _products.FirstOrDefault(p => p.Article == article);
        }

        // Метод для получения всех товаров (для проверки в тестах)
        public List<Product> GetAllProducts()
        {
            return new List<Product>(_products);
        }

    }
}