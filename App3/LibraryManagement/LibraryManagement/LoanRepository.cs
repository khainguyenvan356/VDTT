using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class LoanRepository : IloanRepository
    {
        private readonly SqlConnection _connection;

        public LoanRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public void AddLoan(Loan loan)
        {
            string query = @"INSERT INTO Loans (LoanID, ReaderID, BookID, BorrowDate, DueDate, Status) 
                           VALUES (@LoanId, @ReaderId, @BookId, @BorrowDate, @DueDate, @Status)";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
                cmd.Parameters.AddWithValue("@ReaderId", loan.ReaderId);
                cmd.Parameters.AddWithValue("@BookId", loan.BookId);
                cmd.Parameters.AddWithValue("@BorrowDate", loan.BorrowDate);
                cmd.Parameters.AddWithValue("@DueDate", loan.DueDate);
                cmd.Parameters.AddWithValue("@Status", loan.Status);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateLoan(Loan loan)
        {
            string query = @"UPDATE Loans SET ReturnDate = @ReturnDate, Status = @Status, FineAmount = @FineAmount 
                           WHERE LoanID = @LoanId";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@LoanId", loan.LoanId);
                cmd.Parameters.AddWithValue("@ReturnDate", (object)loan.ReturnDate ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", loan.Status);
                cmd.Parameters.AddWithValue("@FineAmount", (object)loan.FineAmount ?? DBNull.Value);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteLoan(string loanId)
        {
            string query = "DELETE FROM Loans WHERE LoanID = @LoanId";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@LoanId", loanId);
                cmd.ExecuteNonQuery();
            }
        }

        public DataTable GetAllLoans()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM Loans";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        public int CheckStock(string bookId)
        {
            string query = "SELECT Quantity FROM Books WHERE BookID = @BookId";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                {
                    // Có thể log ra bookId để kiểm tra
                    throw new InvalidOperationException($"BookID '{bookId}' không tồn tại trong bảng Books.");
                }
                return (int)result;
            }
        }

        public void UpdateStockOnBorrow(string bookId)
        {
            string query = "UPDATE Books SET Quantity = Quantity - 1 WHERE BookId = @BookId";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateStockOnReturn(string bookId)
        {
            string query = "UPDATE Books SET Quantity = Quantity + 1 WHERE BookId = @BookId";
            using (SqlCommand cmd = new SqlCommand(query, _connection))
            {
                cmd.Parameters.AddWithValue("@BookId", bookId);
                cmd.ExecuteNonQuery();
            }
        }

        public Loan GetLoanById(string loanId)
        {
            throw new NotImplementedException();
        }
    }
}