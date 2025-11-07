using System.Collections.Generic;

namespace E_shopLib
{
    public interface IProductRepository
    {
        string AddProduct(Product product);
        string DeleteProduct(string article);
        string UpdateProduct(Product product);
        Product GetProductByArticle(string article);
        List<Product> GetAllProducts();
        bool ArticleExists(string article);
        List<string> GetCategories(); // Новый метод для получения категорий
    }
}