using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tutorial5.Models;

public class PrescriptionMedicament
{
    [Key]
    [ForeignKey("Medicament")]
    public int IdMedicament { get; set; }
    
    [Key]
    [ForeignKey("Prescription")]
    public int IdPrescription { get; set; }
    
    [MaxLength(100)]
    public string Dose { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Details { get; set; }
    
    public Medicament Medicament { get; set; }
    public Prescription Prescription { get; set; }
}