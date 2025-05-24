using Microsoft.EntityFrameworkCore;
using Tutorial5.Models;

namespace Tutorial5.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(a =>
        {
            a.ToTable("Author");
            
            a.HasKey(e => e.AuthorId);
            a.Property(e => e.FirstName).HasMaxLength(100);
            a.Property(e => e.LastName).HasMaxLength(200);
        });

        modelBuilder.Entity<Author>().HasData(new List<Author>()
        {
            new Author() { AuthorId = 1, FirstName = "Jane", LastName = "Doe"},
            new Author() { AuthorId = 2, FirstName = "John", LastName = "Doe"},
        });
        
        modelBuilder.Entity<Book>().HasData(new List<Book>()
        {
            new Book() { BookId = 1, Name = "Book 1", Price = 10.2 },
            new Book() { BookId = 2, Name = "Book 2", Price = 123.2 },
        });
        
        modelBuilder.Entity<BookAuthor>().HasData(new List<BookAuthor>()
        {
            new BookAuthor() { AuthorId = 1, BookId = 1, Notes = "n1" },
            new BookAuthor() { AuthorId = 2, BookId = 1, Notes = "n2" },
        });
    }
}