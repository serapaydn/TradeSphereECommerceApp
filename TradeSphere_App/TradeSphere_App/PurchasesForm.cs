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
    public partial class PurchasesForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public PurchasesForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Purchases p = new Purchases();
            p.Product_ID = int.Parse(cb_product.Text);
            p.Supplier_ID = int.Parse(cb_supplier.Text);
            p.Employee_ID = int.Parse(cb_employee.Text);
            p.Price = nud_price.Value;
            p.Date = dtp_date.Value;
            p.Quantity = tb_quantity.Text;
            try
            {
                db.Purchases.Add(p);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Satın alım başarıyla eklenmiştir. ID: {p.ID}", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_product.Text = "";
                cb_supplier.Text = "";
                cb_employee.Text = "";
                nud_price.Value = 0;
                dtp_date.Value = DateTime.Now;
                tb_quantity.Text = "";
            }
            catch
            {
                MessageBox.Show("Satın alım eklenirken hata oluştu. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void doldur()
        {
            List<Purchases> purchases = db.Purchases.ToList();
            dataGridView1.DataSource = purchases;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;

        }

        private void PurchasesForm_Load(object sender, EventArgs e)
        {
            var products = db.Products.ToList();
            cb_product.DataSource = products;
            cb_product.DisplayMember = "ProductName";
            cb_product.ValueMember = "ID";

            var suppliers = db.Suppliers.ToList();
            cb_supplier.DataSource = suppliers;
            cb_supplier.DisplayMember = "SupplierName";
            cb_supplier.ValueMember = "ID";

            var employees = db.Employees.ToList();
            cb_employee.DataSource = employees;
            cb_employee.DisplayMember = "EmployeeName";
            cb_employee.ValueMember = "ID";

            this.WindowState = FormWindowState.Maximized;
            List<Purchases> purchases = db.Purchases.ToList();
            dataGridView1.DataSource = purchases;

            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Purchases p = db.Purchases.Find(id);
            if (p != null)
            {
                tb_ID.Text = p.ID.ToString();
                cb_product.SelectedValue = p.Product_ID;
                cb_supplier.SelectedValue = p.Supplier_ID;
                cb_employee.SelectedValue = p.Employee_ID;
                nud_price.Value = (decimal)p.Price;
                dtp_date.Value = (DateTime)p.Date;
                tb_quantity.Text = p.Quantity;

                btn_edit.Visible = true;
            }
            else
            {
                MessageBox.Show("Düzenlenecek satın alım bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Purchases p = db.Purchases.Find(id);

            if (p != null)
            {
                db.Purchases.Remove(p);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Satın alım {p.ID} başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silinecek satın alım bulunamadı. Lütfen tekrar kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            Purchases p = db.Purchases.Find(id);
            if (p != null)
            {

                p.Product_ID = int.Parse(cb_product.SelectedValue.ToString());
                p.Supplier_ID = int.Parse(cb_supplier.SelectedValue.ToString());
                p.Employee_ID = int.Parse(cb_employee.SelectedValue.ToString());
                p.Price = nud_price.Value;
                p.Date = dtp_date.Value;
                p.Quantity = tb_quantity.Text;

                db.SaveChanges();
                doldur();

                MessageBox.Show("Satın alım başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncellenmek istenen satın alım bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_edit.Visible = false;
            tb_ID.Text = "";
            cb_product.Text = "";
            cb_supplier.Text = "";
            cb_employee.Text = "";
            nud_price.Value = 0;
            dtp_date.Value = DateTime.Now;
            tb_quantity.Text = "";

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

                            Paragraph title = new Paragraph("Satın Alımlar", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
