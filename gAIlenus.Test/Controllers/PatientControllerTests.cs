using gAIlenus.Core;
using Moq;


namespace gAIlenus.Test
{
    public class PatientControllerTests
    {
        private readonly Mock<IPatientService> _mockPatientService;
        private readonly PatientController _controller;

        public PatientControllerTests()
        {
            _mockPatientService = new Mock<IPatientService>();
            _controller = new PatientController(_mockPatientService.Object);
        }

        [Fact]
        public async Task CreatePatient_ReturnsSuccessResponse()
        {
            var inputDto = new PatientInputDto();
            var expected = new PatientDto();

            _mockPatientService.Setup(s => s.CreatePatientAsync(inputDto)).ReturnsAsync(expected);

            var result = await _controller.CreatePatient(inputDto);
            var response = Assert.IsType<ApiResponseDto<PatientDto>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal(expected, response.Data);
        }

        [Fact]
        public async Task CreateDiagnosis_ReturnsSuccessResponse()
        {
            var inputDto = new DiagnosisInputDto();
            var expected = new DiagnosisDto();

            _mockPatientService.Setup(s => s.CreateDiagnosisAsync(inputDto)).ReturnsAsync(expected);

            var result = await _controller.CreateDiagnosis(inputDto);
            var response = Assert.IsType<ApiResponseDto<DiagnosisDto>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal(expected, response.Data);
        }

        [Fact]
        public async Task GetPatients_ReturnsPatientList()
        {
            var expectedList = new List<PatientDto> { new PatientDto() };

            _mockPatientService.Setup(s => s.GetPatientsAsync()).ReturnsAsync(expectedList);

            var result = await _controller.GetPatients();
            var response = Assert.IsType<ApiResponseDto<IEnumerable<PatientDto>>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal(expectedList, response.Data);
        }

        [Fact]
        public async Task GetPatient_ReturnsPatient()
        {
            int id = 1;
            var expected = new PatientDiagnosesDto();

            _mockPatientService.Setup(s => s.GetPatientAsync(id)).ReturnsAsync(expected);

            var result = await _controller.GetPatient(id);
            var response = Assert.IsType<ApiResponseDto<PatientDiagnosesDto>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal(expected, response.Data);
        }

        [Fact]
        public async Task DeletePatient_ReturnsSuccessMessage_WhenFound()
        {
            int id = 1;
            _mockPatientService.Setup(s => s.DeletePatientAsync(id)).ReturnsAsync(true);

            var result = await _controller.DeletePatient(id);
            var response = Assert.IsType<ApiResponseDto<string>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal("Patient is deleted.", response.Message);
        }

        [Fact]
        public async Task DeletePatient_ReturnsNotFoundMessage_WhenNotFound()
        {
            int id = 42;
            _mockPatientService.Setup(s => s.DeletePatientAsync(id)).ReturnsAsync(false);

            var result = await _controller.DeletePatient(id);
            var response = Assert.IsType<ApiResponseDto<string>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal($"Patient is not found with ID {id}.", response.Message);
        }

        [Fact]
        public async Task UpdatePatient_ReturnsUpdatedPatient()
        {
            int id = 1;
            var inputDto = new PatientInputDto { Name = "John", Surname = "Doe" };
            var expected = new PatientDto { Id = id, Name = "John", Surname = "Doe" };

            _mockPatientService.Setup(s => s.UpdatePatientAsync(It.IsAny<PatientDto>())).ReturnsAsync(expected);

            var result = await _controller.UpdatePatient(id, inputDto);
            var response = Assert.IsType<ApiResponseDto<PatientDto>>(result.Value);

            Assert.True(response.Success);
            Assert.Equal(expected, response.Data);
        }
    }
}
