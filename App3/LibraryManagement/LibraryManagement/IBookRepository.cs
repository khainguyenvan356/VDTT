using System.Data;

namespace LibraryManagement
{
        public interface IBookRepository
        {
            void AddBook(Book book);
            void UpdateBook(Book book);
            void DeleteBook(string bookID);
            DataTable GetAllBooks();
        }
}