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
using System.Xml.Linq;

namespace TradeSphere_App
{
    public partial class ProductForm : Form
    {
        TradeSphereApp_DBEntities1 db = new TradeSphereApp_DBEntities1();
        int id;


        public ProductForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            Products product = new Products
            {
                Name = tb_name.Text,
                Supplier_ID = int.Parse(cb_supplierid.SelectedValue.ToString()),
                Category_ID = int.Parse(cb_categoryid.SelectedValue.ToString()),
                Brand_ID = int.Parse(cb_brandid.SelectedValue.ToString()),
                Barcode = tb_barcode.Text,
                Quantity = tb_quantity.Text,
                Price = decimal.Parse(nud_price.Value.ToString()),
                Stock = (short)nud_stock.Value,
                ReorderLevel = (short?)nud_reorderlevel.Value,
                IsActive = true,
                IsDeleted = false
            };
            try
            {
                db.Products.Add(product);
                db.SaveChanges();
                doldur();

                MessageBox.Show("Ürün başarıyla eklendi!");
                tb_name.Text = tb_barcode.Text = tb_quantity.Text = "";
                cb_supplierid.SelectedIndex = -1;
                cb_categoryid.SelectedIndex = -1;
                cb_brandid.SelectedIndex = -1;
                nud_price.Value = 0;
                nud_stock.Value = 0;
                nud_reorderlevel.Value = 0;
            }
            catch
            {
                MessageBox.Show("Ürün oluşturulurken hata oluştu!");
            }
        }

        public void doldur()
        {
            List<Products> products = db.Products.Where(p => p.IsActive == true && p.IsDeleted == false).ToList();
            dataGridView1.DataSource = products;


            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
            CreateXmlFile();

        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            var suppliers = db.Suppliers.ToList();
            cb_supplierid.DataSource = suppliers;
            cb_supplierid.DisplayMember = "SupplierName";
            cb_supplierid.ValueMember = "ID";

            var categories = db.Categories.ToList();
            cb_categoryid.DataSource = categories;
            cb_categoryid.DisplayMember = "CategoryName";
            cb_categoryid.ValueMember = "ID";

            var brands = db.Brands.ToList();
            cb_brandid.DataSource = brands;
            cb_brandid.DisplayMember = "BrandName";
            cb_brandid.ValueMember = "ID";

            this.WindowState = FormWindowState.Maximized;
            List<Products> products = db.Products.Where(p => p.IsActive == true && p.IsDeleted == false).ToList();
            dataGridView1.DataSource = products;

            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;
            dataGridView1.Columns[12].Visible = false;
            dataGridView1.Columns[13].Visible = false;
            dataGridView1.Columns[14].Visible = false;
            dataGridView1.Columns[15].Visible = false;
            dataGridView1.Columns[16].Visible = false;
        }

        private void TSMI_edit_Click(object sender, EventArgs e)
        {
            Products product = db.Products.Find(id);
            try
            {
                if (product != null)
                {
                    product.Name = tb_name.Text;
                    product.Supplier_ID = int.Parse(cb_supplierid.SelectedValue.ToString());
                    product.Category_ID = int.Parse(cb_categoryid.SelectedValue.ToString());
                    product.Brand_ID = int.Parse(cb_brandid.SelectedValue.ToString());
                    product.Barcode = tb_barcode.Text;
                    product.Quantity = tb_quantity.Text;
                    product.Price = decimal.Parse(nud_price.Value.ToString());
                    product.Stock = (short?)nud_stock.Value;
                    product.ReorderLevel = (short?)nud_reorderlevel.Value;
                    btn_edit.Visible = true;
                }
            }
            catch
            {
                MessageBox.Show("Ürün düzenlenirken bir hata oluştu!");
            }
        }

        private void TSMI_delete_Click(object sender, EventArgs e)
        {
            Products product = db.Products.Find(id);
            try
            {
                if (product != null)
                {
                    db.Products.Remove(product);
                    db.SaveChanges();
                    doldur();

                    MessageBox.Show("Ürün başarıyla silindi!");
                }
                else
                {
                    MessageBox.Show("Silinecek ürün bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün silinirken bir hata oluştu: " + ex.Message);
            }
        }

        private void CreateXmlFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "XML Files|*.xml",
                Title = "XML Dosyasını Kaydet",
                InitialDirectory = @"C:\",
                FileName = "Products.xml"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    var discountRates = new Dictionary<string, decimal>
            {
                { "Gold", 20m },
                { "Silver", 10m },
                { "Bronze", 5m }
            };

                    List<Products> products = db.Products.Where(p => p.IsActive == true && p.IsDeleted == false).ToList();

                    XElement xmlProducts = new XElement("Products",
                        products.Select(product => new XElement("Product",
                            new XElement("ID", product.ID),
                            new XElement("Name", product.Name),
                            new XElement("Barcode", product.Barcode),
                            new XElement("Price", product.Price),
                            new XElement("GoldPrice", Math.Round((decimal)(product.Price * (1 - discountRates["Gold"] / 100m)), 2)),
                            new XElement("SilverPrice", Math.Round((decimal)(product.Price * (1 - discountRates["Silver"] / 100m)), 2)),
                            new XElement("BronzePrice", Math.Round((decimal)(product.Price * (1 - discountRates["Bronze"] / 100m)), 2)),
                            new XElement("Stock", product.Stock)
                        ))
                    );

                    xmlProducts.Save(saveFileDialog.FileName);
                    MessageBox.Show($"XML dosyası başarıyla '{saveFileDialog.FileName}' konumuna kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"XML dosyası oluşturulurken bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            Products product = db.Products.Find(id);
            try
            {
                if (product != null)
                {
                    product.Name = tb_name.Text;
                    product.Supplier_ID = int.Parse(cb_supplierid.SelectedValue.ToString());
                    product.Category_ID = int.Parse(cb_categoryid.SelectedValue.ToString());
                    product.Brand_ID = int.Parse(cb_brandid.SelectedValue.ToString());
                    product.Barcode = tb_barcode.Text;
                    product.Quantity = tb_quantity.Text;
                    product.Price = decimal.Parse(nud_price.Value.ToString());
                    product.Stock = (short)nud_stock.Value;
                    product.ReorderLevel = (short?)nud_reorderlevel.Value;
                    db.SaveChanges();

                    doldur();

                    MessageBox.Show("Ürün başarıyla güncellendi!");
                }
                btn_edit.Visible = false;
                tb_name.Text = tb_barcode.Text = tb_quantity.Text = "";
                cb_supplierid.SelectedIndex = -1;
                cb_categoryid.SelectedIndex = -1;
                cb_brandid.SelectedIndex = -1;
                nud_price.Value = 0;
                nud_stock.Value = 0;
                nud_reorderlevel.Value = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ürün güncellenirken bir hata oluştu: " + ex.Message);
            }
        }

        private void btn_pdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF Files|*.pdf",
                Title = "Save as PDF"
            };

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

                            Paragraph title = new Paragraph("Ürünler", FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 20))
                            {
                                Alignment = Element.ALIGN_CENTER
                            };
                            pdfDoc.Add(title);
                            pdfDoc.Add(new Paragraph("\n"));

                            int visibleColumnCount = 0;
                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                if (column.Visible)
                                    visibleColumnCount++;
                            }

                            PdfPTable table = new PdfPTable(visibleColumnCount)
                            {
                                WidthPercentage = 100
                            };

                            foreach (DataGridViewColumn column in dataGridView1.Columns)
                            {
                                if (column.Visible)
                                {
                                    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText))
                                    {
                                        BackgroundColor = BaseColor.LIGHT_GRAY
                                    };
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
