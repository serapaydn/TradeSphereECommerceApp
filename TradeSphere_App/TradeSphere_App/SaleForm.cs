using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeSphere_App.Model;

namespace TradeSphere_App
{
    public partial class SaleForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public SaleForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Sales sale = new Sales();
            sale.Customer_ID = int.Parse(cb_customer.Text);
            sale.Employee_ID = int.Parse(cb_employee.Text);
            sale.TotalPrice = nud_totalprice.Value;
            sale.Date = dtp_date.Value;
            try
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                doldur();
                MessageBox.Show("Satış" + sale.ID + "ile başarıyla eklenmiştir");
                cb_customer.Text = "";
                cb_employee.Text = "";
                nud_totalprice.Value = 0;
                dtp_date.Value = DateTime.Now;
            }
            catch
            {
                MessageBox.Show("Satış eklenirken bir hata oluştu");
            }
        }
        public void doldur()
        {
            List<Sales> sales = db.Sales.ToList();
            dataGridView1.DataSource = sales;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;

        }

        private void SaleForm_Load(object sender, EventArgs e)
        {
            var customer = db.Customers.ToList();
            cb_customer.DataSource = customer;
            cb_customer.DisplayMember = "CustomerName";
            cb_customer.ValueMember = "ID";

            var employees = db.Employees.ToList();
            cb_employee.DataSource = employees;
            cb_employee.DisplayMember = "EmployeeName";
            cb_employee.ValueMember = "ID";
            this.WindowState = FormWindowState.Maximized;
            List<Sales> sales = db.Sales.ToList();
            dataGridView1.DataSource = sales;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Sales sale = db.Sales.Find(id);
            if (sale != null)
            {
                tb_ID.Text = sale.ID.ToString();
                cb_customer.SelectedValue = sale.Customer_ID;
                cb_employee.SelectedValue = sale.Employee_ID;
                nud_totalprice.Value = (decimal)sale.TotalPrice;
                dtp_date.Value = (DateTime)sale.Date;

                btn_edit.Visible = true;
            }
            else
            {
                MessageBox.Show("Satış bulunamadı.");
            }
  
        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Sales sale = db.Sales.Find(id);
            if (sale != null)
            {
                db.Sales.Remove(sale);
                db.SaveChanges();
                doldur();

                MessageBox.Show("Satış başarıyla silindi.");
            }
            else
            {
                MessageBox.Show("Silinecek satış bulunamadı.");
            }

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.ClearSelection();
                int index = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                if (index >= 0)
                {
                    id = Convert.ToInt32(dataGridView1.Rows[index].Cells["ID"].Value);
                    dataGridView1.Rows[index].Selected = true;
                    contextMenuStrip1.Show(dataGridView1, e.X, e.Y);
                }
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Sales sale = db.Sales.Find(id);
            if (sale != null)
            {
                sale.Customer_ID = int.Parse(cb_customer.SelectedValue.ToString());
                sale.Employee_ID = int.Parse(cb_employee.SelectedValue.ToString());
                sale.TotalPrice = nud_totalprice.Value;
                sale.Date = dtp_date.Value;

                db.SaveChanges();
                doldur();

                MessageBox.Show("Satış başarıyla güncellenmiştir.");

               
            }
            btn_edit.Visible = false;
            tb_ID.Text = "";
            cb_customer.Text = "";
            cb_employee.Text = "";
            nud_totalprice.Value = 0;
            dtp_date.Value = DateTime.Now;

        }

        private void btn_pdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files|*.pdf";
            saveFileDialog.Title = "Save as PDF";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (var fs = new System.IO.FileStream(saveFileDialog.FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None))
                    {
                        using (Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 10f))
                        {
                            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
                            pdfDoc.Open();

                            Paragraph title = new Paragraph("Satışlar", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
                            title.Alignment = Element.ALIGN_CENTER;
                            pdfDoc.Add(title);
                            pdfDoc.Add(new Paragraph("\n"));

                            int visibleColumnCount = 0;
                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                if (column.Visible)
                                    visibleColumnCount++;
                            }
                            PdfPTable table = new PdfPTable(visibleColumnCount);
                            table.WidthPercentage = 100;

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                if (column.Visible)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                                    table.AddCell(cell);
                                }
                            }

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    foreach (DataGridViewCell cell in row.Cells)
                                    {
                                        if (dataGridView1.Columns[cell.ColumnIndex].Visible)
                                        {
                                            string cellValue = cell.Value?.ToString() ?? "N/A";
                                            table.AddCell(new Phrase(cellValue));
                                        }
                                    }
                                }
                            }

                            pdfDoc.Add(table);
                        }
                    }

                    MessageBox.Show("PDF başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("PDF oluşturma sırasında bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
