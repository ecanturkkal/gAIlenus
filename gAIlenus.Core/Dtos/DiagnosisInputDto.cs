namespace gAIlenus.Core
{
    public record DiagnosisInputDto
    {
        public int PatientId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ComplaintOfPatient { get; set; } = string.Empty;
        public string DiagnosisOfDoctor { get; set; } = string.Empty;
        public string DoctorRemarks { get; set; } = string.Empty;
    }
}