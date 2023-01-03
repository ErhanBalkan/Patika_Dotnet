using Microsoft.EntityFrameworkCore;

public class BookStoreDbContext : DbContext
{
    public BookStoreDbContext(DbContextOptions options) : base(options){}

    public DbSet<Book> Books {get; set;}
}