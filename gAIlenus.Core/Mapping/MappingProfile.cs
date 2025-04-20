using AutoMapper;


namespace gAIlenus.Core
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Configure mapping
            CreateMap<Patient, PatientDto>();

            CreateMap<Diagnosis, DiagnosisDto>();
        }
    }
}
