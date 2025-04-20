using gAIlenus.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Create new patient
    /// </summary>
    /// <param name="CreatePatientDto">Main patient info</param>
    /// <returns>Created patient</returns>
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ApiResponseDto<UserRegisterDto>>> Register(UserRegisterDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _authService.RegisterAsync(dto);
            return new ApiResponseDto<UserRegisterDto>(true, "User is registered successfully.", user);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<UserRegisterDto>(false, ex.Message, null);
        }
    }

    /// <summary>
    /// Create new patient
    /// </summary>
    /// <param name="CreatePatientDto">Main patient info</param>
    /// <returns>Created patient</returns>
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponseDto<string>>> Login(LoginDto dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = await _authService.LoginAsync(dto);
            return new ApiResponseDto<string>(true, "User login is successful.", token);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<string>(false, ex.Message, null);
        }
    }
}