namespace Tutorial5.DTOs.Responses;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
    public List<PrescriptionResponseDto> Prescriptions { get; set; } = null!;
}