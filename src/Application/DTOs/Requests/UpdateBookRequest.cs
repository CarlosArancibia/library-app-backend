using Domain.Entities;

namespace Application.DTOs.Requests
{
    public class UpdateBookRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public int PublishYear { get; set; }
        public string Genre { get; set; } = string.Empty;
        public BookStatus Status { get; set; }
    }
}