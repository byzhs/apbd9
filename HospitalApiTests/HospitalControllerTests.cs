using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apbd_tutorial09;
using apbd_tutorial09.Controller;
using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Models;
using apbd_tutorial09.Repositories.Abstraction;
using apbd_tutorial09.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HospitalApiTests;

public class HospitalControllerTests
{
    #region firstImplementation

    /*private readonly Mock<AppDbContext> _mockDbContext;
    
    private readonly Mock<IPatientRepository> _mockPatientRepository;
    private readonly Mock<IMedicamentRepository> _mockMedicamentRepository;
    private readonly Mock<IDoctorRepository> _mockDoctorRepository;
    private readonly Mock<IPrescriptionRepository> _mockPrescriptionRepository;
    private readonly Mock<IPrescriptionMedicamentRepository> _mockPrescriptionMedicamentRepository;

    private readonly Mock<IDoctorService> _mockDoctorService;
    private readonly Mock<IMedicamentService> _mockMedicamentService;
    private readonly Mock<IPatientService> _mockPatientService;
    private readonly Mock<IPrescriptionService> _mockPrescriptionService;
    private readonly Mock<IPrescriptionMedicamentService> _mockPrescriptionMedicamentService;
    private readonly Mock<IHospitalService> _mockHospitalService;
    
    private readonly HospitalController _controller;*/

    #endregion
    private readonly Mock<IHospitalService> _mockHospitalService;
    private readonly HospitalController _controller;
    public HospitalControllerTests()
    {
        #region firstImplementation

        /*_mockDbContext = new Mock<AppDbContext>();
        var patientData = new List<Patient>
        {
            new Patient
            {
                IdPatient = 1, FirstName = "John", LastName = "Doe"
            }
        }.AsQueryable();

        var mockPatientDbSet = new Mock<DbSet<Patient>>();
        mockPatientDbSet.As<IQueryable<Patient>>().Setup(m => m.Provider).Returns(patientData.Provider);
        mockPatientDbSet.As<IQueryable<Patient>>().Setup(m => m.Expression).Returns(patientData.Expression);
        mockPatientDbSet.As<IQueryable<Patient>>().Setup(m => m.ElementType).Returns(patientData.ElementType);
        mockPatientDbSet.As<IQueryable<Patient>>().Setup(m => m.GetEnumerator()).Returns(patientData.GetEnumerator());

        _mockDbContext.Setup(c => c.Patients).Returns(mockPatientDbSet.Object);
        
        var medicamentData = new List<Medicament>
        {
            new Medicament { IdMedicament = 1, Name = "TestMedicament1" },
            new Medicament { IdMedicament = 2, Name = "TestMedicament2" }
        }.AsQueryable();

        var mockMedicamentDbSet = new Mock<DbSet<Medicament>>();
        mockMedicamentDbSet.As<IQueryable<Medicament>>().Setup(m => m.Provider).Returns(medicamentData.Provider);
        mockMedicamentDbSet.As<IQueryable<Medicament>>().Setup(m => m.Expression).Returns(medicamentData.Expression);
        mockMedicamentDbSet.As<IQueryable<Medicament>>().Setup(m => m.ElementType).Returns(medicamentData.ElementType);
        mockMedicamentDbSet.As<IQueryable<Medicament>>().Setup(m => m.GetEnumerator()).Returns(medicamentData.GetEnumerator());

        _mockDbContext.Setup(c => c.Medicaments).Returns(mockMedicamentDbSet.Object);
        
        var doctorData = new List<Doctor>
        {
            new Doctor { IdDoctor = 1, FirstName = "Dr. John", LastName = "Doe" }
        }.AsQueryable();

        var mockDoctorDbSet = new Mock<DbSet<Doctor>>();
        mockDoctorDbSet.As<IQueryable<Doctor>>().Setup(m => m.Provider).Returns(doctorData.Provider);
        mockDoctorDbSet.As<IQueryable<Doctor>>().Setup(m => m.Expression).Returns(doctorData.Expression);
        mockDoctorDbSet.As<IQueryable<Doctor>>().Setup(m => m.ElementType).Returns(doctorData.ElementType);
        mockDoctorDbSet.As<IQueryable<Doctor>>().Setup(m => m.GetEnumerator()).Returns(doctorData.GetEnumerator());

        _mockDbContext.Setup(c => c.Doctors).Returns(mockDoctorDbSet.Object);
        
        _mockDoctorRepository = new Mock<IDoctorRepository>(_mockDbContext);
        _mockPatientRepository = new Mock<IPatientRepository>(_mockDbContext);
        _mockPrescriptionRepository = new Mock<IPrescriptionRepository>(_mockDbContext);
        _mockMedicamentRepository = new Mock<IMedicamentRepository>(_mockDbContext);
        _mockPrescriptionMedicamentRepository = new Mock<IPrescriptionMedicamentRepository>(_mockDbContext);

        _mockDoctorService = new Mock<IDoctorService>(_mockDoctorRepository);
        _mockPatientService = new Mock<IPatientService>(_mockPatientRepository);
        _mockPrescriptionService = new Mock<IPrescriptionService>(_mockPrescriptionRepository);
        _mockMedicamentService = new Mock<IMedicamentService>(_mockMedicamentRepository);
        _mockPrescriptionMedicamentService = new Mock<IPrescriptionMedicamentService>(_mockPrescriptionMedicamentRepository);
        _mockHospitalService = new Mock<IHospitalService>(_mockDoctorService,_mockPatientService,
            _mockPrescriptionService,_mockMedicamentService,_mockPrescriptionMedicamentService);
        
        _controller = new HospitalController(_mockHospitalService.Object);*/

        #endregion
        _mockHospitalService = new Mock<IHospitalService>();
        _controller = new HospitalController(_mockHospitalService.Object);
    }
    
