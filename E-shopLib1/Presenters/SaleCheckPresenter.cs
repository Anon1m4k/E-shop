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

        public SaleCheckPresenter(ICategoriesView categoriesView, IProductsView productsView, ProductCatalogManager catalogManager)
        {
            categoriesView_ = categoriesView;
            productsView_ = productsView;
            catalogManager_ = catalogManager;

            // Подписка на событие выбора категории
            categoriesView_.CategorySelected += OnCategorySelected;

            // Загрузка начальных данных
            LoadCategories();
        }

        public List<string> Categories() => catalogManager_.GetCategories();

        public List<Product> ProductsByCategory(string category)
        {
            // Временная реализация - в реальном приложении нужно добавить соответствующий метод в ProductCatalogManager
            List<Product> allProducts = catalogManager_.GetAllProducts();
            return allProducts.FindAll(p => p.Category == category);
        }

        private void LoadCategories()
        {
            List<string> categories = catalogManager_.GetCategories();
            categoriesView_.ShowCategories(categories);
        }

        private void OnCategorySelected(string category)
        {
            List<Product> products = ProductsByCategory(category);
            productsView_.DisplayProducts(products);
        }

        public string CreateSaleCheck(SaleCheck check)
        {
            // Реализация создания чека продажи
            // В реальном приложении нужно добавить соответствующую логику
            return "Чек успешно создан";
        }
    }
}