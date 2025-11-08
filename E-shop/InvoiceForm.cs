using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            invoiceItems = new BindingList<InvoiceItem>();
            dataGridViewItems.DataSource = invoiceItems;

            dataGridViewItems.CellEndEdit += dataGridViewItems_CellEndEdit;

            UpdateTotalAmount();
        }
        private void dataGridViewItems_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var item = dataGridViewItems.Rows[e.RowIndex].DataBoundItem as InvoiceItem;
            if (item == null) return;

            UpdateTotalAmount();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
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

            var result = invoiceManager.AddInvoice(currentInvoice);

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
    }
}
