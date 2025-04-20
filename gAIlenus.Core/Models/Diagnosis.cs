using System.Text.Json.Serialization;

namespace gAIlenus.Core
{
    public class Diagnosis
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public DateTime DiagnosisDate { get; set; }
        public string ComplaintOfPatient { get; set; } = string.Empty;
        public string DiagnosisOfDoctor { get; set; } = string.Empty;
        public string DoctorRemarks { get; set; } = string.Empty;

        [JsonIgnore]
        public Patient Patient { get; set; }
    }
}
