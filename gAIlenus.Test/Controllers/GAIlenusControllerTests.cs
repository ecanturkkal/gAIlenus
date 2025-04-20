using gAIlenus.Core;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace gAIlenus.Test
{
    public class GAIlenusControllerTests
    {
        private readonly Mock<IGAIlenusService> _gAIlenusServiceMock;
        private readonly GAIlenusController _controller;

        public GAIlenusControllerTests()
        {
            _gAIlenusServiceMock = new Mock<IGAIlenusService>();
            _controller = new GAIlenusController(_gAIlenusServiceMock.Object);
        }

        [Fact]
        public async Task Predict_ReturnsSuccessResponse_WhenServiceReturnsResponse()
        {
            // Arrange
            var request = new GAIlenusRequestDto();
            var expectedResponse = new GAIlenusResponseDto();

            _gAIlenusServiceMock.Setup(x => x.AskToGAIlenus(request)).ReturnsAsync(expectedResponse);

            // Act
            var result = await _controller.AskToGAIlenus(request);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponseDto<GAIlenusResponseDto>>>(result);
            var response = Assert.IsType<ApiResponseDto<GAIlenusResponseDto>>(actionResult.Value);

            Assert.True(response.Success);
            Assert.Equal(string.Empty, response.Message);
            Assert.Equal(expectedResponse, response.Data);
        }

        [Fact]
        public async Task Predict_ReturnsErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var request = new GAIlenusRequestDto();
            var exceptionMessage = "AI service error";

            _gAIlenusServiceMock.Setup(x => x.AskToGAIlenus(request)).ThrowsAsync(new Exception(exceptionMessage));

            // Act
            var result = await _controller.AskToGAIlenus(request);

            // Assert
            var actionResult = Assert.IsType<ActionResult<ApiResponseDto<GAIlenusResponseDto>>>(result);
            var response = Assert.IsType<ApiResponseDto<GAIlenusResponseDto>>(actionResult.Value);

            Assert.False(response.Success);
            Assert.Equal(exceptionMessage, response.Message);
        }
    }
}
