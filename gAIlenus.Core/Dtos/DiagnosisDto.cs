namespace gAIlenus.Core
{
    public record DiagnosisDto 
    {
        public int Id { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ComplaintOfPatient { get; set; } = string.Empty;
        public string DiagnosisOfDoctor { get; set; } = string.Empty;
        public string DoctorRemarks { get; set; } = string.Empty;
    }
}
