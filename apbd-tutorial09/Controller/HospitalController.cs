using apbd_tutorial09.DTOs;
using apbd_tutorial09.Exceptions;
using apbd_tutorial09.Services.Abstraction;
using apbd_tutorial09.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace apbd_tutorial09.Controller;

[ApiController]
[Route("api")]
public class HospitalController: ControllerBase
{
    private readonly IHospitalService _hospitalService;

    public HospitalController(IHospitalService hospitalService)
    {
        _hospitalService = hospitalService;
    }

    [HttpPost("prescriptions")]
    public async Task<IActionResult> AddPrescriptionWithPatientAsync([FromBody] PrescriptionRequestDto prescriptionRequestDto)
    {
        await _hospitalService.AddPrescriptionWithPatient(prescriptionRequestDto);
        
        return Ok();
    }
    
    [HttpGet("patients/{id}")]
    public async Task<IActionResult> GetPatientInformation(int id)
    {
        var result = await _hospitalService.GetPatientDetailsAsync(id);
        
        return Ok(result);
    }
}