namespace TradeSphere_App
{
    partial class DealerForm
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
            this.cb_dealertype = new System.Windows.Forms.ComboBox();
            this.mtb_phone = new System.Windows.Forms.MaskedTextBox();
            this.btn_pdf = new System.Windows.Forms.Button();
            this.tb_discountrate = new System.Windows.Forms.TextBox();
            this.tb_mail = new System.Windows.Forms.TextBox();
            this.tb_dealername = new System.Windows.Forms.TextBox();
            this.tb_ID = new System.Windows.Forms.TextBox();
            this.btn_edit = new System.Windows.Forms.Button();
            this.btn_add = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TSMI_edit = new System.Windows.Forms.ToolStripMenuItem();
            this.TSMI_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_dealercode = new System.Windows.Forms.TextBox();
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
            this.groupBox2.Text = "Bayiler";
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
            this.groupBox1.Controls.Add(this.cb_dealertype);
            this.groupBox1.Controls.Add(this.mtb_phone);
            this.groupBox1.Controls.Add(this.btn_pdf);
            this.groupBox1.Controls.Add(this.tb_discountrate);
            this.groupBox1.Controls.Add(this.tb_mail);
            this.groupBox1.Controls.Add(this.tb_dealercode);
            this.groupBox1.Controls.Add(this.tb_dealername);
            this.groupBox1.Controls.Add(this.tb_ID);
            this.groupBox1.Controls.Add(this.btn_edit);
            this.groupBox1.Controls.Add(this.btn_add);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1128, 268);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bayi Ekle";
            // 
            // cb_dealertype
            // 
            this.cb_dealertype.FormattingEnabled = true;
            this.cb_dealertype.Items.AddRange(new object[] {
            "Gold",
            "Silver",
            "Bronz"});
            this.cb_dealertype.Location = new System.Drawing.Point(119, 191);
            this.cb_dealertype.Name = "cb_dealertype";
            this.cb_dealertype.Size = new System.Drawing.Size(201, 24);
            this.cb_dealertype.TabIndex = 7;
            // 
            // mtb_phone
            // 
            this.mtb_phone.Location = new System.Drawing.Point(467, 53);
            this.mtb_phone.Mask = "(999) 000-0000";
            this.mtb_phone.Name = "mtb_phone";
            this.mtb_phone.Size = new System.Drawing.Size(202, 22);
            this.mtb_phone.TabIndex = 6;
            // 
            // btn_pdf
            // 
            this.btn_pdf.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_pdf.Location = new System.Drawing.Point(986, 227);
            this.btn_pdf.Name = "btn_pdf";
            this.btn_pdf.Size = new System.Drawing.Size(125, 35);
            this.btn_pdf.TabIndex = 1;
            this.btn_pdf.Text = "Pdf Oluştur";
            this.btn_pdf.UseVisualStyleBackColor = true;
            this.btn_pdf.Click += new System.EventHandler(this.btn_pdf_Click);
            // 
            // tb_discountrate
            // 
            this.tb_discountrate.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_discountrate.Location = new System.Drawing.Point(467, 187);
            this.tb_discountrate.Name = "tb_discountrate";
            this.tb_discountrate.Size = new System.Drawing.Size(202, 28);
            this.tb_discountrate.TabIndex = 5;
            // 
            // tb_mail
            // 
            this.tb_mail.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_mail.Location = new System.Drawing.Point(467, 120);
            this.tb_mail.Name = "tb_mail";
            this.tb_mail.Size = new System.Drawing.Size(202, 28);
            this.tb_mail.TabIndex = 5;
            // 
            // tb_dealername
            // 
            this.tb_dealername.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_dealername.Location = new System.Drawing.Point(118, 117);
            this.tb_dealername.Name = "tb_dealername";
            this.tb_dealername.Size = new System.Drawing.Size(202, 28);
            this.tb_dealername.TabIndex = 5;
            // 
            // tb_ID
            // 
            this.tb_ID.Enabled = false;
            this.tb_ID.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_ID.Location = new System.Drawing.Point(118, 47);
            this.tb_ID.Name = "tb_ID";
            this.tb_ID.Size = new System.Drawing.Size(202, 28);
            this.tb_ID.TabIndex = 4;
            // 
            // btn_edit
            // 
            this.btn_edit.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_edit.Location = new System.Drawing.Point(987, 145);
            this.btn_edit.Name = "btn_edit";
            this.btn_edit.Size = new System.Drawing.Size(124, 35);
            this.btn_edit.TabIndex = 3;
            this.btn_edit.Text = "Bayi Düzenle";
            this.btn_edit.UseVisualStyleBackColor = true;
            this.btn_edit.Visible = false;
            this.btn_edit.Click += new System.EventHandler(this.btn_edit_Click);
            // 
            // btn_add
            // 
            this.btn_add.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btn_add.Location = new System.Drawing.Point(986, 186);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(121, 35);
            this.btn_add.TabIndex = 2;
            this.btn_add.Text = "Bayi Ekle";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(354, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(107, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "İndirim Oranı:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(39, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 21);
            this.label3.TabIndex = 1;
            this.label3.Text = "Bayi Tipi:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(415, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "Mail:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(41, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Bayi Adı:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(397, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 21);
            this.label4.TabIndex = 0;
            this.label4.Text = "Telefon:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(83, 50);
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
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(41, 241);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(84, 21);
            this.label7.TabIndex = 1;
            this.label7.Text = "Bayi Kodu:";
            // 
            // tb_dealercode
            // 
            this.tb_dealercode.Font = new System.Drawing.Font("Calibri", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.tb_dealercode.Location = new System.Drawing.Point(118, 233);
            this.tb_dealercode.Name = "tb_dealercode";
            this.tb_dealercode.Size = new System.Drawing.Size(202, 28);
            this.tb_dealercode.TabIndex = 5;
            // 
            // DealerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 608);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "DealerForm";
            this.Text = "DealerForm";
            this.Load += new System.EventHandler(this.DealerForm_Load);
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
        private System.Windows.Forms.TextBox tb_dealername;
        private System.Windows.Forms.TextBox tb_ID;
        private System.Windows.Forms.Button btn_edit;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox mtb_phone;
        private System.Windows.Forms.TextBox tb_discountrate;
        private System.Windows.Forms.TextBox tb_mail;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem TSMI_edit;
        private System.Windows.Forms.ToolStripMenuItem TSMI_delete;
        private System.Windows.Forms.ComboBox cb_dealertype;
        private System.Windows.Forms.TextBox tb_dealercode;
        private System.Windows.Forms.Label label7;
    }
}