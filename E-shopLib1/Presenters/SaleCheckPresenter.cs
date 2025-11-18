using E_shop;
using System;
using System.Collections.Generic;

namespace E_shopLib
{
    public class SaleCheckPresenter
    {
        private readonly ProductCatalogManager _catalogManager;
        private readonly ICategoriesView _categoriesView;
        private readonly IProductsView _productsView;

        public SaleCheckPresenter(ICategoriesView categoriesView, IProductsView productsView, ProductCatalogManager catalogManager)
        {
            _categoriesView = categoriesView;
            _productsView = productsView;
            _catalogManager = catalogManager;

            // Подписка на событие выбора категории
            _categoriesView.CategorySelected += OnCategorySelected;

            // Загрузка начальных данных
            LoadCategories();
        }

        public List<string> Categories() => _catalogManager.GetCategories();

        public List<Product> ProductsByCategory(string category)
        {
            // Временная реализация - в реальном приложении нужно добавить соответствующий метод в ProductCatalogManager
            var allProducts = _catalogManager.GetAllProducts();
            return allProducts.FindAll(p => p.Category == category);
        }

        private void LoadCategories()
        {
            var categories = _catalogManager.GetCategories();
            _categoriesView.ShowCategories(categories);
        }

        private void OnCategorySelected(string category)
        {
            var products = ProductsByCategory(category);
            _productsView.DisplayProducts(products);
        }

        public string CreateSaleCheck(SaleCheck check)
        {
            // Реализация создания чека продажи
            // В реальном приложении нужно добавить соответствующую логику
            return "Чек успешно создан";
        }
    }
}