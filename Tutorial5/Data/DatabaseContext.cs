using Microsoft.EntityFrameworkCore;
using Tutorial5.Models;

namespace Tutorial5.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Patient> Patients { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Prescription> Prescriptions { get; set; }
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigurePatient(modelBuilder);
        ConfigureDoctor(modelBuilder);
        ConfigureMedicament(modelBuilder);
        ConfigurePrescription(modelBuilder);
        ConfigurePrescriptionMedicament(modelBuilder);
        
        SeedData(modelBuilder);
    }

    private static void ConfigurePatient(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Patient>(p =>
        {
            p.ToTable("Patient");
            p.HasKey(e => e.IdPatient);
            p.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            p.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            p.Property(e => e.Birthdate).IsRequired();
            
            // Configure relationship
            p.HasMany(e => e.Prescriptions)
                .WithOne(pr => pr.Patient)
                .HasForeignKey(pr => pr.IdPatient)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureDoctor(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Doctor>(d =>
        {
            d.ToTable("Doctor");
            d.HasKey(e => e.IdDoctor);
            d.Property(e => e.FirstName).HasMaxLength(100).IsRequired();
            d.Property(e => e.LastName).HasMaxLength(100).IsRequired();
            
            // Configure relationship
            d.HasMany<Prescription>()
                .WithOne(p => p.Doctor)
                .HasForeignKey(p => p.IdDoctor)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting doctors with prescriptions
        });
    }

    private static void ConfigureMedicament(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Medicament>(m =>
        {
            m.ToTable("Medicament");
            m.HasKey(e => e.IdMedicament);
            m.Property(e => e.Name).HasMaxLength(100).IsRequired();
            m.Property(e => e.Description).HasMaxLength(100).IsRequired();
            m.Property(e => e.Type).HasMaxLength(100).IsRequired();
            
            // Configure relationship
            m.HasMany(e => e.PrescriptionMedicaments)
                .WithOne(pm => pm.Medicament)
                .HasForeignKey(pm => pm.IdMedicament)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting medicaments in use
        });
    }

    private static void ConfigurePrescription(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Prescription>(p =>
        {
            p.ToTable("Prescription");
            p.HasKey(e => e.IdPrescription);
            p.Property(e => e.Date).IsRequired();
            p.Property(e => e.DueDate).IsRequired();
            p.Property(e => e.IdPatient).IsRequired();
            p.Property(e => e.IdDoctor).IsRequired();
            
            // Configure relationships
            p.HasOne(e => e.Patient)
                .WithMany(pt => pt.Prescriptions)
                .HasForeignKey(e => e.IdPatient);
                
            p.HasOne(e => e.Doctor)
                .WithMany()
                .HasForeignKey(e => e.IdDoctor);
                
            p.HasMany(e => e.PrescriptionMedicaments)
                .WithOne(pm => pm.Prescription)
                .HasForeignKey(pm => pm.IdPrescription)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigurePrescriptionMedicament(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrescriptionMedicament>(pm =>
        {
            pm.ToTable("Prescription_Medicament");
            pm.HasKey(e => new { e.IdMedicament, e.IdPrescription });
            pm.Property(e => e.Dose).HasMaxLength(100);
            pm.Property(e => e.Details).HasMaxLength(100).IsRequired();
            
            // Configure relationships (already configured above)
            pm.HasOne(e => e.Medicament)
                .WithMany(m => m.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdMedicament);
            
            pm.HasOne(e => e.Prescription)
                .WithMany(p => p.PrescriptionMedicaments)
                .HasForeignKey(e => e.IdPrescription);
        });
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Patients
        modelBuilder.Entity<Patient>().HasData(
            new Patient { 
                IdPatient = 1, 
                FirstName = "John", 
                LastName = "Doe", 
                Birthdate = new DateTime(1990, 1, 1) 
            },
            new Patient { 
                IdPatient = 2, 
                FirstName = "Jane", 
                LastName = "Smith", 
                Birthdate = new DateTime(1985, 5, 15) 
            }
        );

        // Seed Doctors
        modelBuilder.Entity<Doctor>().HasData(
            new Doctor { 
                IdDoctor = 1, 
                FirstName = "Dr. Jane", 
                LastName = "Wilson" 
            },
            new Doctor { 
                IdDoctor = 2, 
                FirstName = "Dr. Robert", 
                LastName = "Johnson" 
            }
        );

        // Seed Medicaments
        modelBuilder.Entity<Medicament>().HasData(
            new Medicament { 
                IdMedicament = 1, 
                Name = "Aspirin", 
                Description = "Pain reliever and anti-inflammatory", 
                Type = "Tablet" 
            },
            new Medicament { 
                IdMedicament = 2, 
                Name = "Ibuprofen", 
                Description = "Anti-inflammatory pain reliever", 
                Type = "Tablet" 
            },
            new Medicament { 
                IdMedicament = 3, 
                Name = "Amoxicillin", 
                Description = "Antibiotic for bacterial infections", 
                Type = "Capsule" 
            }
        );
    }
}