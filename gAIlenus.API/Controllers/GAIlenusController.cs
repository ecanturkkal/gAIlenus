using gAIlenus.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class GAIlenusController : ControllerBase
{
    private readonly IGAIlenusService _gAIlenusServise;

    public GAIlenusController(IGAIlenusService gAIlenusServise)
    {
        _gAIlenusServise = gAIlenusServise;
    }

    /// <summary>
    /// Ask to AI service with patient diagnoses 
    /// </summary>
    /// <param name="DiagnosisDtos">List of patient diagnoses</param>
    /// <returns>AI prediction resulst</returns>
    [HttpPost("askToGAIlenus")]
    public async Task<ActionResult<ApiResponseDto<GAIlenusResponseDto>>> AskToGAIlenus([FromBody] GAIlenusRequestDto request)
    {
        try
        {
            // A simple prediction logic based on patient data
            var response = await _gAIlenusServise.AskToGAIlenus(request);
            return new ApiResponseDto<GAIlenusResponseDto>(true, string.Empty, response);
        }
        catch (Exception ex)
        {
            return new ApiResponseDto<GAIlenusResponseDto>(false, ex.Message, null);
        }
    }
}
