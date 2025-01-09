namespace TradeSphere_App
{
    partial class CustomerForm
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_grade = new System.Windows.Forms.ComboBox();
            this.mtb_fax = new System.Windows.Forms.MaskedTextBox();
            this.mtb_phone = new System.Windows.Forms.MaskedTextBox();
            this.btn_pdf = new System.Windows.Forms.Button();
            this.tb_adddress = new System.Windows.Forms.TextBox();
            this.tb_contactname = new System.Windows.Forms.TextBox();
            this.tb_city = new System.Windows.Forms.TextBox();
            this.tb_companyname = new System.Windows.Forms.TextBox();
            this.tb_dealerid = new System.Windows.Forms.TextBox();
            this.tb_ID = new System.Windows.Forms.TextBox();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(12, 286);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1128, 310);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Müşteriler";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(1094, 275);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.cb_grade);
            this.groupBox1.Controls.Add(this.mtb_fax);
            this.groupBox1.Controls.Add(this.mtb_phone);
            this.groupBox1.Controls.Add(this.btn_pdf);
            this.groupBox1.Controls.Add(this.tb_adddress);
            this.groupBox1.Controls.Add(this.tb_contactname);
            this.groupBox1.Controls.Add(this.tb_city);
            this.groupBox1.Controls.Add(this.tb_companyname);
            this.groupBox1.Controls.Add(this.tb_dealerid);
            this.groupBox1.Controls.Add(this.tb_ID);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1128, 268);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Müşteri Ekle";
            // 
            // cb_grade
            // 
            this.cb_grade.FormattingEnabled = true;
            this.cb_grade.Items.AddRange(new object[] {
            "Gold",
            "Silver",
            "Bronz"});
            this.cb_grade.Location = new System.Drawing.Point(445, 102);
            this.cb_grade.Name = "cb_grade";
            this.cb_grade.Size = new System.Drawing.Size(202, 24);
            this.cb_grade.TabIndex = 7;
            // 
            // mtb_fax
            // 
            this.mtb_fax.Location = new System.Drawing.Point(731, 154);
            this.mtb_fax.Mask = "(999) 000-0000";
            this.mtb_fax.Name = "mtb_fax";
            this.mtb_fax.Size = new System.Drawing.Size(98, 22);
            this.mtb_fax.TabIndex = 6;
            // 
            // mtb_phone
            // 
            this.mtb_phone.Location = new System.Drawing.Point(731, 112);
            this.mtb_phone.Mask = "(999) 000-0000";
            this.mtb_phone.Name = "mtb_phone";
            this.mtb_phone.Size = new System.Drawing.Size(98, 22);
            this.mtb_phone.TabIndex = 6;
            // 
            // btn_pdf
            // 
            this.btn_pdf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_pdf.Location = new System.Drawing.Point(922, 227);
            this.btn_pdf.Name = "btn_pdf";
            this.btn_pdf.Size = new System.Drawing.Size(189, 35);
            this.btn_pdf.TabIndex = 1;
            this.btn_pdf.Text = "Pdf Oluştur";
            this.btn_pdf.UseVisualStyleBackColor = true;
            this.btn_pdf.Click += new System.EventHandler(this.btn_pdf_Click);
            // 
            // tb_adddress
            // 
            this.tb_adddress.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_adddress.Location = new System.Drawing.Point(445, 145);
            this.tb_adddress.Multiline = true;
            this.tb_adddress.Name = "tb_adddress";
            this.tb_adddress.Size = new System.Drawing.Size(202, 78);
            this.tb_adddress.TabIndex = 5;
            // 
            // tb_contactname
            // 
            this.tb_contactname.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_contactname.Location = new System.Drawing.Point(445, 48);
            this.tb_contactname.Name = "tb_contactname";
            this.tb_contactname.Size = new System.Drawing.Size(202, 28);
            this.tb_contactname.TabIndex = 5;
            // 
            // tb_city
            // 
            this.tb_city.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_city.Location = new System.Drawing.Point(731, 51);
            this.tb_city.Name = "tb_city";
            this.tb_city.Size = new System.Drawing.Size(202, 28);
            this.tb_city.TabIndex = 5;
            // 
            // tb_companyname
            // 
            this.tb_companyname.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_companyname.Location = new System.Drawing.Point(111, 152);
            this.tb_companyname.Name = "tb_companyname";
            this.tb_companyname.Size = new System.Drawing.Size(202, 28);
            this.tb_companyname.TabIndex = 5;
            // 
            // tb_dealerid
            // 
            this.tb_dealerid.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_dealerid.Location = new System.Drawing.Point(108, 102);
            this.tb_dealerid.Name = "tb_dealerid";
            this.tb_dealerid.Size = new System.Drawing.Size(202, 28);
            this.tb_dealerid.TabIndex = 4;
            // 
            // tb_ID
            // 
            this.tb_ID.Enabled = false;
            this.tb_ID.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_ID.Location = new System.Drawing.Point(108, 48);
            this.tb_ID.Name = "tb_ID";
            this.tb_ID.Size = new System.Drawing.Size(202, 28);
            this.tb_ID.TabIndex = 4;
            // 
            // btn_edit
            // 
            this.btn_edit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_edit.Location = new System.Drawing.Point(922, 186);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(189, 35);
            this.btn_edit.TabIndex = 3;
            this.btn_edit.Text = "Müşteri Düzenle";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Visible = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_add.Location = new System.Drawing.Point(922, 186);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(189, 35);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Müşteri Ekle";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(334, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "İletişim Adı:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(687, 156);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 21);
            this.label8.TabIndex = 1;
            this.label8.Text = "Fax:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(661, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(64, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Telefon:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(371, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "Adres:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(364, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "Derece:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(675, 55);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(50, 21);
            this.label9.TabIndex = 1;
            this.label9.Text = "Şehir:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(21, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Firma Adı:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(40, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Bayi ID:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(73, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSMI_edit,
            this.TSMI_delete});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(133, 52);
            // 
            // TSMI_edit
            // 
            this.TSMI_edit.Name = "TSMI_edit";
            this.TSMI_edit.Size = new System.Drawing.Size(132, 24);
            this.TSMI_edit.Text = "Düzenle";
            this.TSMI_edit.Click += new System.EventHandler(this.TSMI_edit_Click);
            // 
            // TSMI_delete
            // 
            this.TSMI_delete.Name = "TSMI_delete";
            this.TSMI_delete.Size = new System.Drawing.Size(132, 24);
            this.TSMI_delete.Text = "Sil";
            this.TSMI_delete.Click += new System.EventHandler(this.TSMI_delete_Click);
            // 
            // CustomerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 608);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "CustomerForm";
            this.Text = "CustomerForm";
            this.Load += new System.EventHandler(this.CustomerForm_Load);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_pdf;
        private System.Windows.Forms.TextBox tb_companyname;
        private System.Windows.Forms.TextBox tb_ID;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtb_phone;
        private System.Windows.Forms.TextBox tb_adddress;
        private System.Windows.Forms.TextBox tb_contactname;
        private System.Windows.Forms.TextBox tb_dealerid;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cb_grade;
        private System.Windows.Forms.MaskedTextBox mtb_fax;
        private System.Windows.Forms.TextBox tb_city;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_edit;
        private System.Windows.Forms.ToolStripMenuItem TSMI_delete;
    }
}