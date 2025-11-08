using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
    }
}
