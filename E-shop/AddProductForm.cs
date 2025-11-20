using System;
using System.Collections.Generic;
using System.Windows.Forms;
using E_shopLib;

namespace E_shop
{
    public partial class AddProductForm : Form
    {
        private ProductCatalogManager catalogManager;

        public Product NewProduct { get; private set; }

        // Список для единиц измерения
        private string[] units = { "шт", "кг", "г", "л", "м", "упак", "пар" };

        public AddProductForm(ProductCatalogManager manager)
        {
            InitializeComponent();
            catalogManager = manager;
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            // Заполняем ComboBox для единиц измерения
            cmbUnit.Items.AddRange(units);

            // Заполняем ComboBox для категорий из базы данных
            try
            {
                List<string> categories = catalogManager.GetCategories();
                cmbCategory.Items.AddRange(categories.ToArray());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки категорий: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Установим значения по умолчанию
            if (cmbUnit.Items.Count > 0)
                cmbUnit.SelectedIndex = 0;

            if (cmbCategory.Items.Count > 0)
                cmbCategory.SelectedIndex = 0;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Product product = new Product
            {
                Article = txtArticle.Text.Trim(),
                Name = txtName.Text.Trim(),
                Category = cmbCategory.Text.Trim(),
                Price = numPrice.Value,
                Stock = (int)numStock.Value,
                Unit = cmbUnit.Text.Trim()
            };

            string result = catalogManager.AddProduct(product);

            if (string.IsNullOrEmpty(result))
            {
                NewProduct = product;
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}