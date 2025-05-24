using Tutorial5.DTOs.Requests;

namespace Tutorial5.Services;

public interface IPrescriptionService
{
    Task AddPrescriptionAsync(CreatePrescriptionRequestDto request);
}