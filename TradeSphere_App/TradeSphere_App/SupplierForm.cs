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
    public partial class SupplierForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public SupplierForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Suppliers s = new Suppliers();
            s.CompanyName = tb_companyname.Text;
            s.ContactName = tb_contactname.Text;
            s.Phone = mtb_phone.Text;
            s.City = tb_city.Text;
            s.Address = tb_adddress.Text;
            s.Mail = tb_mail.Text;
            try
            {
                db.Suppliers.Add(s);
                db.SaveChanges();
                doldur();
                MessageBox.Show("Tedarikçi başarıyla eklenmiştir");
                tb_companyname.Text = "";
                tb_contactname.Text = "";
                mtb_phone.Text = "";
                tb_city.Text = "";
                tb_adddress.Text = "";
                tb_mail.Text = "";

            }
            catch
            {
                MessageBox.Show("Tedarikçi eklenirken hata oluştu!");
            }

        }
        public void doldur()
        {
            List<Suppliers> suppliers = db.Suppliers.ToList();
            dataGridView1.DataSource = suppliers;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;

        }


        private void SupplierForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            List<Suppliers> suppliers = db.Suppliers.ToList();
            dataGridView1.DataSource = suppliers;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Suppliers s = db.Suppliers.Find(id);
            try
            {
                if (s != null)
                {
                    tb_ID.Text = s.ID.ToString();
                    tb_companyname.Text = s.CompanyName;
                    tb_contactname.Text = s.ContactName;
                    mtb_phone.Text = s.Phone;
                    tb_city.Text = s.City;
                    tb_adddress.Text = s.Address;
                    tb_mail.Text = s.Mail;
                    btn_edit.Visible = true;
                }
            }
            catch
            {
                MessageBox.Show("Tedarikçi düzenlenirken hata oluştu!");
            }
        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Suppliers s = db.Suppliers.Find(id);
            try
            {
                if (s != null)
                {
                    db.Suppliers.Remove(s);
                    db.SaveChanges();
                    doldur();
                    MessageBox.Show("Tedarikçi başarıyla silinmiştir!");
                }
            }
            catch
            {
                MessageBox.Show("Tedarikçi silinirken hata oluştu");
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
            Suppliers s = db.Suppliers.Find(id);

            if (s != null)
            {
                s.CompanyName = tb_companyname.Text;
                s.ContactName = tb_contactname.Text;
                s.Phone = mtb_phone.Text;
                s.City = tb_city.Text;
                s.Address = tb_adddress.Text;
                s.Mail = tb_mail.Text;

                db.SaveChanges();
                doldur();

                MessageBox.Show("Tedarikçi başarıyla güncellendi!");

            }
            btn_edit.Visible = false;
            tb_companyname.Text = "";
            tb_contactname.Text = "";
            mtb_phone.Text = "";
            tb_city.Text = "";
            tb_adddress.Text = "";
            tb_mail.Text = "";

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

                            Paragraph title = new Paragraph("Tedarikçiler", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
