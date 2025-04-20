using gAIlenus.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class PatientController : ControllerBase
{
    private readonly IPatientService _patientService;

    public PatientController(IPatientService patientService)
    {
        _patientService = patientService;
    }

    /// <summary>
    /// Create new patient
    /// </summary>
    /// <param name="PatientInputDto">Main patient info</param>
    /// <returns>Created patient</returns>
    [HttpPost("createPatient")]
    public async Task<ActionResult<ApiResponseDto<PatientDto>>> CreatePatient([FromBody] PatientInputDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patient = await _patientService.CreatePatientAsync(dto);
            return new ApiResponseDto<PatientDto>(true, "Patient is created.", patient);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<PatientDto>(false, ex.Message, null);
        }
    }

    /// <summary>
    /// Create new patient
    /// </summary>
    /// <param name="PatientInputDto">Main patient info</param>
    /// <returns>Created patient</returns>
    [HttpPost("createDiagnosis")]
    public async Task<ActionResult<ApiResponseDto<DiagnosisDto>>> CreateDiagnosis([FromBody] DiagnosisInputDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diagnosis = await _patientService.CreateDiagnosisAsync(dto);
            return new ApiResponseDto<DiagnosisDto>(true, "Diagnosis is created.", diagnosis);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<DiagnosisDto>(false, ex.Message, null);
        }
    }

    /// <summary>
    /// List all patients
    /// </summary>
    /// <returns>Patient list</returns>
    [HttpGet("getPatients")]
    public async Task<ActionResult<ApiResponseDto<IEnumerable<PatientDto>>>> GetPatients()
    {
        try
        {
            var patients = await _patientService.GetPatientsAsync();
            return new ApiResponseDto<IEnumerable<PatientDto>>(true, string.Empty, patients);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<IEnumerable<PatientDto>>(false, ex.Message, null);
        }
    }

    /// <summary>
    /// Get patient
    /// </summary>
    /// <param name="id">patient ID</param>
    /// <returns>Existing patient</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponseDto<PatientDiagnosesDto>>> GetPatient(int id)
    {
        try
        {
            var patientInfo = await _patientService.GetPatientAsync(id);
            return new ApiResponseDto<PatientDiagnosesDto>(true, string.Empty, patientInfo);

        }
        catch (Exception ex)
        {
            return new ApiResponseDto<PatientDiagnosesDto>(false, ex.Message, null);
        }
    }

    /// <summary>
    /// Delete a patient by its ID
    /// </summary>
    /// <param name="id">patient ID</param>
    /// <returns>Deletion result</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponseDto<string>>> DeletePatient(int id)
    {
        try
        {
            var deleted = await _patientService.DeletePatientAsync(id);
            var message = deleted ? "Patient is deleted." : $"Patient is not found with ID {id}.";
            return new ApiResponseDto<string>(true, message, string.Empty);
                
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<string>(false, ex.Message, null);
        }
    }

    /// <summary>
    /// Update patient
    /// </summary>
    /// <param name="PatientInputDto">Main patient info</param>
    /// <returns>Updated patient</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponseDto<PatientDto>>> UpdatePatient(int id, [FromBody] PatientInputDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var patientDto = new PatientDto
            {
                Id = id,
                Name = dto.Name,
                Surname = dto.Surname,
                Birthdate = dto.Birthdate
            };

            var patient = await _patientService.UpdatePatientAsync(patientDto);
            return new ApiResponseDto<PatientDto>(true, "Patient is updated.", patient);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<PatientDto>(false, ex.Message, null);
        }
    }
}
