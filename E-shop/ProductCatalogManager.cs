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

        public bool AddProduct(Product product)
        {
            if (validator != null)
            {
                bool isValid = validator.Validate(product);
                if (!isValid)
                {
                    return false;
                }
            }
            if (repository != null)
            {
                if (repository.ArticleExists(product.Article))
                {
                    return false;
                }
                repository.AddProduct(product);
            }
            return true;
        }

        public bool DeleteProduct(string article)
        {
            if (repository == null)
                return false;
            return repository.DeleteProduct(article);
        }
    }
}