namespace gAIlenus.Core
{
    public record GAIlenusRequestDto
    {
        public DateTime Birthdate { get; set; }
        public ICollection<Detail> Details { get; set; }
    }

    public record Detail
    {
        public DateTime DiagnosisDate { get; set; }
        public string ComplaintOfPatient { get; set; } = string.Empty;
        public string DiagnosisOfDoctor { get; set; } = string.Empty;
        public string DoctorRemarks { get; set; } = string.Empty;
    }
}