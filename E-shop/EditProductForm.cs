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

        public EditProductForm(ProductCatalogManager manager, Product product)
        {
            InitializeComponent();
            catalogManager = manager;
            productToEdit = product;

            // Заполняем поля формы данными товара
            txtArticle.Text = product.Article;
            txtName.Text = product.Name;
            txtCategory.Text = product.Category;
            numPrice.Value = product.Price;
            numStock.Value = product.Stock;
            txtUnit.Text = product.Unit;

            // Артикул нельзя редактировать - он является ключом
            txtArticle.ReadOnly = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var updatedProduct = new Product
            {
                Article = txtArticle.Text.Trim(), // Артикул остается прежним
                Name = txtName.Text.Trim(),
                Category = txtCategory.Text.Trim(),
                Price = numPrice.Value,
                Stock = (int)numStock.Value,
                Unit = txtUnit.Text.Trim()
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