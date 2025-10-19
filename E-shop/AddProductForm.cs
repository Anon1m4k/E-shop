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
            var product = new Product
            {
                Article = txtArticle.Text.Trim(),
                Name = txtName.Text.Trim(),
                Category = txtCategory.Text.Trim(),
                Price = numPrice.Value,
                Stock = (int)numStock.Value,
                Unit = txtUnit.Text.Trim()
            };

            string result = catalogManager.AddProduct(product);

            // ProductCatalogManager возвращает пустую строку при успехе
            // и строку с ошибкой при неудаче
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