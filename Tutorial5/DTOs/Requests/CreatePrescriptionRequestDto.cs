namespace Tutorial5.DTOs.Requests;

public class CreatePrescriptionRequestDto
{
    public PatientDto Patient { get; set; } = null!;
    public int IdDoctor { get; set; }
    public List<MedicamentDto> Medicaments { get; set; } = null!;
    public DateTime Date { get; set; } 
    public DateTime DueDate { get; set; }
}