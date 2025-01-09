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
    public partial class CategoryForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public CategoryForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Categories c = new Categories();
            c.Name = tb_name.Text;
            c.Description = tb_description.Text;
            try
            {
                db.Categories.Add(c);
                db.SaveChanges();
                MessageBox.Show("Kategori" + c.ID + "ID ile başarıyla eklenmiştir");
                tb_name.Text = "";
                tb_description.Text = "";
            }
            catch
            {
                MessageBox.Show("Hata!");
            }

        }
        public void doldur()
        {
            List<Categories> categories = db.Categories.ToList();
            dataGridView1.DataSource = categories;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }
        private void CategoryForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            List<Categories> categories = db.Categories.ToList();
            dataGridView1.DataSource = categories;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Categories c = db.Categories.Find(id);
            if (c != null)
            {
                tb_ID.Text = c.ID.ToString();
                tb_name.Text = c.Name;
                tb_description.Text = c.Description;
                btn_edit.Visible = true;
            }

        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Categories c = db.Categories.Find(id);
            if (c != null)
            {
                db.Categories.Remove(c);
                db.SaveChanges();
                doldur();
                MessageBox.Show("Kategori" + c.Name + "başarıyla silindi");
            }
            else
            {
                MessageBox.Show("Silinecek kategori bulunamadı");
            }
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Categories c = db.Categories.Find(id);
            if (c != null)
            {
                c.Name = tb_name.Text;
                c.Description = tb_description.Text;
                db.SaveChanges();
                doldur();
                MessageBox.Show("Kategori başarıyla güncellendi");
            }
            btn_edit.Visible = false;
            tb_name.Text = tb_description.Text = tb_ID.Text = "";
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

                            Paragraph title = new Paragraph("Kategoriler", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
