namespace gAIlenus.Core
{
    public record PatientDiagnosesDto : PatientDto
    {
        public ICollection<DiagnosisDto> Diagnoses { get; set; }
    }
}
