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
            ProductCatalogManager catalogManager = new ProductCatalogManager(productManager);
            AddProductForm addForm = new AddProductForm(catalogManager);
            if (addForm.ShowDialog() == DialogResult.OK)
            {
                // Обновляем таблицу после добавления
                dataGridView.DataSource = productManager.GetAllProducts();
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем артикул из выбранной строки
                string selectedArticle = dataGridView.SelectedRows[0].Cells["Article"].Value.ToString();

                // Запрашиваем подтверждение удаления
                DialogResult result = MessageBox.Show("Удалить выбранный товар?", "Подтверждение удаления",
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
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                // Получаем объект товара из выбранной строки
                Product selectedProduct = dataGridView.SelectedRows[0].DataBoundItem as Product;

                if (selectedProduct != null)
                {
                    // Создаем копию товара для редактирования
                    Product productToEdit = selectedProduct.Clone();

                    ProductCatalogManager catalogManager = new ProductCatalogManager(productManager);
                    EditProductForm editForm = new EditProductForm(catalogManager, productToEdit);
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        // Обновляем оригинальный товар в списке свойствами из копии
                        selectedProduct.Name = productToEdit.Name;
                        selectedProduct.Category = productToEdit.Category;
                        selectedProduct.Price = productToEdit.Price;
                        selectedProduct.Stock = productToEdit.Stock;
                        selectedProduct.Unit = productToEdit.Unit;

                        // Теперь selectedProduct изменился, и так как он в BindingList, то DataGridView обновится
                        // НЕ перезагружаем всю таблицу из базы!
                    }
                }
                else
                {
                    MessageBox.Show("Товар не найден", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите товар для редактирования.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonEdit_Click_1(object sender, EventArgs e)
        {

        }

        private void AddInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                InvoiceForm invoiceForm = new InvoiceForm();
                invoiceForm.ShowDialog();


                dataGridView.DataSource = productManager.GetAllProducts();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии формы накладной: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}