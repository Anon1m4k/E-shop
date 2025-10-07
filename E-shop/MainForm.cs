using E_shopLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace E_shop
{
    public partial class MainForm : Form
    {
        private SQLProductManager productManager;
        private List<Product> products;

        public MainForm()
        {
            InitializeComponent();
            productManager = new SQLProductManager();
            LoadProducts();
        }

        private void LoadProducts()
        {
            products = productManager.GetAllProducts();
            dataGridView.DataSource = products;
            ConfigureDataGridView();
        }

        private void ConfigureDataGridView() //не очень хорошо сделать ручками
        {
            dataGridView.AutoGenerateColumns = true;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.ReadOnly = true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddProductForm addForm = new AddProductForm();
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                LoadProducts(); // Обновляем список после добавления
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                var result = MessageBox.Show("Удалить выбранный товар?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Получаем артикул из выбранной строки
                    string article = dataGridView.SelectedRows[0].Cells["Article"].Value.ToString();

                    // Создаем ProductCatalogManager и удаляем товар
                    ProductCatalogManager catalogManager = new ProductCatalogManager(productManager);
                    string deleteResult = catalogManager.DeleteProduct(article);

                    if (string.IsNullOrEmpty(deleteResult))
                    {
                        MessageBox.Show("Товар успешно удален", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProducts(); // Обновляем DataGridView
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
                MessageBox.Show("Выберите товар для удаления", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadProducts();
        }
    }
}