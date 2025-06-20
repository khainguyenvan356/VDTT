namespace LibraryManagement
{
    internal interface IAuthentication
    {
            bool Login(string username, string password);
    }
}