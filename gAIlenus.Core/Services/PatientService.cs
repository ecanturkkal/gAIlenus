using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace gAIlenus.Core
{
    public class PatientService : IPatientService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public PatientService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async  Task<PatientDto> CreatePatientAsync(PatientInputDto dto)
        {
            var patient = new Patient
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Birthdate = dto.Birthdate.Date
            };

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return _mapper.Map<PatientDto>(patient);
        }

        public async Task<DiagnosisDto> CreateDiagnosisAsync(DiagnosisInputDto dto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == dto.PatientId);
            if (patient == null)
                throw new Exception("Patient is not found.");

            var diagnosis = new Diagnosis
            {
                PatientId = dto.PatientId,
                DiagnosisDate = dto.DiagnosisDate,
                ComplaintOfPatient = dto.ComplaintOfPatient,
                DiagnosisOfDoctor = dto.DiagnosisOfDoctor,
                DoctorRemarks = dto.DoctorRemarks
            };

            _context.Diagnoses.Add(diagnosis);
            await _context.SaveChangesAsync();

            return _mapper.Map<DiagnosisDto>(diagnosis);
        }

        public async Task<PatientDto> UpdatePatientAsync(PatientDto dto)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == dto.Id);
            if (patient == null)
                throw new Exception("Patient is not found.");

            patient.Name = dto.Name;
            patient.Surname = dto.Surname;
            patient.Birthdate = dto.Birthdate.Date;

            _context.Entry(patient).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeletePatientAsync(int patientId)
        {
            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == patientId);
            if (patient == null)
                throw new Exception("Patient is not found.");

            _context.Patients.Remove(patient);

            var diagnoses = await _context.Diagnoses.Where(p => p.PatientId == patientId).ToListAsync();
            foreach (var item in diagnoses)
            {
                _context.Diagnoses.Remove(item);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PatientDiagnosesDto> GetPatientAsync(int patientId)
        {
            var patient = await _context.Patients
                .Include(p => p.Diagnoses)
                .FirstOrDefaultAsync(p => p.Id == patientId);

            if (patient == null)
                return new PatientDiagnosesDto();

            var patientWithDiagnoses = new PatientDiagnosesDto()
            {
                Id = patientId,
                Name = patient.Name,
                Surname = patient.Surname,
                Birthdate = patient.Birthdate,
                Diagnoses = new List<DiagnosisDto>()
            };

            foreach (var pd in patient.Diagnoses)
            {
                patientWithDiagnoses.Diagnoses.Add(_mapper.Map<DiagnosisDto>(pd));
            }

            return patientWithDiagnoses;
        }

        public async Task<IEnumerable<PatientDto>> GetPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return _mapper.Map<IEnumerable<PatientDto>>(patients);
        }
    }
}
