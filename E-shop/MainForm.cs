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
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем артикул из выбранной строки
                string selectedArticle = dataGridView.SelectedRows[0].Cells["Article"].Value.ToString();

                // Запрашиваем подтверждение удаления
                var result = MessageBox.Show("Удалить выбранный товар?", "Подтверждение удаления",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
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
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка",
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