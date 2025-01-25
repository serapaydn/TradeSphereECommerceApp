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
    public partial class DealerForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;

        public DealerForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Dealers d = new Dealers
            {
                DealerName = tb_dealername.Text,
                DealerType = cb_dealertype.Text,
                Phone = mtb_phone.Text,
                Mail = tb_mail.Text,
                DiscountRate = int.Parse(tb_discountrate.Text),
                DealerCode = tb_dealercode.Text
            };
            try
            {
                db.Dealers.Add(d);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Bayi başarıyla eklenmiştir. Bayi ID: {d.ID}", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tb_dealername.Text = "";
                cb_dealertype.Text = "";
                mtb_phone.Text = "";
                tb_mail.Text = "";
                tb_discountrate.Text = "";
                tb_dealercode.Text = "";
            }
            catch
            {
                MessageBox.Show("Bayi eklenirken hata oluştu. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void doldur()
        {
            List<Dealers> dealers = db.Dealers.ToList();
            dataGridView1.DataSource = dealers;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[6].Visible = false;

        }

        private void DealerForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            List<Dealers> dealers = db.Dealers.ToList();
            dataGridView1.DataSource = dealers;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {


            Dealers d = db.Dealers.Find(id);
            if (d != null)
            {
                tb_ID.Text = d.ID.ToString();
                tb_dealername.Text = d.DealerName;
                cb_dealertype.Text = d.DealerType;
                mtb_phone.Text = d.Phone;
                tb_mail.Text = d.Mail;
                tb_discountrate.Text = d.DiscountRate.ToString();
                tb_dealercode.Text = d.DealerCode;
                btn_edit.Visible = true;
            }
            else
            {
                MessageBox.Show("Düzenlenecek bayi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Dealers d = db.Dealers.Find(id);
            if (d != null)
            {
                db.Dealers.Remove(d);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Bayi {d.DealerName} başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silinecek bayi bulunamadı. Lütfen tekrar kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Dealers d = db.Dealers.Find(id);
            if (d != null)
            {
                d.DealerName = tb_dealername.Text;
                d.DealerType = cb_dealertype.Text;
                d.Phone = mtb_phone.Text;
                d.Mail = tb_mail.Text;
                d.DiscountRate = int.Parse(tb_discountrate.Text);
                d.DealerCode = tb_dealercode.Text;

                db.SaveChanges();
                doldur();
                MessageBox.Show("Bayi başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncellenmek istenen bayi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_edit.Visible = false;
            tb_ID.Text = "";
            tb_dealername.Text = "";
            cb_dealertype.Text = "";
            mtb_phone.Text = "";
            tb_mail.Text = "";
            tb_discountrate.Text = "";
            tb_dealercode.Text = "";
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

                            Paragraph title = new Paragraph("Bayiler", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
