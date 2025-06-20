using System.Data;

namespace LibraryManagement
{
    public interface IReaderRepository
    {
        void AddReader(Reader reader);
        void UpdateReader(Reader reader);
        void DeleteReader(string readerID);
        DataTable GetAllReaders();
    }
}