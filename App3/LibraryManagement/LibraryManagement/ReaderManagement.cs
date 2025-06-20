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

namespace LibraryManagement
{
    public partial class ReaderManagement : UserControl
    {
        private readonly IReaderRepository _readerRepository;
        public ReaderManagement()
        {
            InitializeComponent();
            SqlConnection conn = DBConnection.GetConnection();
            conn.Open();
            IRepositoryFactory factory = new SqlRepositoryFactory(conn);
            _readerRepository = factory.CreateReaderRepository();
            LoadReaders();
        }
        private void LoadReaders()
        {
            dataGridViewReaders.DataSource = _readerRepository.GetAllReaders();
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void ReaderManagement_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Reader reader = new Reader
            {
                ReaderID = readerID_txt.Text,
                Name = name_txt.Text,
                BirthDate = birthDate_dt.Value,
                Email = email_txt.Text,
                Phone = phone_txt.Text
            };

            try
            {
                _readerRepository.AddReader(reader);
                MessageBox.Show("Thêm độc giả thành công!", "Thêm độc giả");
                ClearInput();
                LoadReaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm độc giả: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(readerID_txt.Text)) return;
            Reader reader = new Reader
            {
                ReaderID = readerID_txt.Text,
                Name = name_txt.Text,
                BirthDate = birthDate_dt.Value,
                Email = email_txt.Text,
                Phone = phone_txt.Text
            };

            try
            {
                _readerRepository.UpdateReader(reader);
                MessageBox.Show("Cập nhật độc giả thành công!", "Sửa độc giả");
                ClearInput();
                LoadReaders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật độc giả: {ex.Message}");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(readerID_txt.Text)) return;

            string readerID = readerID_txt.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa độc giả này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    _readerRepository.DeleteReader(readerID);
                    MessageBox.Show("Xóa độc giả thành công!", "Xóa độc giả");
                    ClearInput();
                    LoadReaders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa độc giả: {ex.Message}", "Xóa độc giả");
                }
            }
        }

        private void dataGridViewReaders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewReaders.Rows[e.RowIndex];
                readerID_txt.Text = row.Cells["ReaderID"].Value.ToString();
                name_txt.Text = row.Cells["Name"].Value?.ToString() ?? "";
                birthDate_dt.Value = row.Cells["BirthDate"].Value != DBNull.Value ? (DateTime)row.Cells["BirthDate"].Value : DateTime.Now;
                email_txt.Text = row.Cells["Email"].Value?.ToString() ?? "";
                phone_txt.Text = row.Cells["Phone"].Value?.ToString() ?? "";
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void ClearInput()
        {
            readerID_txt.Text = "";
            name_txt.Text = "";
            birthDate_dt.Value = DateTime.Now;
            email_txt.Text = "";
            phone_txt.Text = "";
        }

        private void panel2_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridViewReaders.DataSource = null;
            LoadReaders();
            MessageBox.Show("Làm mới", "Làm mới thành công");
        }
    }
}
