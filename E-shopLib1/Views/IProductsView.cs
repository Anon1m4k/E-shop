using System.Collections.Generic;

namespace E_shopLib
{
    public interface IProductsView
    {
        void DisplayProducts(List<Product> products);
    }
}