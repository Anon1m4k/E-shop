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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем артикул из выбранной строки
                string selectedArticle = dataGridView.SelectedRows[0].Cells["Article"].Value.ToString();

                // Запрашиваем подтверждение удаления
                var result = MessageBox.Show("Удалить выбранный товар?", "Подтверждение удаления",
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
    }
}