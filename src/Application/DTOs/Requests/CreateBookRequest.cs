namespace Application.DTOs.Requests
{
  public class CreateBookRequest
  {
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int PublishYear { get; set; }
    public string Genre { get; set; } = string.Empty;
    
  }
}