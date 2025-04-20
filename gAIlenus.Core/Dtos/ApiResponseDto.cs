namespace gAIlenus.Core
{
    public record ApiResponseDto<T>(bool Success, string Message, T? Data);
}