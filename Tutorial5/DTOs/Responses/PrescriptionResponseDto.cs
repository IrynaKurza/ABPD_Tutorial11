namespace Tutorial5.DTOs.Responses;

public class PrescriptionResponseDto
{
    public int IdPrescription { get; set; }
    public DateTime Date { get; set; }
    public DateTime DueDate { get; set; }
    public List<MedicamentResponseDto> Medicaments { get; set; }
    public DoctorResponseDto Doctor { get; set; }
}