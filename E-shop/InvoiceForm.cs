using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using E_shopLib;
using E_shopLib1;

namespace E_shop
{
    public partial class InvoiceForm: Form
    {
        private InvoiceManager invoiceManager;
        private Invoice currentInvoice;
        private BindingList<InvoiceItem> invoiceItems;
        private List<string> availableUnits;
        public InvoiceForm()
        {
            InitializeComponent();
            InitializeForm();
        }
        private void InitializeForm()
        {
            invoiceManager = new InvoiceManager(new SQLInvoiceRepository());
            currentInvoice = invoiceManager.CreateNewInvoice();

            lblDate.Text = currentInvoice.Date.ToString("dd.MM.yyyy");

            unitColumn.DataSource = Product.AvailableUnits;
            unitColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            unitColumn.FlatStyle = FlatStyle.Flat;

            invoiceItems = new BindingList<InvoiceItem>();
            invoiceItems.ListChanged += InvoiceItems_ListChanged;
            dataGridViewItems.DataSource = invoiceItems;

            dataGridViewItems.CellEndEdit += dataGridViewItems_CellEndEdit;

        }
        
        private void InvoiceItems_ListChanged(object sender, ListChangedEventArgs e)
        {
            UpdateTotalAmount();
        }
        private void dataGridViewItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            InvoiceItem item = dataGridViewItems.Rows[e.RowIndex].DataBoundItem as InvoiceItem;
            if (item == null) return;

            UpdateTotalAmount();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {            
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (invoiceItems.Count == 0)
            {
                MessageBox.Show("Накладная должна содержать хотя бы один товар", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentInvoice.SerialNumber = SerialNumberInvoice.Text?.Trim(); 
            currentInvoice.Items = invoiceItems.Select(item => new Product
            {
                Article = item.Article?.Trim() ?? "",
                Name = item.Name?.Trim() ?? "",
                Category = item.Category?.Trim() ?? "", 
                Price = item.Price,
                Stock = item.Quantity,
                Unit = item.Unit?.Trim() ?? "" 
            }).ToList();

            string result = invoiceManager.AddInvoice(currentInvoice);

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show("Накладная успешно добавлена", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку для удаления", "Информация",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            InvoiceItem itemToRemove = dataGridViewItems.SelectedRows[0].DataBoundItem as InvoiceItem;
            if (itemToRemove == null) return;

            DialogResult result = MessageBox.Show($"Удалить товар '{itemToRemove.Name}'?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                invoiceItems.Remove(itemToRemove);
            }
        }
        private void UpdateTotalAmount()
        {
            decimal total = invoiceItems.Sum(item => item.Price * item.Quantity);
            lblTotalValue.Text = total.ToString("N2");
        }
    }
}
