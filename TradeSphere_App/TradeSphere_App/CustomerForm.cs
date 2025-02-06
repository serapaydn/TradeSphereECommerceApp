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
    public partial class CustomerForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public CustomerForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Customers c = new Customers();

            if (int.TryParse(tb_dealerid.Text, out int dealerId))
            {
                c.Dealer_ID = dealerId;
            }
            else
            {
                c.Dealer_ID = null;
            }

            c.CompanyName = tb_companyname.Text;
            c.ContactName = tb_contactname.Text;
            c.Grade = cb_grade.Text;
            c.Address = tb_adddress.Text;
            c.City = tb_city.Text;
            c.Phone = mtb_phone.Text;
            c.Fax = mtb_fax.Text;

            try
            {
                db.Customers.Add(c);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Müşteri başarıyla eklenmiştir. Müşteri ID: {c.ID}", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                tb_dealerid.Text = "";
                tb_companyname.Text = "";
                tb_contactname.Text = "";
                cb_grade.Text = "";
                tb_adddress.Text = "";
                tb_city.Text = "";
                mtb_phone.Text = "";
                mtb_fax.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Müşteri eklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void doldur()
        {
            List<Customers> customers = db.Customers.ToList();
            dataGridView1.DataSource = customers;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            List<Customers> customers = db.Customers.ToList();
            dataGridView1.DataSource = customers;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Customers c = db.Customers.Find(id);
            if (c != null)
            {
                tb_ID.Text = c.ID.ToString();
                tb_dealerid.Text = c.Dealer_ID.ToString();
                tb_companyname.Text = c.CompanyName;
                tb_contactname.Text = c.ContactName;
                cb_grade.Text = c.Grade;
                tb_adddress.Text = c.Address;
                tb_city.Text = c.City;
                mtb_phone.Text = c.Phone;
                mtb_fax.Text = c.Fax;

                btn_edit.Visible = true;
            }
            else
            {
                MessageBox.Show("Düzenlenecek müşteri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Customers c = db.Customers.Find(id);
            if (c != null)
            {
                db.Customers.Remove(c);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Müşteri {c.CompanyName} başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silinecek müşteri bulunamadı. Lütfen tekrar kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Customers c = db.Customers.Find(id);
            if (c != null)
            {
                c.CompanyName = tb_companyname.Text;
                c.ContactName = tb_contactname.Text;
                c.Grade = cb_grade.Text;
                c.Address = tb_adddress.Text;
                c.City = tb_city.Text;
                c.Phone = mtb_phone.Text;
                c.Fax = mtb_fax.Text;

                db.SaveChanges();
                doldur();
                MessageBox.Show("Müşteri başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncellenmek istenen müşteri bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            btn_edit.Visible = false;
            tb_ID.Text = "";
            tb_companyname.Text = "";
            tb_contactname.Text = "";
            cb_grade.Text = "";
            tb_adddress.Text = "";
            tb_city.Text = "";
            mtb_phone.Text = "";
            mtb_fax.Text = "";
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

                            Paragraph title = new Paragraph("Müşteriler", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
