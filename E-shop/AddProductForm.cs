using System;
using System.Windows.Forms;
using E_shopLib;

namespace E_shop
{
    public partial class AddProductForm : Form
    {
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}