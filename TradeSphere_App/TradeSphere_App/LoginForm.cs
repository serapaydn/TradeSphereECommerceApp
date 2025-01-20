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
    public partial class LoginForm : Form
    {
        bool giris;
        public LoginForm()
        {
            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#dbc4bf");
            giris = false;
        }

        private void btn_giris_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_kullaniciadi.Text))
            {
                if (!string.IsNullOrEmpty(tb_sifre.Text))
                {
                    if (tb_kullaniciadi.Text == "admin" && tb_sifre.Text == "123")
                    {
                        giris = true;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı!!!");
                    }
                }
                else
                {
                    MessageBox.Show("Şifre boş bırakılamaz");
                }
            }
            else
            {
                MessageBox.Show("Kuallanıcı Adı boş bırakılamaz");
            }
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
          
            if (!giris)
            {
                DialogResult result = MessageBox.Show("Çıkış Yapmak İstiyor musun?", "Çıkış Onay", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK)
                {
                    e.Cancel = true;
                }
            }
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!giris)
            {
                Application.Exit();
            }
        }
    }
}
