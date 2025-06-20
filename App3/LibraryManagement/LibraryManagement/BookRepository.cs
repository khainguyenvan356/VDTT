using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class BookRepository : IBookRepository
    {
        private readonly SqlConnection _connection;

        public BookRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void AddBook(Book book)
        {
            string query = "INSERT INTO Books (BookID, Title, Author, PublishYear, Publisher, Category, Quantity) VALUES (@BookID, @Title, @Author, @PublishYear, @Publisher, @Category, @Quantity)";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BookID", book.BookID);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", (object)book.Author ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PublishYear", (object)book.PublishYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Publisher", (object)book.Publisher ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Category", book.Category);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateBook(Book book)
        {
            string query = "UPDATE Books SET Title = @Title, Author = @Author, PublishYear = @PublishYear, Publisher = @Publisher, Category = @Category, Quantity = @Quantity WHERE BookID = @BookID";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BookID", book.BookID);
                cmd.Parameters.AddWithValue("@Title", book.Title);
                cmd.Parameters.AddWithValue("@Author", (object)book.Author ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@PublishYear", (object)book.PublishYear ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Publisher", (object)book.Publisher ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Category", book.Category);
                cmd.Parameters.AddWithValue("@Quantity", book.Quantity);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteBook(string bookID)
        {
            string query = "DELETE FROM Books WHERE BookID = @BookID";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BookID", bookID);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllBooks()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Books";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
    }
}