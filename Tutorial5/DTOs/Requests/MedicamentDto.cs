namespace Tutorial5.DTOs.Requests;

public class MedicamentDto
{
    public int IdMedicament { get; set; }
    public string Dose { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}