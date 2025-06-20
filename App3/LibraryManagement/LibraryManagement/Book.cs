namespace LibraryManagement
{
    public class Book
    {
        public string BookID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int? PublishYear { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
    }
}