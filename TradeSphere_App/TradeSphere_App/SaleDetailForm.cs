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
    public partial class SaleDetailForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public SaleDetailForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            SaleDetails s = new SaleDetails();
            s.Product_ID = int.Parse(cb_product.Text);
            s.Sale_ID = int.Parse(cb_sale.Text);
            s.SalePrice = nud_saleprice.Value;
            s.Quantity = tb_quantity.Text;
            try
            {
                db.SaleDetails.Add(s);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Satış detayları başarıyla eklenmiştir. Satış ID: {s.ID}", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cb_product.Text = "";
                cb_sale.Text = "";
                nud_saleprice.Value = 0;
                tb_quantity.Text = "";
            }
            catch
            {
                MessageBox.Show("Satış detayı eklenirken hata oluştu. Lütfen tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public void doldur()
        {
            List<SaleDetails> saleDetails = db.SaleDetails.ToList();
            dataGridView1.DataSource = saleDetails;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;

        }
        private void SaleDetailForm_Load(object sender, EventArgs e)
        {

            var products = db.Products.ToList();
            cb_product.DataSource = products;
            cb_product.DisplayMember = "ProductName";
            cb_product.ValueMember = "ID";

            var sales = db.Sales.ToList();
            cb_sale.DataSource = sales;
            cb_sale.DisplayMember = "SaleName";
            cb_sale.ValueMember = "ID";

            this.WindowState = FormWindowState.Maximized;
            List<SaleDetails> saleDetails = db.SaleDetails.ToList();
            dataGridView1.DataSource = saleDetails;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
        }
        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            SaleDetails s = db.SaleDetails.Find(id);
            if (s != null)
            {
                tb_ID.Text = s.ID.ToString();
                cb_product.SelectedValue = s.Product_ID;
                cb_sale.SelectedValue = s.Sale_ID;
                nud_saleprice.Value = (decimal)s.SalePrice;
                tb_quantity.Text = s.Quantity;

                btn_edit.Visible = true;
            }
            else
            {
                MessageBox.Show("Düzenlenecek satış detayı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            SaleDetails s = db.SaleDetails.Find(id);
            if (s != null)
            {
                db.SaleDetails.Remove(s);
                db.SaveChanges();
                doldur();
                MessageBox.Show($"Satış detayı başarıyla silindi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Silinecek satış detayı bulunamadı. Lütfen tekrar kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            SaleDetails s = db.SaleDetails.Find(id);
            if (s != null)
            {
                s.Product_ID = int.Parse(cb_product.SelectedValue.ToString());
                s.Sale_ID = int.Parse(cb_sale.SelectedValue.ToString());
                s.SalePrice = nud_saleprice.Value;
                s.Quantity = tb_quantity.Text;
                db.SaveChanges();
                doldur();

                MessageBox.Show("Satış detayı başarıyla güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Güncellenmek istenen satış detayı bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btn_edit.Visible = false;
            tb_ID.Text = "";
            cb_product.Text = "";
            cb_sale.Text = "";
            nud_saleprice.Value = 0;
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

                            Paragraph title = new Paragraph("Satış Detayları", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
