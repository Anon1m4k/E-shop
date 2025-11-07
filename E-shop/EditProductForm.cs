using E_shopLib;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml.Linq;

namespace E_shop
{
    public partial class EditProductForm : Form
    {
        private ProductCatalogManager catalogManager;
        private Product productToEdit;

        // Списки для ComboBox
        private string[] units = { "шт", "кг", "г", "л", "м", "упак", "пар" };

        public EditProductForm(ProductCatalogManager manager, Product product)
        {
            InitializeComponent();
            catalogManager = manager;
            productToEdit = product;
            InitializeComboBoxes();
            FillFormData();
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
        }
        private void FillFormData()
        {
            // Заполняем поля формы данными товара
            txtArticle.Text = productToEdit.Article;
            txtName.Text = productToEdit.Name;
            numPrice.Value = productToEdit.Price;
            numStock.Value = productToEdit.Stock;

            // Устанавливаем выбранные значения в ComboBox
            cmbUnit.Text = productToEdit.Unit;
            cmbCategory.Text = productToEdit.Category;

            // Артикул нельзя редактировать - он является ключом
            txtArticle.ReadOnly = true;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            // Обновляем объект товара новыми значениями
            productToEdit.Name = txtName.Text.Trim();
            productToEdit.Category = cmbCategory.Text.Trim();
            productToEdit.Price = numPrice.Value;
            productToEdit.Stock = (int)numStock.Value;
            productToEdit.Unit = cmbUnit.Text.Trim();

            string result = catalogManager.UpdateProduct(productToEdit);

            if (string.IsNullOrEmpty(result))
            {
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