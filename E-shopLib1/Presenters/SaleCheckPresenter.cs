using E_shop;
using System;
using System.Collections.Generic;

namespace E_shopLib
{
    public class SaleCheckPresenter
    {
        private readonly ProductCatalogManager catalogManager_;
        private readonly ICategoriesView categoriesView_;
        private readonly IProductsView productsView_;

        public SaleCheckPresenter(ICategoriesView categoriesView, IProductsView productsView, IProductRepository repository)
        {
            categoriesView_ = categoriesView;
            productsView_ = productsView;
            catalogManager_ = new ProductCatalogManager(repository);

            categoriesView_.CategorySelected += OnCategorySelected;
            LoadCategories();
        }

        public List<string> Categories => catalogManager_.GetCategories();

        private void LoadCategories()
        {
            List<string> categories = catalogManager_.GetCategories();
            categoriesView_.ShowCategories(categories);
        }

        private void OnCategorySelected(string category)
        {
            List<Product> products;

            if (category == "Все")
            {
                products = catalogManager_.GetAllProducts();
            }
            else
            {
                products = catalogManager_.GetProductsByCategory(category);
            }

            productsView_.DisplayProducts(products);
        }

        public string CreateSaleCheck(SaleCheck check)
        {
            // TODO: Реализация создания чека продажи
            return "Чек успешно создан";
        }

        // Метод для обновления данных
        public void RefreshData()
        {
            catalogManager_.RefreshData();
            LoadCategories();
        }
    }
}