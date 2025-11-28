using System.Windows.Forms;

namespace E_shop
{
    partial class InvoiceForm
    {
        private System.ComponentModel.IContainer components = null;
        private Label labelInvoiceNumber;
        private Label labelDate;
        private DataGridView dataGridViewItems;
        private Button btnSave;
        private Button btnCancel;
        private Label lblDate;
        private Label labelTotal;
        private Label lblTotalValue;
        private Panel panel1;
        private Panel panel2;
        private Button btnDelete;
        private TextBox SerialNumberInvoice;
        private Button btnPrint;
        private Button btnSavePdf;
        private Button btnPreview;
        private WebBrowser webBrowser1;
        private Panel panelPreviewHeader;
        private Button btnClosePreview;

        // Колонки DataGridView
        private DataGridViewTextBoxColumn articleColumn;
        private DataGridViewTextBoxColumn nameColumn;
        private DataGridViewTextBoxColumn categoryColumn;
        private DataGridViewTextBoxColumn quantityColumn;
        private DataGridViewComboBoxColumn unitColumn;
        private DataGridViewTextBoxColumn priceColumn;
        private DataGridViewTextBoxColumn totalColumn;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelInvoiceNumber = new System.Windows.Forms.Label();
            this.labelDate = new System.Windows.Forms.Label();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.articleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.categoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quantityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.unitColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.priceColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.totalColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblDate = new System.Windows.Forms.Label();
            this.labelTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SerialNumberInvoice = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSavePdf = new System.Windows.Forms.Button();
            this.btnPreview = new System.Windows.Forms.Button();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.panelPreviewHeader = new System.Windows.Forms.Panel();
            this.btnClosePreview = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelPreviewHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelInvoiceNumber
            // 
            this.labelInvoiceNumber.AutoSize = true;
            this.labelInvoiceNumber.ForeColor = System.Drawing.Color.White;
            this.labelInvoiceNumber.Location = new System.Drawing.Point(11, 12);
            this.labelInvoiceNumber.Name = "labelInvoiceNumber";
            this.labelInvoiceNumber.Size = new System.Drawing.Size(194, 16);
            this.labelInvoiceNumber.TabIndex = 0;
            this.labelInvoiceNumber.Text = "Серийный номер накладной:";
            // 
            // labelDate
            // 
            this.labelDate.AutoSize = true;
            this.labelDate.ForeColor = System.Drawing.Color.White;
            this.labelDate.Location = new System.Drawing.Point(460, 16);
            this.labelDate.Name = "labelDate";
            this.labelDate.Size = new System.Drawing.Size(42, 16);
            this.labelDate.TabIndex = 1;
            this.labelDate.Text = "Дата:";
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.articleColumn,
            this.nameColumn,
            this.categoryColumn,
            this.quantityColumn,
            this.unitColumn,
            this.priceColumn,
            this.totalColumn});
            this.dataGridViewItems.Location = new System.Drawing.Point(12, 130);
            this.dataGridViewItems.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.RowHeadersWidth = 51;
            this.dataGridViewItems.Size = new System.Drawing.Size(760, 330);
            this.dataGridViewItems.TabIndex = 2;
            // 
            // articleColumn
            // 
            this.articleColumn.DataPropertyName = "Article";
            this.articleColumn.HeaderText = "Артикул";
            this.articleColumn.MinimumWidth = 6;
            this.articleColumn.Name = "articleColumn";
            // 
            // nameColumn
            // 
            this.nameColumn.DataPropertyName = "Name";
            this.nameColumn.HeaderText = "Наименование";
            this.nameColumn.MinimumWidth = 6;
            this.nameColumn.Name = "nameColumn";
            // 
            // categoryColumn
            // 
            this.categoryColumn.DataPropertyName = "Category";
            this.categoryColumn.HeaderText = "Категория";
            this.categoryColumn.MinimumWidth = 6;
            this.categoryColumn.Name = "categoryColumn";
            // 
            // quantityColumn
            // 
            this.quantityColumn.DataPropertyName = "Quantity";
            this.quantityColumn.HeaderText = "Количество";
            this.quantityColumn.MinimumWidth = 6;
            this.quantityColumn.Name = "quantityColumn";
            // 
            // unitColumn
            // 
            this.unitColumn.DataPropertyName = "Unit";
            this.unitColumn.HeaderText = "Ед. изм.";
            this.unitColumn.MinimumWidth = 6;
            this.unitColumn.Name = "unitColumn";
            // 
            // priceColumn
            // 
            this.priceColumn.DataPropertyName = "Price";
            this.priceColumn.HeaderText = "Цена";
            this.priceColumn.MinimumWidth = 6;
            this.priceColumn.Name = "priceColumn";
            // 
            // totalColumn
            // 
            this.totalColumn.DataPropertyName = "Total";
            this.totalColumn.HeaderText = "Сумма";
            this.totalColumn.MinimumWidth = 6;
            this.totalColumn.Name = "totalColumn";
            this.totalColumn.ReadOnly = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(371, 10);
            this.btnSave.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(155, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(577, 10);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(147, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Отмена";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDate.ForeColor = System.Drawing.Color.White;
            this.lblDate.Location = new System.Drawing.Point(513, 12);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(90, 18);
            this.lblDate.TabIndex = 3;
            this.lblDate.Text = "01.01.2024";
            // 
            // labelTotal
            // 
            this.labelTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelTotal.AutoSize = true;
            this.labelTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelTotal.Location = new System.Drawing.Point(31, 15);
            this.labelTotal.Name = "labelTotal";
            this.labelTotal.Size = new System.Drawing.Size(121, 18);
            this.labelTotal.TabIndex = 2;
            this.labelTotal.Text = "Общая сумма:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalValue.Location = new System.Drawing.Point(165, 15);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(40, 18);
            this.lblTotalValue.TabIndex = 3;
            this.lblTotalValue.Text = "0.00";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panel1.Controls.Add(this.SerialNumberInvoice);
            this.panel1.Controls.Add(this.lblDate);
            this.panel1.Controls.Add(this.labelDate);
            this.panel1.Controls.Add(this.labelInvoiceNumber);
            this.panel1.Location = new System.Drawing.Point(12, 50);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(760, 39);
            this.panel1.TabIndex = 0;
            // 
            // SerialNumberInvoice
            // 
            this.SerialNumberInvoice.Location = new System.Drawing.Point(221, 7);
            this.SerialNumberInvoice.Margin = new System.Windows.Forms.Padding(4);
            this.SerialNumberInvoice.Name = "SerialNumberInvoice";
            this.SerialNumberInvoice.Size = new System.Drawing.Size(151, 22);
            this.SerialNumberInvoice.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.SystemColors.Control;
            this.panel2.Controls.Add(this.lblTotalValue);
            this.panel2.Controls.Add(this.labelTotal);
            this.panel2.Controls.Add(this.btnCancel);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Location = new System.Drawing.Point(12, 465);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(760, 50);
            this.panel2.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.Location = new System.Drawing.Point(12, 95);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(107, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Удалить";
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(12, 12);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(120, 35);
            this.btnPrint.TabIndex = 7;
            this.btnPrint.Text = "🖨️ Печать";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSavePdf
            // 
            this.btnSavePdf.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnSavePdf.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSavePdf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSavePdf.ForeColor = System.Drawing.Color.White;
            this.btnSavePdf.Location = new System.Drawing.Point(138, 12);
            this.btnSavePdf.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSavePdf.Name = "btnSavePdf";
            this.btnSavePdf.Size = new System.Drawing.Size(187, 35);
            this.btnSavePdf.TabIndex = 8;
            this.btnSavePdf.Text = "📄 Сохранить PDF";
            this.btnSavePdf.UseVisualStyleBackColor = false;
            this.btnSavePdf.Click += new System.EventHandler(this.btnSavePdf_Click);
            // 
            // btnPreview
            // 
            this.btnPreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(196)))), ((int)(((byte)(15)))));
            this.btnPreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPreview.ForeColor = System.Drawing.Color.White;
            this.btnPreview.Location = new System.Drawing.Point(331, 12);
            this.btnPreview.Name = "btnPreview";
            this.btnPreview.Size = new System.Drawing.Size(160, 35);
            this.btnPreview.TabIndex = 9;
            this.btnPreview.Text = "👁️ Предпросмотр";
            this.btnPreview.UseVisualStyleBackColor = false;
            this.btnPreview.Click += new System.EventHandler(this.btnPreview_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(0, 50);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(784, 477);
            this.webBrowser1.TabIndex = 10;
            this.webBrowser1.Visible = false;
            // 
            // panelPreviewHeader
            // 
            this.panelPreviewHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.panelPreviewHeader.Controls.Add(this.btnClosePreview);
            this.panelPreviewHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPreviewHeader.Location = new System.Drawing.Point(0, 0);
            this.panelPreviewHeader.Name = "panelPreviewHeader";
            this.panelPreviewHeader.Size = new System.Drawing.Size(784, 50);
            this.panelPreviewHeader.TabIndex = 11;
            this.panelPreviewHeader.Visible = false;
            // 
            // btnClosePreview
            // 
            this.btnClosePreview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnClosePreview.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClosePreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnClosePreview.ForeColor = System.Drawing.Color.White;
            this.btnClosePreview.Location = new System.Drawing.Point(20, 8);
            this.btnClosePreview.Name = "btnClosePreview";
            this.btnClosePreview.Size = new System.Drawing.Size(180, 35);
            this.btnClosePreview.TabIndex = 0;
            this.btnClosePreview.Text = "✕ Закрыть предпросмотр";
            this.btnClosePreview.UseVisualStyleBackColor = false;
            this.btnClosePreview.Click += new System.EventHandler(this.BtnClosePreview_Click);
            // 
            // InvoiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(784, 527);
            this.Controls.Add(this.panelPreviewHeader);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.btnPreview);
            this.Controls.Add(this.btnSavePdf);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MinimumSize = new System.Drawing.Size(800, 575);
            this.Name = "InvoiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Накладная";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panelPreviewHeader.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}