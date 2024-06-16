using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Mappers;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using Microsoft.EntityFrameworkCore;

namespace apbd_tutorial09.Repositories.Implementation;

public class PrescriptionRepository: IPrescriptionRepository
{
    private readonly AppDbContext _dbContext;

    public PrescriptionRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Prescription> AddPrescriptionAsync(Prescription prescription)
    {
        await _dbContext.Prescriptions.AddAsync(prescription);
        await _dbContext.SaveChangesAsync();
        return prescription;
    }

    public async Task<List<PrescriptionResponseDto>> GetPrescriptionsByPatientIdAsync(int id)
    {
        var prescriptions = await _dbContext.Prescriptions
            .Include(p => p.PrescriptionMedicaments)
            .ThenInclude(pm => pm.Medicament)
            .Include(p => p.Doctor)
            .Where(p => p.IdPatient == id)
            .OrderBy(p => p.DueDate)
            .ToListAsync();

        var listOfPrescriptionDto = new List<PrescriptionResponseDto>();
        foreach (var prescription in prescriptions)
        {
            var dto = prescription.PrescriptionResponseDto();
            dto.Medicaments = prescription.PrescriptionMedicaments.Select(pm => pm.Medicament.MedicamentResponseDto())
                .ToList();
            dto.Doctor = prescription.Doctor.ToDoctorDto();
        }

        return listOfPrescriptionDto;
    }

    /*public async Task AddPrescriptionWithPatient(PrescriptionRequestDto prescriptionRequestDto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            /*var isPatientExists =
                await _dbContext.Patients.AnyAsync(p => p.IdPatient == prescriptionRequestDto.Patient.IdPatient);
            if (!isPatientExists)
            {
                _dbContext.Patients.Add(prescriptionRequestDto.Patient.RequestDtoToPatient());
                await _dbContext.SaveChangesAsync();
            }

            var isDoctorExists =
                await _dbContext.Doctors.AnyAsync(d => d.IdDoctor == prescriptionRequestDto.Doctor.IdDoctor);

            if (!isDoctorExists)
            {
                _dbContext.Doctors.Add(prescriptionRequestDto.Doctor.ToDoctor());
                await _dbContext.SaveChangesAsync();
            }#1#

            var numberOfMedicaments = prescriptionRequestDto.Medicaments.Count();

            if (numberOfMedicaments > 10)
            {
                throw new MedicamentListTooLong(numberOfMedicaments);
            }

            if (prescriptionRequestDto.DueDate < prescriptionRequestDto.Date)
            {
                throw new DueDateEarlierThanDate();
            }

            // Prescription prescription = prescriptionRequestDto.RequestDtoToPrescription();
            // _dbContext.Prescriptions.Add(prescription);
            // await _dbContext.SaveChangesAsync();

            /*foreach (var medicament in prescriptionRequestDto.Medicaments)
            {
                var isMedicamentExists =
                    await _dbContext.Medicaments.AnyAsync(m => m.IdMedicament == medicament.IdMedicament);
                if (!isMedicamentExists)
                {
                    throw new MedicamentNotFound(medicament.IdMedicament);
                }

                PrescriptionMedicament prescriptionMedicament = new();
                prescriptionMedicament.IdPrescription = prescription.IdPrescription;
                prescriptionMedicament.IdMedicament = medicament.IdMedicament;
                prescriptionMedicament.Dose = medicament.Dose;
                prescriptionMedicament.Details = medicament.Description;
                _dbContext.PrescriptionMedicaments.Add(prescriptionMedicament);

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }#1#
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }*/
}