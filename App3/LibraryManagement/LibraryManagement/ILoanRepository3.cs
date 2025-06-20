using System.Data;

namespace LibraryManagement
{
    public interface IloanRepository
    {
        void AddLoan(Loan loan);
        void UpdateLoan(Loan loan);
        void DeleteLoan(string loanId);
        DataTable GetAllLoans();
        int CheckStock(string bookId);
        void UpdateStockOnBorrow(string bookId);
        void UpdateStockOnReturn(string bookId);
    }
}