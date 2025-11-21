namespace E_shop
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.tabControlMain = new System.Windows.Forms.TabControl();
            this.tabPageProducts = new System.Windows.Forms.TabPage();
            this.panelProductsButtons = new System.Windows.Forms.Panel();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.tabPageInvoices = new System.Windows.Forms.TabPage();
            this.AddInvoice = new System.Windows.Forms.Button();
            this.tabPageSales = new System.Windows.Forms.TabPage();
            this.splitContainerSales = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanelCategories = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRefreshSales = new System.Windows.Forms.Button();
            this.lblSalesTitle = new System.Windows.Forms.Label();
            this.dataGridViewProductsSales = new System.Windows.Forms.DataGridView();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.dataGridViewCart = new System.Windows.Forms.DataGridView();
            this.btnRemoveFromCart = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblClient = new System.Windows.Forms.Label();
            this.txtClient = new System.Windows.Forms.TextBox();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblDateValue = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.tabControlMain.SuspendLayout();
            this.tabPageProducts.SuspendLayout();
            this.panelProductsButtons.SuspendLayout();
            this.tabPageInvoices.SuspendLayout();
            this.tabPageSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSales)).BeginInit();
            this.splitContainerSales.Panel1.SuspendLayout();
            this.splitContainerSales.Panel2.SuspendLayout();
            this.splitContainerSales.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductsSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCart)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle10;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(107)))), ((int)(((byte)(129)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Padding = new System.Windows.Forms.Padding(0, 8, 0, 8);
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.dataGridView.ColumnHeadersHeight = 40;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(4, 2, 4, 2);
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(235)))), ((int)(((byte)(252)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle12;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView.Location = new System.Drawing.Point(0, 61);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.RowHeadersWidth = 51;
            this.dataGridView.RowTemplate.Height = 32;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(999, 302);
            this.dataGridView.TabIndex = 0;
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.buttonAdd.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.buttonAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.buttonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(159)))), ((int)(((byte)(56)))));
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.ForeColor = System.Drawing.Color.White;
            this.buttonAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAdd.Location = new System.Drawing.Point(7, 5);
            this.buttonAdd.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(181, 35);
            this.buttonAdd.TabIndex = 1;
            this.buttonAdd.Text = "➕ Добавить товар";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.buttonDelete.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.buttonDelete.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.buttonDelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(57)))), ((int)(((byte)(53)))));
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonDelete.ForeColor = System.Drawing.Color.White;
            this.buttonDelete.Location = new System.Drawing.Point(196, 5);
            this.buttonDelete.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(185, 35);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "🗑️ Удалить";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // tabControlMain
            // 
            this.tabControlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlMain.Controls.Add(this.tabPageProducts);
            this.tabControlMain.Controls.Add(this.tabPageInvoices);
            this.tabControlMain.Controls.Add(this.tabPageSales);
            this.tabControlMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabControlMain.ItemSize = new System.Drawing.Size(120, 30);
            this.tabControlMain.Location = new System.Drawing.Point(12, 12);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1439, 401);
            this.tabControlMain.TabIndex = 5;
            // 
            // tabPageProducts
            // 
            this.tabPageProducts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tabPageProducts.Controls.Add(this.dataGridView);
            this.tabPageProducts.Controls.Add(this.panelProductsButtons);
            this.tabPageProducts.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPageProducts.Location = new System.Drawing.Point(4, 34);
            this.tabPageProducts.Name = "tabPageProducts";
            this.tabPageProducts.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageProducts.Size = new System.Drawing.Size(999, 363);
            this.tabPageProducts.TabIndex = 0;
            this.tabPageProducts.Text = "📦 Товары";
            // 
            // panelProductsButtons
            // 
            this.panelProductsButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelProductsButtons.BackColor = System.Drawing.Color.Transparent;
            this.panelProductsButtons.Controls.Add(this.buttonEdit);
            this.panelProductsButtons.Controls.Add(this.buttonAdd);
            this.panelProductsButtons.Controls.Add(this.buttonDelete);
            this.panelProductsButtons.Location = new System.Drawing.Point(0, 5);
            this.panelProductsButtons.Name = "panelProductsButtons";
            this.panelProductsButtons.Size = new System.Drawing.Size(999, 40);
            this.panelProductsButtons.TabIndex = 1;
            // 
            // buttonEdit
            // 
            this.buttonEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.buttonEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonEdit.ForeColor = System.Drawing.Color.White;
            this.buttonEdit.Location = new System.Drawing.Point(389, 3);
            this.buttonEdit.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(185, 37);
            this.buttonEdit.TabIndex = 2;
            this.buttonEdit.Text = "✏️ Редактировать";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click_1);
            // 
            // tabPageInvoices
            // 
            this.tabPageInvoices.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tabPageInvoices.Controls.Add(this.AddInvoice);
            this.tabPageInvoices.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPageInvoices.Location = new System.Drawing.Point(4, 34);
            this.tabPageInvoices.Name = "tabPageInvoices";
            this.tabPageInvoices.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageInvoices.Size = new System.Drawing.Size(999, 363);
            this.tabPageInvoices.TabIndex = 1;
            this.tabPageInvoices.Text = "📋 Накладные";
            // 
            // AddInvoice
            // 
            this.AddInvoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.AddInvoice.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.AddInvoice.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(142)))), ((int)(((byte)(60)))));
            this.AddInvoice.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(104)))), ((int)(((byte)(159)))), ((int)(((byte)(56)))));
            this.AddInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddInvoice.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.AddInvoice.ForeColor = System.Drawing.Color.White;
            this.AddInvoice.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.AddInvoice.Location = new System.Drawing.Point(7, 6);
            this.AddInvoice.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddInvoice.Name = "AddInvoice";
            this.AddInvoice.Size = new System.Drawing.Size(140, 35);
            this.AddInvoice.TabIndex = 2;
            this.AddInvoice.Text = "➕ Добавить ";
            this.AddInvoice.UseVisualStyleBackColor = false;
            this.AddInvoice.Click += new System.EventHandler(this.AddInvoice_Click);
            // 
            // tabPageSales
            // 
            this.tabPageSales.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.tabPageSales.Controls.Add(this.splitContainerSales);
            this.tabPageSales.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tabPageSales.Location = new System.Drawing.Point(4, 34);
            this.tabPageSales.Name = "tabPageSales";
            this.tabPageSales.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSales.Size = new System.Drawing.Size(1431, 363);
            this.tabPageSales.TabIndex = 2;
            this.tabPageSales.Text = "💰 Продажи";
            // 
            // splitContainerSales
            // 
            this.splitContainerSales.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerSales.Location = new System.Drawing.Point(3, 3);
            this.splitContainerSales.Name = "splitContainerSales";
            // 
            // splitContainerSales.Panel1
            // 
            this.splitContainerSales.Panel1.Controls.Add(this.flowLayoutPanelCategories);
            this.splitContainerSales.Panel1.Controls.Add(this.btnRefreshSales);
            this.splitContainerSales.Panel1.Controls.Add(this.lblSalesTitle);
            this.splitContainerSales.Panel1.Controls.Add(this.dataGridViewProductsSales);
            // 
            // splitContainerSales.Panel2
            // 
            this.splitContainerSales.Panel2.Controls.Add(this.lblCartTitle);
            this.splitContainerSales.Panel2.Controls.Add(this.dataGridViewCart);
            this.splitContainerSales.Panel2.Controls.Add(this.btnRemoveFromCart);
            this.splitContainerSales.Panel2.Controls.Add(this.lblTotal);
            this.splitContainerSales.Panel2.Controls.Add(this.lblTotalValue);
            this.splitContainerSales.Panel2.Controls.Add(this.lblClient);
            this.splitContainerSales.Panel2.Controls.Add(this.txtClient);
            this.splitContainerSales.Panel2.Controls.Add(this.lblDate);
            this.splitContainerSales.Panel2.Controls.Add(this.lblDateValue);
            this.splitContainerSales.Size = new System.Drawing.Size(1425, 357);
            this.splitContainerSales.SplitterDistance = 711;
            this.splitContainerSales.TabIndex = 0;
            // 
            // flowLayoutPanelCategories
            // 
            this.flowLayoutPanelCategories.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flowLayoutPanelCategories.AutoScroll = true;
            this.flowLayoutPanelCategories.Location = new System.Drawing.Point(6, 25);
            this.flowLayoutPanelCategories.Name = "flowLayoutPanelCategories";
            this.flowLayoutPanelCategories.Size = new System.Drawing.Size(162, 329);
            this.flowLayoutPanelCategories.TabIndex = 0;
            // 
            // btnRefreshSales
            // 
            this.btnRefreshSales.Location = new System.Drawing.Point(174, 8);
            this.btnRefreshSales.Name = "btnRefreshSales";
            this.btnRefreshSales.Size = new System.Drawing.Size(94, 44);
            this.btnRefreshSales.TabIndex = 3;
            this.btnRefreshSales.Text = "Обновить";
            this.btnRefreshSales.UseVisualStyleBackColor = true;
            this.btnRefreshSales.Click += new System.EventHandler(this.btnRefreshSales_Click);
            // 
            // lblSalesTitle
            // 
            this.lblSalesTitle.AutoSize = true;
            this.lblSalesTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSalesTitle.Location = new System.Drawing.Point(3, 7);
            this.lblSalesTitle.Name = "lblSalesTitle";
            this.lblSalesTitle.Size = new System.Drawing.Size(100, 15);
            this.lblSalesTitle.TabIndex = 2;
            this.lblSalesTitle.Text = "Каталог товаров";
            // 
            // dataGridViewProductsSales
            // 
            this.dataGridViewProductsSales.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewProductsSales.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProductsSales.Location = new System.Drawing.Point(174, 58);
            this.dataGridViewProductsSales.Name = "dataGridViewProductsSales";
            this.dataGridViewProductsSales.Size = new System.Drawing.Size(534, 234);
            this.dataGridViewProductsSales.TabIndex = 1;
            this.dataGridViewProductsSales.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProductsSales_CellDoubleClick);
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.AutoSize = true;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblCartTitle.Location = new System.Drawing.Point(3, 40);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(56, 15);
            this.lblCartTitle.TabIndex = 9;
            this.lblCartTitle.Text = "Корзина";
            // 
            // dataGridViewCart
            // 
            this.dataGridViewCart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewCart.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCart.Location = new System.Drawing.Point(6, 58);
            this.dataGridViewCart.Name = "dataGridViewCart";
            this.dataGridViewCart.Size = new System.Drawing.Size(701, 230);
            this.dataGridViewCart.TabIndex = 8;
            this.dataGridViewCart.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCart_CellEndEdit);
            this.dataGridViewCart.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewCart_CellValueChanged);
            // 
            // btnRemoveFromCart
            // 
            this.btnRemoveFromCart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRemoveFromCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemoveFromCart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveFromCart.ForeColor = System.Drawing.Color.White;
            this.btnRemoveFromCart.Location = new System.Drawing.Point(6, 294);
            this.btnRemoveFromCart.Name = "btnRemoveFromCart";
            this.btnRemoveFromCart.Size = new System.Drawing.Size(120, 30);
            this.btnRemoveFromCart.TabIndex = 7;
            this.btnRemoveFromCart.Text = "Удалить из корзины";
            this.btnRemoveFromCart.UseVisualStyleBackColor = false;
            this.btnRemoveFromCart.Click += new System.EventHandler(this.btnRemoveFromCart_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotal.Location = new System.Drawing.Point(3, 330);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(89, 15);
            this.lblTotal.TabIndex = 5;
            this.lblTotal.Text = "Общая сумма:";
            // 
            // lblTotalValue
            // 
            this.lblTotalValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTotalValue.Location = new System.Drawing.Point(100, 330);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(31, 15);
            this.lblTotalValue.TabIndex = 6;
            this.lblTotalValue.Text = "0.00";
            // 
            // lblClient
            // 
            this.lblClient.AutoSize = true;
            this.lblClient.Location = new System.Drawing.Point(3, 10);
            this.lblClient.Name = "lblClient";
            this.lblClient.Size = new System.Drawing.Size(49, 15);
            this.lblClient.TabIndex = 3;
            this.lblClient.Text = "Клиент:";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(58, 7);
            this.txtClient.Name = "txtClient";
            this.txtClient.Size = new System.Drawing.Size(200, 23);
            this.txtClient.TabIndex = 4;
            // 
            // lblDate
            // 
            this.lblDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(567, 10);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(35, 15);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "Дата:";
            // 
            // lblDateValue
            // 
            this.lblDateValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblDateValue.AutoSize = true;
            this.lblDateValue.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblDateValue.Location = new System.Drawing.Point(608, 10);
            this.lblDateValue.Name = "lblDateValue";
            this.lblDateValue.Size = new System.Drawing.Size(0, 15);
            this.lblDateValue.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1463, 425);
            this.Controls.Add(this.tabControlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Электрон - Магазин техники";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.tabControlMain.ResumeLayout(false);
            this.tabPageProducts.ResumeLayout(false);
            this.panelProductsButtons.ResumeLayout(false);
            this.tabPageInvoices.ResumeLayout(false);
            this.tabPageSales.ResumeLayout(false);
            this.splitContainerSales.Panel1.ResumeLayout(false);
            this.splitContainerSales.Panel1.PerformLayout();
            this.splitContainerSales.Panel2.ResumeLayout(false);
            this.splitContainerSales.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerSales)).EndInit();
            this.splitContainerSales.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProductsSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageProducts;
        private System.Windows.Forms.Panel panelProductsButtons;
        private System.Windows.Forms.TabPage tabPageInvoices;
        private System.Windows.Forms.Button AddInvoice;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.TabPage tabPageSales;
        private System.Windows.Forms.SplitContainer splitContainerSales;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelCategories;
        private System.Windows.Forms.Button btnRefreshSales;
        private System.Windows.Forms.Label lblSalesTitle;
        private System.Windows.Forms.DataGridView dataGridViewProductsSales;
        private System.Windows.Forms.Label lblCartTitle;
        private System.Windows.Forms.DataGridView dataGridViewCart;
        private System.Windows.Forms.Button btnRemoveFromCart;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Label lblClient;
        private System.Windows.Forms.TextBox txtClient;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblDateValue;
    }
}