    [Fact]
    public async Task AddPrescriptionWithPatientAsync_ReturnsOk()
    {
        
        var prescriptionRequestDto = new PrescriptionRequestDto
        {
            Patient = new PatientRequestDto { IdPatient = 1, FirstName = "John", LastName = "Doe" },
            Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Jane"},
            Medicaments = new List<MedicamentRequestDto>
            {
                new MedicamentRequestDto { IdMedicament = 1, Dose = 10, Description = "Take once daily" }
            },
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(10)
        };
        
        _mockHospitalService.Setup(s => s.AddPrescriptionWithPatient(prescriptionRequestDto)).Returns(Task.CompletedTask);

        
        var result = await _controller.AddPrescriptionWithPatientAsync(prescriptionRequestDto);

        
        var okResult = Assert.IsType<OkResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task AddPrescriptionWithPatientAsync_ReturnsBadRequest_WhenDueDateEarlierThanDate()
    {
        
        var prescriptionRequestDto = new PrescriptionRequestDto
        {
            Patient = new PatientRequestDto { IdPatient = 1, FirstName = "John", LastName = "Doe" },
            Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Jane"},
            Medicaments = new List<MedicamentRequestDto>
            {
                new MedicamentRequestDto { IdMedicament = 1, Dose = 10, Description = "Take once daily" }
            },
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(-1)
        };
        
        _mockHospitalService.Setup(service => service.AddPrescriptionWithPatient(It.IsAny<PrescriptionRequestDto>()))
            .ThrowsAsync(new DueDateEarlierThanDate());
        
        var result = await _controller.AddPrescriptionWithPatientAsync(prescriptionRequestDto);
        
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    [Fact]
    public async Task AddPrescriptionWithPatientAsync_ReturnsNotFound_WhenMedicamentNotFound()
    {
        
        var prescriptionRequestDto = new PrescriptionRequestDto
        {
            Patient = new PatientRequestDto { IdPatient = 1, FirstName = "John", LastName = "Doe" },
            Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Jane"},
            Medicaments = new List<MedicamentRequestDto>
            {
                new MedicamentRequestDto { IdMedicament = 1,Dose = 10, Description = "Take once daily" }
            },
            Date = DateTime.Now,
            DueDate = DateTime.Now.AddDays(10)
        };
        
        _mockHospitalService.Setup(service => service.AddPrescriptionWithPatient(It.IsAny<PrescriptionRequestDto>()))
            .ThrowsAsync(new MedicamentNotFound(prescriptionRequestDto.Medicaments.First().IdMedicament));
        
        var result = await _controller.AddPrescriptionWithPatientAsync(prescriptionRequestDto);
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
    }
    
    [Fact]
    public async Task AddPrescriptionWithPatientAsync_ReturnsBadRequest_WhenMedicamentListTooLong()
    {
        var prescriptionRequestDto = new PrescriptionRequestDto
    {
        Patient = new PatientRequestDto { IdPatient = 1, FirstName = "John", LastName = "Doe" },
        Doctor = new DoctorDto { IdDoctor = 1, FirstName = "Jane"},
        Medicaments = new List<MedicamentRequestDto>
        {
            new MedicamentRequestDto { IdMedicament = 1, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 2, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 3, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 4, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 5, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 6, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 7, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 8, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 9, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 10, Dose = 10, Description = "Take once daily" },
            new MedicamentRequestDto { IdMedicament = 11, Dose = 10, Description = "Take once daily" }
        },
        Date = DateTime.Now,
        DueDate = DateTime.Now.AddDays(10)
    };

    _mockHospitalService.Setup(service => service.AddPrescriptionWithPatient(It.IsAny<PrescriptionRequestDto>()))
        .ThrowsAsync(new MedicamentListTooLong(prescriptionRequestDto.Medicaments.Count));
    
    var result = await _controller.AddPrescriptionWithPatientAsync(prescriptionRequestDto);
    
    var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
    
    Assert.Equal(400, badRequestResult.StatusCode);
    }
    
    
    /*[Fact]
    public async Task GetPatientInformation_ReturnsNotFound_WhenPatientNotFound()
    {
        
        _mockHospitalService.Setup(service => service.GetPatientDetailsAsync(It.IsAny<int>()))
            .ThrowsAsync(new PatientNotFound(1));

        
        var result = await _controller.GetPatientInformation(1);

        
        var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
        Assert.Equal(404, notFoundResult.StatusCode);
        Assert.Equal("Patient with id 1 not found", notFoundResult.Value.GetType().GetProperty("Message")?.GetValue(notFoundResult.Value, null));
    }
    
    
    [Fact]
    public async Task GetPatientInformation_ReturnsOk_WhenPatientFound()
    {
        var patientResponseDto = new PatientResponseDto(); 
        _mockHospitalService.Setup(service => service.GetPatientDetailsAsync(It.IsAny<int>()))
            .ReturnsAsync(patientResponseDto);

        
        var result = await _controller.GetPatientInformation(1);

        
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
        Assert.Equal(patientResponseDto, okResult.Value);
    }*/
}