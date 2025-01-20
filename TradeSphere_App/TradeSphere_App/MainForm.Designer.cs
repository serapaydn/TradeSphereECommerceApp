namespace TradeSphere_App
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.dosyaİşlemleriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_category = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_brand = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_product = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_employee = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_customer = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_supplier = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_ = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_purchases = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_sale = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_saledetail = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_dealer = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_icon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_close = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.cms_icon.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.White;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dosyaİşlemleriToolStripMenuItem,
            this.TSMI_category,
            this.TSMI_brand,
            this.TSMI_product,
            this.TSMI_employee,
            this.TSMI_customer,
            this.TSMI_supplier,
            this.TSMI_,
            this.TSMI_dealer});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1152, 28);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // dosyaİşlemleriToolStripMenuItem
            // 
            this.dosyaİşlemleriToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dosyaİşlemleriToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.dosyaİşlemleriToolStripMenuItem.Name = "dosyaİşlemleriToolStripMenuItem";
            this.dosyaİşlemleriToolStripMenuItem.Size = new System.Drawing.Size(124, 24);
            this.dosyaİşlemleriToolStripMenuItem.Text = "Dosya İşlemleri";
            // 
            // TSMI_category
            // 
            this.TSMI_category.Name = "TSMI_category";
            this.TSMI_category.Size = new System.Drawing.Size(97, 24);
            this.TSMI_category.Text = "Kategoriler";
            this.TSMI_category.Click += new System.EventHandler(this.TSMI_category_Click);
            // 
            // TSMI_brand
            // 
            this.TSMI_brand.Name = "TSMI_brand";
            this.TSMI_brand.Size = new System.Drawing.Size(81, 24);
            this.TSMI_brand.Text = "Markalar";
            this.TSMI_brand.Click += new System.EventHandler(this.TSMI_brand_Click);
            // 
            // TSMI_product
            // 
            this.TSMI_product.Name = "TSMI_product";
            this.TSMI_product.Size = new System.Drawing.Size(71, 24);
            this.TSMI_product.Text = "Ürünler";
            this.TSMI_product.Click += new System.EventHandler(this.TSMI_product_Click);
            // 
            // TSMI_employee
            // 
            this.TSMI_employee.Name = "TSMI_employee";
            this.TSMI_employee.Size = new System.Drawing.Size(87, 24);
            this.TSMI_employee.Text = "Çalışanlar";
            this.TSMI_employee.Click += new System.EventHandler(this.TSMI_employee_Click);
            // 
            // TSMI_customer
            // 
            this.TSMI_customer.Name = "TSMI_customer";
            this.TSMI_customer.Size = new System.Drawing.Size(89, 24);
            this.TSMI_customer.Text = "Müşteriler";
            this.TSMI_customer.Click += new System.EventHandler(this.TSMI_customer_Click);
            // 
            // TSMI_supplier
            // 
            this.TSMI_supplier.Name = "TSMI_supplier";
            this.TSMI_supplier.Size = new System.Drawing.Size(99, 24);
            this.TSMI_supplier.Text = "Tedarikçiler";
            this.TSMI_supplier.Click += new System.EventHandler(this.TSMI_supplier_Click);
            // 
            // TSMI_
            // 
            this.TSMI_.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_purchases,
            this.TSMI_sale,
            this.TSMI_saledetail});
            this.TSMI_.Name = "TSMI_";
            this.TSMI_.Size = new System.Drawing.Size(71, 24);
            this.TSMI_.Text = "Satışlar";
            // 
            // TSMI_purchases
            // 
            this.TSMI_purchases.Name = "TSMI_purchases";
            this.TSMI_purchases.Size = new System.Drawing.Size(187, 26);
            this.TSMI_purchases.Text = "Satın Alımlar";
            this.TSMI_purchases.Click += new System.EventHandler(this.TSMI_purchases_Click);
            // 
            // TSMI_sale
            // 
            this.TSMI_sale.Name = "TSMI_sale";
            this.TSMI_sale.Size = new System.Drawing.Size(187, 26);
            this.TSMI_sale.Text = "Satışlar";
            this.TSMI_sale.Click += new System.EventHandler(this.TSMI_sale_Click);
            // 
            // TSMI_saledetail
            // 
            this.TSMI_saledetail.Name = "TSMI_saledetail";
            this.TSMI_saledetail.Size = new System.Drawing.Size(187, 26);
            this.TSMI_saledetail.Text = "Satış Detayları";
            this.TSMI_saledetail.Click += new System.EventHandler(this.TSMI_saledetail_Click);
            // 
            // TSMI_dealer
            // 
            this.TSMI_dealer.Name = "TSMI_dealer";
            this.TSMI_dealer.Size = new System.Drawing.Size(68, 24);
            this.TSMI_dealer.Text = "Bayiler";
            this.TSMI_dealer.Click += new System.EventHandler(this.TSMI_dealer_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.cms_icon;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // cms_icon
            // 
            this.cms_icon.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms_icon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_close});
            this.cms_icon.Name = "cms_icon";
            this.cms_icon.Size = new System.Drawing.Size(118, 28);
            // 
            // TSMI_close
            // 
            this.TSMI_close.Name = "TSMI_close";
            this.TSMI_close.Size = new System.Drawing.Size(117, 24);
            this.TSMI_close.Text = "Kapat";
            this.TSMI_close.Click += new System.EventHandler(this.TSMI_close_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 608);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Trade Sphere";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cms_icon.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem dosyaİşlemleriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem TSMI_category;
        private System.Windows.Forms.ToolStripMenuItem TSMI_brand;
        private System.Windows.Forms.ToolStripMenuItem TSMI_product;
        private System.Windows.Forms.ToolStripMenuItem TSMI_employee;
        private System.Windows.Forms.ToolStripMenuItem TSMI_customer;
        private System.Windows.Forms.ToolStripMenuItem TSMI_supplier;
        private System.Windows.Forms.ToolStripMenuItem TSMI_;
        private System.Windows.Forms.ToolStripMenuItem TSMI_purchases;
        private System.Windows.Forms.ToolStripMenuItem TSMI_sale;
        private System.Windows.Forms.ToolStripMenuItem TSMI_saledetail;
        private System.Windows.Forms.ToolStripMenuItem TSMI_dealer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip cms_icon;
        private System.Windows.Forms.ToolStripMenuItem TSMI_close;
    }
}