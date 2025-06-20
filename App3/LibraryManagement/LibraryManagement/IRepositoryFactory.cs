using System;
using System.Data.SqlClient;

namespace LibraryManagement
{

    public interface IRepositoryFactory
    {
        IBookRepository CreateBookRepository();
        IReaderRepository CreateReaderRepository();
        IloanRepository CreateLoanRepository();
    }

    public class SqlRepositoryFactory : IRepositoryFactory
    {
        private readonly SqlConnection _connection;

        public SqlRepositoryFactory(SqlConnection connection)
        {
            _connection = connection;
        }

        public IBookRepository CreateBookRepository()
        {
            return new BookRepository(_connection);
        }

        public IReaderRepository CreateReaderRepository()
        {
            return new ReaderRepository(_connection);
        }

        public IloanRepository CreateLoanRepository()
        {
            return new LoanRepository(_connection);
        }
    }
}
