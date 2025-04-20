namespace gAIlenus.Core
{
    public record GAIlenusResponseDto
    {
        public string Diagnosis { get; set; } = string.Empty;
        public double Confidence { get; set; }
        public string RiskLevel { get; set; } = string.Empty;
        public string GAIlenusRemarks { get; set; } = string.Empty;
    }
}
