namespace Tutorial5.DTOs.Responses;

public class MedicamentResponseDto
{
    public int IdMedicament { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Dose { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}