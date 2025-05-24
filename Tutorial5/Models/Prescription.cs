using System.ComponentModel.DataAnnotations;

namespace Tutorial5.Models;

public class Prescription
{
    [Key]
    public int IdPrescription { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    public DateTime DueDate { get; set; }
    
    [Required]
    public int IdPatient { get; set; }
    public Patient Patient { get; set; } = new Patient();
    
    [Required]
    public int IdDoctor { get; set; }
    public Doctor Doctor { get; set; } = new Doctor();
    
    public ICollection<PrescriptionMedicament> PrescriptionMedicaments { get; set; } = new List<PrescriptionMedicament>();
}