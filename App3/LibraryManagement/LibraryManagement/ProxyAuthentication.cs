namespace LibraryManagement
{
    public class ProxyAuthentication : IAuthentication
    {
        private RealAuthentication realAuth = new RealAuthentication();

        public bool Login(string username, string password)
        {
            // Kiểm tra đầu vào
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                // Gọi real authentication
                return realAuth.Login(username, password);
            }
        }
    }
}