using Microsoft.AspNetCore.Mvc;
using Tutorial5.DTOs.Requests;
using Tutorial5.Services;

namespace Tutorial5.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _service;

    public PrescriptionsController(IPrescriptionService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> AddPrescription(CreatePrescriptionRequestDto request)
    {
        try
        {
            await _service.AddPrescriptionAsync(request);
            return Ok(new { message = "Prescription created successfully" });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { error = "An error occurred while creating the prescription" });
        }
    }
}