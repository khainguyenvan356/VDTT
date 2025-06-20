using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace LibraryManagement
{
    public class RealAuthentication : IAuthentication
    {
        public bool Login(string username, string password)
        {
            using (SqlConnection conn = DBConnection.GetConnection())
            {
                string query = "SELECT COUNT(*) FROM ThuThu WHERE TenDangNhap=@username AND MatKhau=@password";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                try
                {
                    conn.Open();
                    Console.WriteLine("Kết nối thành công: " + conn.State);
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        throw new InvalidOperationException("Kết nối không thể mở.");
                    }
                    int result = (int)cmd.ExecuteScalar();
                    return result > 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi: {ex.Message}");
                    return false;
                }
            }
        }
    }
}