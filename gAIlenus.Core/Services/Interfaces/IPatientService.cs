namespace gAIlenus.Core
{
    public interface IPatientService
    {
        Task<PatientDto> CreatePatientAsync(PatientInputDto dto);
        Task<DiagnosisDto> CreateDiagnosisAsync(DiagnosisInputDto dto);
        Task<PatientDto> UpdatePatientAsync(PatientDto dto);
        Task<bool> DeletePatientAsync(int patientId);
        Task<PatientDiagnosesDto> GetPatientAsync(int patientId);
        Task<IEnumerable<PatientDto>> GetPatientsAsync();
    }
}
