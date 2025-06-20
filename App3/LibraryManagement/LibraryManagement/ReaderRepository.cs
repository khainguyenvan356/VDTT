using System;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class ReaderRepository : IReaderRepository
    {
        private readonly SqlConnection _connection;

        public ReaderRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void AddReader(Reader reader)
        {
            string query = "INSERT INTO Readers (ReaderID, Name, BirthDate, Email, Phone) VALUES (@ReaderID, @Name, @BirthDate, @Email, @Phone)";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@ReaderID", reader.ReaderID);
                cmd.Parameters.AddWithValue("@Name", (object)reader.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthDate", (object)reader.BirthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)reader.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)reader.Phone ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateReader(Reader reader)
        {
            string query = "UPDATE Readers SET Name = @Name, BirthDate = @BirthDate, Email = @Email, Phone = @Phone WHERE ReaderID = @ReaderID";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@ReaderID", reader.ReaderID);
                cmd.Parameters.AddWithValue("@Name", (object)reader.Name ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BirthDate", (object)reader.BirthDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)reader.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", (object)reader.Phone ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteReader(string readerID)
        {
            string query = "DELETE FROM Readers WHERE ReaderID = @ReaderID";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@ReaderID", readerID);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllReaders()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Readers";
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