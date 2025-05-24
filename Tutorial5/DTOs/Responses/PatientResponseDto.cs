namespace Tutorial5.DTOs.Responses;

public class PatientResponseDto
{
    public int IdPatient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birthdate { get; set; }
    public List<PrescriptionResponseDto> Prescriptions { get; set; }
}