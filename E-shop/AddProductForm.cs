using System;
using System.Windows.Forms;
using E_shopLib;

namespace E_shop
{
    public partial class AddProductForm : Form
    {
        private ProductCatalogManager catalogManager;

        public Product NewProduct { get; private set; }

        public AddProductForm(ProductCatalogManager manager)
        {
            InitializeComponent();
            catalogManager = manager;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var product = new Product
                {
                    Article = txtArticle.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    Category = txtCategory.Text.Trim(),
                    Price = decimal.Parse(txtPrice.Text),
                    Stock = (int)numStock.Value,
                    Unit = txtUnit.Text.Trim()
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
                    MessageBox.Show(result, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Проверьте правильность ввода цены", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}