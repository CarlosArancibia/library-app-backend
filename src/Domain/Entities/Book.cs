namespace Domain.Entities
{
  public class Book
  {
    public Guid Id { get; set; }
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public int PublishYear { get; set; }
    public string Genre { get; set; } = "";
    public BookStatus Status { get; set; } = BookStatus.Available;
  }

  public enum BookStatus
  {
    Available,
    Borrowed
  }

}