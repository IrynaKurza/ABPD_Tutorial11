using System.ComponentModel.DataAnnotations;

namespace Tutorial5.Models;


public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateTime Birthdate { get; set; }
    
    public ICollection<Prescription> Prescriptions { get; set; } = new HashSet<Prescription>();
}