using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs.Requests;
using Tutorial5.Services;

namespace Tutorial5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsService : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionsService(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(CreatePrescriptionRequestDto request)
    {
        try
        {
            await _service.AddPrescriptionAsync(request);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}