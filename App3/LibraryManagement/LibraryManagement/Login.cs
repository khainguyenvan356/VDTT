using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen; // Căn giữa màn hình
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string tenDangNhap = textBox1.Text.Trim();
            string matKhau = textBox2.Text.Trim();

            IAuthentication auth = new ProxyAuthentication();
            try
            {
                if (auth.Login(tenDangNhap, matKhau))
                {
                    MessageBox.Show("Đăng nhập thành công");
                    MainForm mainForm = new MainForm();
                    mainForm.Show();
                    this.Hide(); // Ẩn form đăng nhập sau khi đăng nhập thành công
                }
                else
                {
                    MessageBox.Show("Sai tài khoản hoặc mật khẩu!");
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}");
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
