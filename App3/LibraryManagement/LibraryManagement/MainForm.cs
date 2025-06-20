using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibraryManagement
{
    public partial class MainForm : Form
    {
        ReaderManagement readerManagement = new ReaderManagement();
        BookManagement bookManagement = new BookManagement();
        LoanManagement loanManagement = new LoanManagement();
        public MainForm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            readerManagement.Dock = DockStyle.Fill;
            bookManagement.Dock = DockStyle.Fill;
            loanManagement.Dock = DockStyle.Fill;

            this.Controls.Add(readerManagement);
            this.Controls.Add(bookManagement);
            this.Controls.Add(loanManagement);

            readerManagement.Visible = false;
            bookManagement.Visible = false;
            loanManagement.Visible = false;
        }
        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void bookManagement1_Load(object sender, EventArgs e)
        {

        }

        private void readerManagement1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            readerManagement.Visible = false;
            bookManagement.Visible = true;
            loanManagement.Visible = false;
            bookManagement.BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            readerManagement.Visible = true;
            bookManagement.Visible = false;
            loanManagement.Visible = false;
            readerManagement.BringToFront();
        }

        private void readerManagement1_Load_1(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            readerManagement.Visible = false;
            bookManagement.Visible = false;
            loanManagement.Visible = true;
            loanManagement.BringToFront();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Login loginForm = new Login();
            this.Hide();
            loginForm.Show();
        }
    }
}
