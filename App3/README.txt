1) Tải SQL Management 
2) Tạo bộ CSDL (Từ file Database.txt)
3) Kết nối CSDL từ Visual Studio ( Mở bằng Ctrl + Alt + S )
4) Thay SqlConnection conn = new SqlConnection("Data Source=LocalHost;Initial Catalog=Library;Integrated Security=True;TrustServerCertificate=True"); 
Thay LocalHost = tên Server 
- Nằm trong 3 file: 	+ BookManagement.cs
			+ LoanManagement.cs
			+ ReaderManagerment.cs
5) Chạy LibraryManagement.csproj