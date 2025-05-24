using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;

namespace Tutorial5.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<BookWithAuthorsDto>> GetBooks()
    {
        var books = await _context.Books.Select(e =>
        new BookWithAuthorsDto {
            Name = e.Name,
            Price = e.Price,
            Authors = e.BookAuthors.Select(a =>
            new AuthorDto {
                FirstName = a.Author.FirstName,
                LastName = a.Author.LastName
            }).ToList()
        }).ToListAsync();
        return books;
    }
}