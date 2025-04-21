using Microsoft.Extensions.Configuration;

namespace gAIlenus.Core
{
    public class GAIlenusService : IGAIlenusService
    {
        private readonly IConfiguration _config;

        public GAIlenusService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<GAIlenusResponseDto> AskToGAIlenus(GAIlenusRequestDto request)
        {
            // Waiting for a while to think something...
            await Task.Delay(3000);

            foreach (var detail in request.Details)
            {
                var ageAtDiagnosis = DateTime.UtcNow.Year - request.Birthdate.Year;
                // Do something with age and details...
            }

            // Creating mock AI response
            var random = new Random();

            var AIDiagnosises = new[] { 
                "GAIlenus-Diagnosis-1", 
                "GAIlenus-Diagnosis-2", 
                "GAIlenus-Diagnosis-3", 
                "GAIlenus-Diagnosis-4", 
                "GAIlenus-Diagnosis-5" 
            };
            // Diagnosis is made
            var diagnosis = AIDiagnosises[random.Next(AIDiagnosises.Length)];

            var AIRemarks = new[] {
                "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.",
                "Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.",
                "Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat.",
                "At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio.",
                "Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consectetur, adipisci velit, sed quia non numquam eius modi tempora incidunt ut labore et dolore magnam aliquam quaerat voluptatem." 
            };

            // Specific recommendation is determined based on the diagnosis selected.
            var remarks = AIRemarks[random.Next(AIRemarks.Length)];

            return new GAIlenusResponseDto
            {
                Diagnosis = diagnosis,
                Confidence = Math.Round(0.65 + random.NextDouble() * 0.3, 2),
                RiskLevel = random.Next(3) switch { 0 => "Low", 1 => "Middle", _ => "High" },
                GAIlenusRemarks = remarks
            };
        }
    }
}
