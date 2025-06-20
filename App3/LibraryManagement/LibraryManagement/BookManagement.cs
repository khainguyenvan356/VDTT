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
    public partial class BookManagement : UserControl
    {
        private readonly IBookRepository _bookRepository;
        public BookManagement()
        {
            InitializeComponent();
            category_cb.Items.AddRange(new[] { "Lap trinh", "Cong nghe thong tin", "Toan hoc", "Kinh te" });
            SqlConnection conn = DBConnection.GetConnection();
            conn.Open();
            IRepositoryFactory factory = new SqlRepositoryFactory(conn);
            _bookRepository = factory.CreateBookRepository();
            LoadBooks();
        }

        private void BookManagement_Load(object sender, EventArgs e)
        {

        }
        private void LoadBooks()
        {
            dataGridView1.DataSource = _bookRepository.GetAllBooks();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ClearInput()
        {
            bookID_txt.Text = "";
            tilte_txt.Text = "";
            author_txt.Text = "";
            publishYear_txt.Text = "";
            publisher_txt.Text = "";
            category_cb.SelectedIndex = -1;
            quantity_txt.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Book book = new Book
            {
                BookID = bookID_txt.Text,
                Title = tilte_txt.Text,
                Author = author_txt.Text,
                PublishYear = int.TryParse(publishYear_txt.Text, out int year) ? year : (int?)null,
                Publisher = publisher_txt.Text,
                Category = category_cb.SelectedItem?.ToString(),
                Quantity = int.Parse(quantity_txt.Text)
            };

            try
            {
                _bookRepository.AddBook(book);
                MessageBox.Show("Thêm sách thành công!", "Thêm sách");
                ClearInput();
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm sách: {ex.Message}");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Book book = new Book
            {
                BookID = bookID_txt.Text,
                Title = tilte_txt.Text,
                Author = author_txt.Text,
                PublishYear = int.TryParse(publishYear_txt.Text, out int year) ? year : (int?)null,
                Publisher = publisher_txt.Text,
                Category = category_cb.SelectedItem?.ToString(),
                Quantity = int.Parse(quantity_txt.Text)
            };

            try
            {
                _bookRepository.UpdateBook(book);
                MessageBox.Show("Cập nhật sách thành công!", "Sửa sách");
                ClearInput();
                LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật sách: {ex.Message}");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Đảm bảo không phải header
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                bookID_txt.Text = row.Cells["BookID"].Value.ToString();
                tilte_txt.Text = row.Cells["Title"].Value.ToString();
                author_txt.Text = row.Cells["Author"].Value?.ToString() ?? "";
                publishYear_txt.Text = row.Cells["PublishYear"].Value?.ToString() ?? "";
                publisher_txt.Text = row.Cells["Publisher"].Value?.ToString() ?? "";
                category_cb.SelectedItem = row.Cells["Category"].Value?.ToString();
                quantity_txt.Text = row.Cells["Quantity"].Value.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(bookID_txt.Text)) return;
            string bookID = bookID_txt.Text;
                    try
                    {
                        MessageBox.Show("Xóa sách thành công!", "Xóa sách");
                        _bookRepository.DeleteBook(bookID);
                        ClearInput();
                        LoadBooks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi khi xóa sách: {ex.Message}", "Xóa sách");
                    }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            LoadBooks();
            MessageBox.Show("Làm mới thành công", "Làm mới");
        }
    }
}
