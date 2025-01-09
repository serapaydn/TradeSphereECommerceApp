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
    public partial class EmployeeForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;
        public EmployeeForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {


            Employees emp = new Employees();
            emp.Name = tb_name.Text;
            emp.Surname = tb_surname.Text;
            emp.Title = tb_title.Text;
            emp.Address = tb_address.Text;
            emp.BirthDate = dtp_bithdate.Value;
            emp.HireDate = dtp_hiredate.Value;
            emp.Phone = mtb_phone.Text;
            emp.Notes = tb_notes.Text;
            try
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                doldur();
                MessageBox.Show(" Çalışan " + emp.ID + " ID ile başarıyla eklenmiştir");
                tb_name.Text = "";
                tb_surname.Text = "";
                tb_title.Text = "";
                tb_address.Text = "";
                dtp_bithdate.Value = DateTime.Now;
                dtp_hiredate.Value = DateTime.Now;
                mtb_phone.Text = "";
                tb_notes.Text = "";

            }
            catch
            {
                MessageBox.Show("Çalışan eklenirken hata oluştu!");
            }


        }
        public void doldur()
        {
            List<Employees> employees = db.Employees.ToList();
            dataGridView1.DataSource = employees;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;


        }

        private void EmployeeForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            List<Employees> employees = db.Employees.ToList();
            dataGridView1.DataSource = employees;
            dataGridView1.Columns[9].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;

        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Employees emp = db.Employees.Find(id);
            if (emp != null)
            {
                tb_ID.Text = emp.ID.ToString();
                tb_name.Text = emp.Name;
                tb_surname.Text = emp.Surname;
                tb_title.Text = emp.Title;
                tb_address.Text = emp.Address;
                dtp_bithdate.Value = emp.BirthDate.Value;
                dtp_hiredate.Value = emp.HireDate.Value;
                mtb_phone.Text = emp.Phone;
                tb_notes.Text = emp.Notes;
                btn_edit.Visible = true;
            }
        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Employees emp = db.Employees.Find(id);
            if (emp != null)
            {
                db.Employees.Remove(emp);
                db.SaveChanges();
                doldur();
                MessageBox.Show("Çalışan" + emp.Name + "başarıyla silindi");
            }
            else
            {
                MessageBox.Show("Silinecek çalışan bulunamadı");
            }

        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            Employees emp = db.Employees.Find(id);
            if (emp != null)
            {
                emp.Name = tb_name.Text;
                emp.Surname = tb_surname.Text;
                emp.Title = tb_title.Text;
                emp.Address = tb_address.Text;
                emp.BirthDate = dtp_bithdate.Value;
                emp.HireDate = dtp_hiredate.Value;
                emp.Phone = mtb_phone.Text;
                emp.Notes = tb_notes.Text;
                db.SaveChanges();
                doldur();
                MessageBox.Show("Çalışan başarıyla güncellendi");

            }
            btn_edit.Visible = false;
            tb_ID.Text = "";
            tb_name.Text = "";
            tb_surname.Text = "";
            tb_title.Text = "";
            tb_address.Text = "";
            dtp_bithdate.Value = DateTime.Now;
            dtp_hiredate.Value = DateTime.Now;
            mtb_phone.Text = "";
            tb_notes.Text = "";
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

                            Paragraph title = new Paragraph("Çalışanlar", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20));
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
