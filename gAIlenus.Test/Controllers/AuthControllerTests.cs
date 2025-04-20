using gAIlenus.Core;
using Microsoft.AspNetCore.Mvc;
using Moq;


namespace gAIlenus.Test
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Register_ReturnsSuccessResponse_WhenModelIsValid()
        {
            // Arrange
            var dto = new UserRegisterDto();
            _authServiceMock.Setup(x => x.RegisterAsync(dto)).ReturnsAsync(dto);

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var okResult = Assert.IsType<ActionResult<ApiResponseDto<UserRegisterDto>>>(result);
            var response = Assert.IsType<ApiResponseDto<UserRegisterDto>>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal("User is registered successfully.", response.Message);
            Assert.Equal(dto, response.Data);
        }

        [Fact]
        public async Task Login_ReturnsSuccessResponse_WhenModelIsValid()
        {
            // Arrange
            var dto = new LoginDto();
            var fakeToken = "fake.jwt.token";
            _authServiceMock.Setup(x => x.LoginAsync(dto)).ReturnsAsync(fakeToken);

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var okResult = Assert.IsType<ActionResult<ApiResponseDto<string>>>(result);
            var response = Assert.IsType<ApiResponseDto<string>>(okResult.Value);
            Assert.True(response.Success);
            Assert.Equal("User login is successful.", response.Message);
            Assert.Equal(fakeToken, response.Data);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Email", "Email is required");
            var dto = new UserRegisterDto();

            // Act
            var result = await _controller.Register(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenModelIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Password", "Password is required");
            var dto = new LoginDto();

            // Act
            var result = await _controller.Login(dto);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result.Result);
        }

        [Fact]
        public async Task Register_ReturnsErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var dto = new UserRegisterDto();
            _authServiceMock.Setup(x => x.RegisterAsync(dto)).ThrowsAsync(new Exception("Unexpected error"));

            // Act
            var result = await _controller.Register(dto);

            // Assert
            var response = Assert.IsType<ApiResponseDto<UserRegisterDto>>(result.Value);
            Assert.False(response.Success);
            Assert.Equal("Unexpected error", response.Message);
        }

        [Fact]
        public async Task Login_ReturnsErrorResponse_WhenExceptionThrown()
        {
            // Arrange
            var dto = new LoginDto();
            _authServiceMock.Setup(x => x.LoginAsync(dto)).ThrowsAsync(new Exception("Login failed"));

            // Act
            var result = await _controller.Login(dto);

            // Assert
            var response = Assert.IsType<ApiResponseDto<string>>(result.Value);
            Assert.False(response.Success);
            Assert.Equal("Login failed", response.Message);
        }
    }

}
