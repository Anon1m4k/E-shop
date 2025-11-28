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
    public partial class InvoiceForm : Form
    {
        private InvoiceManager invoiceManager;
        private Invoice currentInvoice;
        private BindingList<InvoiceItem> invoiceItems;
        private List<string> availableUnits;
        private PrintableInvoiceCreator printableInvoiceCreator;
        public InvoiceForm()
        {
            InitializeComponent();
            InitializeForm();
        }
        public InvoiceForm(int invoiceId)
        {
            InitializeComponent();
            InitializeForm();
            LoadInvoice(invoiceId);
        }
        private void LoadInvoice(int invoiceId)
        {
            try
            {
                var repository = new SQLInvoiceRepository();
                currentInvoice = repository.GetInvoiceById(invoiceId);

                if (currentInvoice != null)
                {

                    SerialNumberInvoice.Text = currentInvoice.SerialNumber;
                    lblDate.Text = currentInvoice.Date.ToString("dd.MM.yyyy");
                    SerialNumberInvoice.ReadOnly = true;

                    invoiceItems.Clear();
                    foreach (var product in currentInvoice.Items)
                    {
                        invoiceItems.Add(new InvoiceItem
                        {
                            Article = product.Article,
                            Name = product.Name,
                            Category = product.Category,
                            Quantity = product.Stock,
                            Unit = product.Unit,
                            Price = product.Price
                        });
                    }

                    UpdateTotalAmount();
                }
                else
                {
                    MessageBox.Show("Накладная не найдена", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки накладной: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }
        private void InitializeForm()
        {
            invoiceManager = new InvoiceManager(new SQLInvoiceRepository());
            currentInvoice = invoiceManager.CreateNewInvoice();
            printableInvoiceCreator = new PrintableInvoiceCreator();

            lblDate.Text = currentInvoice.Date.ToString("dd.MM.yyyy");

            unitColumn.DataSource = Product.AvailableUnits;
            unitColumn.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            unitColumn.FlatStyle = FlatStyle.Flat;

            invoiceItems = new BindingList<InvoiceItem>();
            invoiceItems.ListChanged += InvoiceItems_ListChanged;
            dataGridViewItems.DataSource = invoiceItems;
            dataGridViewItems.DataError += dataGridViewItems_DataError;

        }
        private void dataGridViewItems_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception is ArgumentException && e.Context == DataGridViewDataErrorContexts.Commit)
            {
                if (dataGridViewItems.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                {
                    e.ThrowException = false;
                }
            }
            if (dataGridViewItems.Columns[e.ColumnIndex] == quantityColumn || dataGridViewItems.Columns[e.ColumnIndex] == priceColumn)
            {
                MessageBox.Show($"Некорректное значение в поле '{dataGridViewItems.Columns[e.ColumnIndex].HeaderText}'. " +
                               $"Введите числовое значение.", "Ошибка ввода",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.ThrowException = false;
            }
        }

        private void InvoiceItems_ListChanged(object sender, ListChangedEventArgs e)
        {
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

            string result;

            if (IsNewInvoice())
            {
                result = invoiceManager.AddInvoice(currentInvoice);
            }
            else
            {
                result = invoiceManager.UpdateInvoice(currentInvoice);
            }

            if (string.IsNullOrEmpty(result))
            {
                MessageBox.Show(IsNewInvoice() ?
                    "Накладная успешно добавлена" : "Накладная успешно обновлена",
                    "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show(result, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsNewInvoice()
        {

            return string.IsNullOrEmpty(SerialNumberInvoice.Text) ||
                   SerialNumberInvoice.ReadOnly == false;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
        }

        private void btnSavePdf_Click(object sender, EventArgs e)
        {
            if (invoiceItems.Count == 0)
            {
                MessageBox.Show("Добавьте товары в накладную", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            currentInvoice.SerialNumber = SerialNumberInvoice.Text?.Trim();

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PDF files (*.pdf)|*.pdf";
                dialog.FileName = $"Накладная_{currentInvoice.SerialNumber}.pdf";
                dialog.Title = "Сохранить PDF файл";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string html = printableInvoiceCreator.GenerateInvoiceHtml(currentInvoice, invoiceItems.ToList());
                    string result = printableInvoiceCreator.CreatePdfFromHtml(html, dialog.FileName);

                    if (string.IsNullOrEmpty(result))
                    {
                        MessageBox.Show($"PDF успешно сохранен:\n{dialog.FileName}", "Успех",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(result, "Ошибка сохранения PDF",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                if (invoiceItems.Count == 0)
                {
                    MessageBox.Show("Накладная пустая, добавьте позиции в накладную!", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                currentInvoice.SerialNumber = SerialNumberInvoice.Text?.Trim();

                string htmlContent = printableInvoiceCreator.GenerateInvoiceHtml(currentInvoice, invoiceItems.ToList());


                webBrowser1.Visible = true;
                panelPreviewHeader.Visible = true;

                webBrowser1.DocumentText = htmlContent;
                panelPreviewHeader.BringToFront();
                webBrowser1.BringToFront();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при предпросмотре: {ex.Message}", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BtnClosePreview_Click(object sender, EventArgs e)
        {
            // Скрываем элементы предпросмотра
            webBrowser1.Visible = false;
            panelPreviewHeader.Visible = false;

            // Показываем основные элементы формы
            dataGridViewItems.Visible = true;
            panel1.Visible = true;
            panel2.Visible = true;
            btnDelete.Visible = true;
            btnPrint.Visible = true;
            btnSavePdf.Visible = true;
            btnPreview.Visible = true;
        }
    }

}
