using E_shopLib;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace E_shop
{
    public partial class MainForm : Form
    {
        SQLProductManager productManager = new SQLProductManager();

        // Поля для вкладки продаж
        private ProductCatalogManager salesCatalogManager;
        private List<Product> cartItems;
        private string selectedCategory;

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
                InitializeSalesTab(); // Инициализация вкладки продаж
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}