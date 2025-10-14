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
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}