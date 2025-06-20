using System;

namespace LibraryManagement
{
    public interface ICommand
    {
        void Execute();
    }

    public class BorrowCommand : ICommand
    {
        private Loan loan;
        private LoanNotification notification;
        private ILoanRepository loanRepository;

        public BorrowCommand(Loan loan, LoanNotification notification, ILoanRepository loanRepository)
        {
            this.loan = loan;
            this.notification = notification;
            this.loanRepository = loanRepository;
        }

        public void Execute()
        {
            int remainingStock = loanRepository.CheckStock(loan.BookId);
            if (remainingStock <= 0)
            {
                notification.Notify("Book is out of stock, cannot borrow!");
                return;
            }

            loan.Status = "Not Returned";
            loanRepository.AddLoan(loan);
            loanRepository.UpdateStockOnBorrow(loan.BookId);

            notification.Notify($"Successfully borrowed book for reader {loan.ReaderId}");
            if (remainingStock - 1 == 1)
            {
                notification.Notify($"Warning: Book {loan.BookId} has only 1 copy left!");
            }
        }
    }

    public class ReturnCommand : ICommand
    {
        private string loanId;
        private LoanNotification notification;
        private ILoanRepository loanRepository;
        private IFineCalculationStrategy fineStrategy;

        public ReturnCommand(string loanId, LoanNotification notification, ILoanRepository loanRepository, IFineCalculationStrategy fineStrategy)
        {
            this.loanId = loanId;
            this.notification = notification;
            this.loanRepository = loanRepository;
            this.fineStrategy = fineStrategy;
        }

        public void Execute()
        {
            System.Data.DataTable loans = loanRepository.GetAllLoans();
            System.Data.DataRow loanRow = null;
            foreach (System.Data.DataRow row in loans.Rows)
            {
                if (row["LoanId"].ToString() == loanId)
                {
                    loanRow = row;
                    break;
                }
            }

            if (loanRow != null)
            {
                Loan loan = new Loan
                {
                    LoanId = loanId,
                    ReaderId = loanRow["ReaderId"].ToString(),
                    BookId = loanRow["BookId"].ToString(),
                    LoanDate = (DateTime)loanRow["LoanDate"],
                    DueDate = (DateTime)loanRow["DueDate"],
                    ReturnDate = DateTime.Now,
                    Status = "Returned"
                };

                decimal fine = fineStrategy.CalculateFine(loan.DueDate, loan.ReturnDate.Value);
                loan.FineAmount = fine > 0 ? fine : (decimal?)null;

                loanRepository.UpdateLoan(loan);
                loanRepository.UpdateStockOnReturn(loan.BookId);

                notification.Notify($"Successfully returned book for reader {loan.ReaderId}");
                if (fine > 0)
                {
                    notification.Notify($"Late return fine: {fine} VND");
                }
            }
            else
            {
                notification.Notify("Loan not found!");
            }
        }
    }
}