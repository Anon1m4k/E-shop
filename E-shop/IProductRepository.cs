using System.Collections.Generic;

namespace E_shop
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        bool DeleteProduct(string article);
        Product GetProductByArticle(string article);
        List<Product> GetAllProducts();
        bool ArticleExists(string article);
    }
}