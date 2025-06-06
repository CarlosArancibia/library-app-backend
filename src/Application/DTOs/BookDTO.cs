namespace Application.DTOs
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublishYear { get; set; }
        public string Genre { get; set; } = string.Empty;
        public int Status { get; set; }  // Utilizo int para mapear maás fácil el enum
    }
}
