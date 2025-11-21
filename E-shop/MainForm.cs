using E_shopLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace E_shop
{
    public partial class MainForm : Form
    {
        SQLProductManager productManager = new SQLProductManager();

        // Поля для вкладки продаж
        private ProductCatalogManager salesCatalogManager;
        private List<Product> cartItems;
        private string selectedCategory;

        public MainForm()
        {
            (bool isValid, string errorMessage) = AppSettings.AreSettingsValidWithDetails();
            if (!isValid)
            {
                MessageBox.Show($"Ошибка загрузки настроек: {errorMessage}", "Ошибка конфигурации",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
                return;
            }
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView.DataSource = productManager.GetAllProducts();
                InitializeSalesTab(); // Инициализация вкладки продаж
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #region Вкладка "Товары"

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            ProductCatalogManager catalogManager = new ProductCatalogManager(productManager);
            AddProductForm addForm = new AddProductForm(catalogManager);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // Обновляем таблицу после добавления
                dataGridView.DataSource = productManager.GetAllProducts();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем артикул из выбранной строки
                string selectedArticle = dataGridView.SelectedRows[0].Cells["Article"].Value.ToString();

                // Запрашиваем подтверждение удаления
                DialogResult result = MessageBox.Show("Удалить выбранный товар?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Удаляем товар через менеджер
                    string deleteResult = productManager.DeleteProduct(selectedArticle);

                    if (string.IsNullOrEmpty(deleteResult))
                    {
                        MessageBox.Show("Товар успешно удален", "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Обновляем таблицу
                        dataGridView.DataSource = productManager.GetAllProducts();
                    }
                    else
                    {
                        MessageBox.Show(deleteResult, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для удаления.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем объект товара из выбранной строки
                Product selectedProduct = dataGridView.SelectedRows[0].DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    // Создаем копию товара для редактирования
                    Product productToEdit = selectedProduct.Clone();

                    ProductCatalogManager catalogManager = new ProductCatalogManager(productManager);
                    EditProductForm editForm = new EditProductForm(catalogManager, productToEdit);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Обновляем оригинальный товар в списке свойствами из копии
                        selectedProduct.Name = productToEdit.Name;
                        selectedProduct.Category = productToEdit.Category;
                        selectedProduct.Price = productToEdit.Price;
                        selectedProduct.Stock = productToEdit.Stock;
                        selectedProduct.Unit = productToEdit.Unit;

                        // Теперь selectedProduct изменился, и так как он в BindingList, то DataGridView обновится
                        // НЕ перезагружаем всю таблицу из базы!
                    }
                }
                else
                {
                    MessageBox.Show("Товар не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для редактирования.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {
            // Обработчик для кнопки редактирования
        }

        private void AddInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                InvoiceForm invoiceForm = new InvoiceForm();
                invoiceForm.ShowDialog();

                dataGridView.DataSource = productManager.GetAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы накладной: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region Вкладка "Продажи"

        private void InitializeSalesTab()
        {
            salesCatalogManager = new ProductCatalogManager(productManager);
            cartItems = new List<Product>();
            selectedCategory = "Все";

            // Настройка DataGridView
            ConfigureProductsGrid();
            ConfigureCartGrid();

            // Загрузка начальных данных
            LoadSalesData();

            // Установка даты
            lblDateValue.Text = DateTime.Now.ToString("dd.MM.yyyy");
        }

        private void ConfigureProductsGrid()
        {
            dataGridViewProductsSales.ReadOnly = true;
            dataGridViewProductsSales.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewProductsSales.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewProductsSales.RowHeadersVisible = false;

            // Настройка внешнего вида
            dataGridViewProductsSales.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGray;
        }

        private void ConfigureCartGrid()
        {
            dataGridViewCart.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewCart.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCart.RowHeadersVisible = false;

            // Делаем колонку Stock редактируемой
            if (dataGridViewCart.Columns["Stock"] != null)
            {
                dataGridViewCart.Columns["Stock"].ReadOnly = false;
                dataGridViewCart.Columns["Stock"].HeaderText = "Количество";
            }
        }

        private void LoadSalesData()
        {
            try
            {
                // Загрузка категорий
                var categories = salesCatalogManager.GetCategories();
                UpdateCategoriesButtons(categories);

                // Загрузка всех товаров
                var allProducts = salesCatalogManager.GetAllProducts();
                dataGridViewProductsSales.DataSource = allProducts;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCategoriesButtons(List<string> categories)
        {
            flowLayoutPanelCategories.Controls.Clear();

            // Добавляем кнопку "Все"
            var allButton = CreateCategoryButton("Все");
            flowLayoutPanelCategories.Controls.Add(allButton);

            // Добавляем кнопки для каждой категории
            foreach (var category in categories)
            {
                var button = CreateCategoryButton(category);
                flowLayoutPanelCategories.Controls.Add(button);
            }
        }

        private Button CreateCategoryButton(string categoryName)
        {
            var button = new Button
            {
                Text = categoryName,
                Size = new Size(100, 35),
                Margin = new Padding(3),
                BackColor = categoryName == selectedCategory ?
                    SystemColors.Highlight : SystemColors.Control,
                ForeColor = categoryName == selectedCategory ?
                    Color.White : SystemColors.ControlText,
                FlatStyle = FlatStyle.Flat
            };

            button.FlatAppearance.BorderSize = 0;

            button.Click += (s, e) =>
            {
                selectedCategory = categoryName;
                UpdateCategoriesButtons(salesCatalogManager.GetCategories());
                LoadProductsByCategory(categoryName);
            };

            return button;
        }

        private void LoadProductsByCategory(string category)
        {
            try
            {
                List<Product> products;
                if (category == "Все")
                {
                    products = salesCatalogManager.GetAllProducts();
                }
                else
                {
                    products = salesCatalogManager.GetProductsByCategory(category);
                }
                dataGridViewProductsSales.DataSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewProductsSales_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dataGridViewProductsSales.RowCount)
            {
                var product = dataGridViewProductsSales.Rows[e.RowIndex].DataBoundItem as Product;
                if (product != null && product.Stock > 0)
                {
                    AddToCart(product);
                }
                else
                {
                    MessageBox.Show("Товара нет в наличии", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void AddToCart(Product product)
        {
            // Создаем копию товара для корзины
            var cartProduct = product.Clone();
            cartProduct.Stock = 1; // Начальное количество в корзине

            // Проверяем, есть ли уже такой товар в корзине
            var existingItem = cartItems.FirstOrDefault(p => p.Article == cartProduct.Article);
            if (existingItem != null)
            {
                var originalProduct = salesCatalogManager.GetAllProducts()
                    .FirstOrDefault(p => p.Article == cartProduct.Article);

                if (originalProduct != null && existingItem.Stock < originalProduct.Stock)
                {
                    existingItem.Stock++;
                }
                else
                {
                    MessageBox.Show("Недостаточно товара на складе", "Внимание",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                cartItems.Add(cartProduct);
            }

            UpdateCartGrid();
            UpdateTotalAmount();
        }

        private void UpdateCartGrid()
        {
            dataGridViewCart.DataSource = null;
            dataGridViewCart.DataSource = cartItems;
        }

        private void dataGridViewCart_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewCart.Columns["Stock"].Index)
            {
                var product = dataGridViewCart.Rows[e.RowIndex].DataBoundItem as Product;
                var allProducts = salesCatalogManager.GetAllProducts();
                var originalProduct = allProducts.FirstOrDefault(p => p.Article == product.Article);

                if (originalProduct != null && product.Stock > originalProduct.Stock)
                {
                    MessageBox.Show($"Недостаточно товара на складе. Доступно: {originalProduct.Stock}",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    product.Stock = originalProduct.Stock;
                    dataGridViewCart.Refresh();
                }

                UpdateTotalAmount();
            }
        }

        private void dataGridViewCart_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewCart.Columns["Stock"].Index)
            {
                var product = dataGridViewCart.Rows[e.RowIndex].DataBoundItem as Product;
                if (product.Stock <= 0)
                {
                    // Удаляем товар при нулевом количестве
                    cartItems.Remove(product);
                    UpdateCartGrid();
                }
                UpdateTotalAmount();
            }
        }

        private void UpdateTotalAmount()
        {
            decimal total = cartItems.Sum(item => item.Price * item.Stock);
            lblTotalValue.Text = total.ToString("N2");
        }

        private void btnRemoveFromCart_Click(object sender, EventArgs e)
        {
            if (dataGridViewCart.SelectedRows.Count > 0)
            {
                var selectedProduct = dataGridViewCart.SelectedRows[0].DataBoundItem as Product;
                if (selectedProduct != null)
                {
                    cartItems.Remove(selectedProduct);
                    UpdateCartGrid();
                    UpdateTotalAmount();
                }
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления из корзины", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefreshSales_Click(object sender, EventArgs e)
        {
            LoadSalesData();
            UpdateTotalAmount();
        }

        #endregion
    }
}