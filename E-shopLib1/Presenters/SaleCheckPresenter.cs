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
            // Создаем ProductCatalogManager внутри презентера
            catalogManager_ = new ProductCatalogManager(repository);

            // Подписка на событие выбора категории
            categoriesView_.CategorySelected += OnCategorySelected;

            // Загрузка начальных данных
            LoadCategories();
        }

        public List<string> Categories() => catalogManager_.GetCategories();

        private void LoadCategories()
        {
            List<string> categories = catalogManager_.GetCategories();
            categoriesView_.ShowCategories(categories);
        }

        private void OnCategorySelected(string category)
        {
            List<Product> products = catalogManager_.GetProductsByCategory(category);
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