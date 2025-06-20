using System;
using System.Data;
using System.Windows.Forms;
using System.Windows.Input;

namespace LibraryManagement
{
    public interface ICommand
    {
        void Execute();
    }
    internal class BorrowCommand : ICommand
    {
        private Loan loan;
        private IloanRepository loanRepository;

        public BorrowCommand(Loan loan, IloanRepository loanRepository)
        {
            this.loan = loan;
            this.loanRepository = loanRepository;
        }

        public void Execute()
        {
            int remainingStock = loanRepository.CheckStock(loan.BookId);
            if (remainingStock <= 0)
            {
                MessageBox.Show("Sách đã hết, vui lòng chọn sách khác để mượn.", "Lỗi mượn sách", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            loan.Status = "Chưa trả";
            loanRepository.AddLoan(loan);
            loanRepository.UpdateStockOnBorrow(loan.BookId);

            MessageBox.Show($"Độc giả {loan.ReaderId} đã mượn sách thành công", "Mượn sách", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (remainingStock - 1 == 1)
            {
                MessageBox.Show($"Cảnh báo: Sách {loan.BookId} chỉ còn 1 bản!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public class ReturnCommand : ICommand
    {
        private string loanId;
        private IloanRepository loanRepository;
        private FineStrategyManager fineManager;

        public ReturnCommand(string loanId, IloanRepository loanRepository)
        {
            this.loanId = loanId;
            this.loanRepository = loanRepository;
            this.fineManager = new FineStrategyManager();
        }

        public void Execute()
        {
            if (string.IsNullOrEmpty(loanId))
            {
                MessageBox.Show("Hãy thêm mã phiếu mượn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var loans = loanRepository.GetAllLoans();
            DataRow loanRow = null;
            foreach (DataRow row in loans.Rows)
            {
                if (row["LoanId"].ToString() == loanId)
                {
                    loanRow = row;
                    break;
                }
            }

            if (loanRow == null)
            {
                MessageBox.Show("Không tìm thấy phiếu mượn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (loanRow["Status"] != DBNull.Value && loanRow["Status"].ToString() == "Đã trả")
            {
                MessageBox.Show("Sách này đã được trả!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Loan loan = new Loan
            {
                LoanId = loanId,
                ReaderId = loanRow["ReaderId"].ToString(),
                BookId = loanRow["BookId"].ToString(),
                BorrowDate = (DateTime)loanRow["BorrowDate"],
                DueDate = (DateTime)loanRow["DueDate"],
                ReturnDate = DateTime.Now, 
                Status = "Đã trả"
            };

            decimal fine = fineManager.CalculateTotalFine(loan.DueDate, loan.ReturnDate.Value);
            loan.FineAmount = fine > 0 ? fine : (decimal?)null;

            loanRepository.UpdateLoan(loan);
            loanRepository.UpdateStockOnReturn(loan.BookId);

            MessageBox.Show($"Trả sách {loan.ReaderId} thành công", "Trả sách", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (fine > 0)
            {
                MessageBox.Show($"Tiền trả muộn: {fine} VND", "Tiền trả muộn", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}