using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs;
using Tutorial5.Models;

namespace Tutorial5.Services;

public class PrescriptionService : IPrescriptionService
{
    private readonly DatabaseContext _context;

    public PrescriptionService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task AddPrescriptionAsync(CreatePrescriptionRequestDto request)
    {
        // Validation
        await ValidateRequestAsync(request);

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            // Handle patient - create if doesn't exist
            var patient = await GetOrCreatePatientAsync(request.Patient);

            // Verify doctor exists
            await ValidateDoctorExistsAsync(request.IdDoctor);

            // Create prescription
            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = request.IdDoctor,
                PrescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
                {
                    IdMedicament = m.IdMedicament,
                    Dose = m.Dose,
                    Details = m.Description
                }).ToList()
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task ValidateRequestAsync(CreatePrescriptionRequestDto request)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Patient == null)
            throw new ArgumentException("Patient information is required");

        if (request.DueDate < request.Date)
            throw new ArgumentException("DueDate must be later than Date");

        if (request.Medicaments == null || !request.Medicaments.Any())
            throw new ArgumentException("At least one medicament is required");

        if (request.Medicaments.Count > 10)
            throw new ArgumentException("Maximum 10 medicaments allowed");

        // Validate all medicaments exist
        var medicamentIds = request.Medicaments.Select(m => m.IdMedicament).Distinct().ToList();
        var existingMedicaments = await _context.Medicaments
            .Where(m => medicamentIds.Contains(m.IdMedicament))
            .Select(m => m.IdMedicament)
            .ToListAsync();

        if (existingMedicaments.Count != medicamentIds.Count)
        {
            var missingIds = medicamentIds.Except(existingMedicaments);
            throw new ArgumentException($"Medicaments with IDs {string.Join(", ", missingIds)} not found");
        }

        // Validate no duplicate medicaments in request
        if (medicamentIds.Count != request.Medicaments.Count)
            throw new ArgumentException("Duplicate medicaments are not allowed in a prescription");
    }

    private async Task<Patient> GetOrCreatePatientAsync(PatientDto patientDto)
    {
        var existingPatient = await _context.Patients.FindAsync(patientDto.IdPatient);
        
        if (existingPatient != null)
        {
            // Optionally update patient information if it has changed
            if (existingPatient.FirstName != patientDto.FirstName ||
                existingPatient.LastName != patientDto.LastName ||
                existingPatient.Birthdate != patientDto.Birthdate)
            {
                existingPatient.FirstName = patientDto.FirstName;
                existingPatient.LastName = patientDto.LastName;
                existingPatient.Birthdate = patientDto.Birthdate;
            }
            return existingPatient;
        }

        var newPatient = new Patient
        {
            IdPatient = patientDto.IdPatient,
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Birthdate = patientDto.Birthdate
        };

        _context.Patients.Add(newPatient);
        return newPatient;
    }

    private async Task ValidateDoctorExistsAsync(int doctorId)
    {
        var doctorExists = await _context.Doctors.AnyAsync(d => d.IdDoctor == doctorId);
        if (!doctorExists)
            throw new ArgumentException($"Doctor with ID {doctorId} not found");
    }
}