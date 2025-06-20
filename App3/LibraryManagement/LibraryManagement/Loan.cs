using System;

namespace LibraryManagement
{
    public class Loan
    {
        public string LoanId { get; set; }
        public string ReaderId { get; set; }
        public string BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string Status { get; set; }
        public decimal? FineAmount { get; set; } // Thêm trường tính tiền phạt
    }
}