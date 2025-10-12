using E_shopLib;
using System;
using System.Windows.Forms;

namespace E_shop
{
    public partial class MainForm : Form
    {
        SQLProductManager productManager = new SQLProductManager();
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            dataGridView.DataSource = productManager.GetAllProducts();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                ProductCatalogManager catalogManager = new ProductCatalogManager(productManager);
                AddProductForm addForm = new AddProductForm(catalogManager);
                if (addForm.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем таблицу после добавления
                    dataGridView.DataSource = productManager.GetAllProducts();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
           
        }
    }
}