using Microsoft.EntityFrameworkCore;
using Tutorial5.Data;
using Tutorial5.DTOs.Requests;
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
            // Handle patient - create if doesn't exist or find existing by personal data
            var patient = await GetOrCreatePatientAsync(request.Patient);

            // Verify doctor exists
            await ValidateDoctorExistsAsync(request.IdDoctor);

            // Create prescription
            var prescription = new Prescription
            {
                Date = request.Date,
                DueDate = request.DueDate,
                IdPatient = patient.IdPatient,
                IdDoctor = request.IdDoctor
            };

            _context.Prescriptions.Add(prescription);
            await _context.SaveChangesAsync(); // Save to get the prescription ID

            // Add prescription medicaments
            var prescriptionMedicaments = request.Medicaments.Select(m => new PrescriptionMedicament
            {
                IdPrescription = prescription.IdPrescription,
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            }).ToList();

            _context.PrescriptionMedicaments.AddRange(prescriptionMedicaments);
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
            throw new ArgumentException("DueDate must be later than or equal to Date");

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
        // If IdPatient is provided and > 0, try to find existing patient
        if (patientDto.IdPatient > 0)
        {
            var existingPatient = await _context.Patients.FindAsync(patientDto.IdPatient);
            if (existingPatient != null)
            {
                return existingPatient;
            }
        }

        // Try to find patient by personal data (FirstName, LastName, Birthdate)
        var patientByData = await _context.Patients
            .FirstOrDefaultAsync(p => 
                p.FirstName == patientDto.FirstName &&
                p.LastName == patientDto.LastName &&
                p.Birthdate.Date == patientDto.Birthdate.Date);

        if (patientByData != null)
        {
            return patientByData;
        }

        // Create new patient (let EF handle ID generation)
        var newPatient = new Patient
        {
            FirstName = patientDto.FirstName,
            LastName = patientDto.LastName,
            Birthdate = patientDto.Birthdate
        };

        _context.Patients.Add(newPatient);
        await _context.SaveChangesAsync(); // Save to get the generated ID
        return newPatient;
    }

    private async Task ValidateDoctorExistsAsync(int doctorId)
    {
        var doctorExists = await _context.Doctors.AnyAsync(d => d.IdDoctor == doctorId);
        if (!doctorExists)
            throw new ArgumentException($"Doctor with ID {doctorId} not found");
    }
}