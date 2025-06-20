using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace LibraryManagement
{
    public partial class LoanManagement : UserControl
    {
        private IloanRepository _loanRepository;
        public LoanManagement()
        {
            InitializeComponent();
            SqlConnection conn = DBConnection.GetConnection();
            conn.Open();
            LoadLoanData(conn);
            IRepositoryFactory loanFactory = new SqlRepositoryFactory(conn);
                _loanRepository = loanFactory.CreateLoanRepository();
            LoadLoan();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string loanId = loanID_txt.Text.Trim();
                if (string.IsNullOrEmpty(loanId))
                {
                    System.Windows.Forms.MessageBox.Show("Hãy nhập trường ID");
                    return;
                }

                ICommand returnCommand = new ReturnCommand(loanId, _loanRepository);
                returnCommand.Execute();
                LoadLoan();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error: {ex.Message}");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string loanId = loanID_txt.Text.Trim();
                string bookId = comboBookID_txt.SelectedItem?.ToString();
                string readerId = comboReaderID_txt.SelectedItem?.ToString();
                DateTime borrowDate = dateTimePicker1.Value;
                DateTime dueDate = dateTimePicker2.Value;
                if (string.IsNullOrEmpty(loanId) || string.IsNullOrEmpty(bookId) || string.IsNullOrEmpty(readerId))
                {
                    System.Windows.Forms.MessageBox.Show("Hãy nhập hết cách trường");
                    return;
                }
                Loan loan = new Loan
                {
                    LoanId = loanId,
                    ReaderId = readerId,
                    BookId = bookId,
                    BorrowDate = borrowDate,
                    DueDate = dueDate
                };
                ICommand borrowCommand = new BorrowCommand(loan, _loanRepository);
                borrowCommand.Execute();
                LoadLoan();
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void LoadLoanData(SqlConnection conn)
        {
            string bookQuery = "SELECT BookId, Title FROM Books";
            using (SqlCommand cmd = new SqlCommand(bookQuery, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    comboBookID_txt.Items.Clear();
                    while (reader.Read())
                    {
                        comboBookID_txt.Items.Add(reader["BookId"].ToString());
                    }
                }
            }

            // Tải dữ liệu cho comboBoxReaderId
            string readerQuery = "SELECT ReaderId, Name FROM Readers";
            using (SqlCommand cmd = new SqlCommand(readerQuery, conn))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    comboReaderID_txt.Items.Clear();
                    while (reader.Read())
                    {
                        comboReaderID_txt.Items.Add(reader["ReaderId"].ToString());
                    }
                }
            }
        }

        private void LoadLoan()
        {
            dataGridView1.DataSource = _loanRepository.GetAllLoans();
            dataGridView1.Columns["LoanId"].HeaderText = "ID";
            dataGridView1.Columns["ReaderId"].HeaderText = "ID độc giả";
            dataGridView1.Columns["BookId"].HeaderText = "ID sách";
            dataGridView1.Columns["BorrowDate"].HeaderText = "Ngày mượn";
            dataGridView1.Columns["DueDate"].HeaderText = "Hạn trả";
            dataGridView1.Columns["ReturnDate"].HeaderText = "Ngày trả";
            dataGridView1.Columns["Status"].HeaderText = "Trạng thái";
            dataGridView1.Columns["FineAmount"].HeaderText = "Tiền phạt"; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                loanID_txt.Text = row.Cells["LoanId"].Value.ToString();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
