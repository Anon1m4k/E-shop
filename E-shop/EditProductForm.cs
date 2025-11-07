using E_shopLib;
using System;
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
        private string[] categories = { "Одежда", "Обувь", "Аксессуары", "Электроника", "Книги", "Игрушки", "Продукты" };

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

            // Заполняем ComboBox для категорий
            cmbCategory.Items.AddRange(categories);
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
            var updatedProduct = new Product
            {
                Article = txtArticle.Text.Trim(), // Артикул остается прежним
                Name = txtName.Text.Trim(),
                Category = cmbCategory.Text.Trim(),
                Price = numPrice.Value,
                Stock = (int)numStock.Value,
                Unit = cmbUnit.Text.Trim()
            };

            string result = catalogManager.UpdateProduct(updatedProduct);

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