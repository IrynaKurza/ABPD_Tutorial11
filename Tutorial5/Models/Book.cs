using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Tutorial5.Models;

[Table("Book")]
public class Book
{
    [Key]
    public int BookId { get; set; }
    [MaxLength(200)]
    public string Name { get; set; }
    [Column(TypeName = "decimal")]
    [Precision(10, 2)]
    public double Price { get; set; }

    public ICollection<BookAuthor> BookAuthors { get; set; }
}