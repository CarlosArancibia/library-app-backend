using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
  public class LibraryDbContext : DbContext
  {
    public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
    {

    }

    public DbSet<Book> Books => Set<Book>();
  }
}