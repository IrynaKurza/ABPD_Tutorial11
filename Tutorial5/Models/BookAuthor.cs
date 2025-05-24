using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial5.Models;

[PrimaryKey(nameof(BookId), nameof(AuthorId))]
[Table("Book_Author")]
public class BookAuthor
{
    [ForeignKey(nameof(Book))]
    public int BookId { get; set; }
    [ForeignKey(nameof(Author))]
    public int AuthorId { get; set; }
    
    [MaxLength(200)]
    public string Notes { get; set; }

    public Book Book { get; set; }
    public Author Author { get; set; }
}