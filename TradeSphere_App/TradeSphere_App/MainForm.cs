using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TradeSphere_App
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient mdiClient)
                {
                    mdiClient.BackColor = ColorTranslator.FromHtml("#dbc4bf");
                }
            }
            //LoginForm frm = new LoginForm();
            //frm.ShowDialog();
        }
        public void FormOpen(Form frm)
        {
            Form[] forms = this.MdiChildren;
            bool isOpen = false;

            foreach (Form item in forms)
            {
                if (item.GetType() == frm.GetType())
                {
                    isOpen = true;
                    item.Activate();
                    item.WindowState = FormWindowState.Normal;
                    item.WindowState = FormWindowState.Maximized;
                }
            }
            if (!isOpen)
            {
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
            }

        }

        private void TSMI_category_Click(object sender, EventArgs e)
        {
            FormOpen(new CategoryForm());
        }

        private void TSMI_brand_Click(object sender, EventArgs e)
        {
            FormOpen(new BrandForm());
        }

        private void TSMI_product_Click(object sender, EventArgs e)
        {
            FormOpen(new ProductForm());
        }

        private void TSMI_employee_Click(object sender, EventArgs e)
        {
            FormOpen(new EmployeeForm());
        }

        private void TSMI_customer_Click(object sender, EventArgs e)
        {
            FormOpen(new CustomerForm());
        }

        private void TSMI_supplier_Click(object sender, EventArgs e)
        {
            FormOpen(new SupplierForm());
        }

        private void TSMI_purchases_Click(object sender, EventArgs e)
        {
            FormOpen(new PurchasesForm());
        }

        private void TSMI_sale_Click(object sender, EventArgs e)
        {
            FormOpen(new SaleForm());
        }

        private void TSMI_saledetail_Click(object sender, EventArgs e)
        {
            FormOpen(new SaleDetailForm());
        }

        private void TSMI_dealer_Click(object sender, EventArgs e)
        {
            FormOpen(new DealerForm());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //DialogResult result = MessageBox.Show("Çıkış yapmak istediğinizden emin misiniz?", "Çıkış Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //if (result != DialogResult.Yes)
            //{
            //    e.Cancel = true;
            //}
        }

        private void TSMI_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
