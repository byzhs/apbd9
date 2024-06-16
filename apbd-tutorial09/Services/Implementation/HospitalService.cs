using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial09.Services.Implementation;

public class HospitalService: IHospitalService
{
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;
    private readonly IMedicamentService _medicamentService;
    private readonly IPrescriptionService _prescriptionService;
    private readonly IPrescriptionMedicamentService _prescriptionMedicamentService;
    private readonly AppDbContext _dbContext;

    public HospitalService(IPatientService patientService, IDoctorService doctorService, IMedicamentService medicamentService,
        IPrescriptionService prescriptionService, IPrescriptionMedicamentService prescriptionMedicamentService, AppDbContext dbContext)
    {
        _patientService = patientService;
        _doctorService = doctorService;
        _medicamentService = medicamentService;
        _prescriptionService = prescriptionService;
        _prescriptionMedicamentService = prescriptionMedicamentService;
        _dbContext = dbContext;
    }

    public async Task AddPrescriptionWithPatient(PrescriptionRequestDto dto)
    {
        using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            await _patientService.AddPatientIfNotExistsAsync(dto.Patient);
            await _doctorService.AddDoctorIfNotExistsAsync(dto.Doctor);

            await _medicamentService.CheckMedicamentsExistAsync(dto.Medicaments);
            var prescription = await _prescriptionService.AddPrescriptionAsync(dto);
            await _prescriptionMedicamentService.AddPrescriptionMedicamentsAsync(prescription.IdPrescription,
                dto.Medicaments);

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.CommitAsync();
            throw;
        }
    }

    public async Task<PatientResponseDto> GetPatientDetailsAsync(int id)
    {
        var patientResponseDto =  await _patientService.GetPatientAsync(id);
        var prescriptionDtos = await _prescriptionService.GetPrescriptionsByPatientId(id);
        patientResponseDto.Prescriptions = prescriptionDtos;
        return patientResponseDto;
    }
}