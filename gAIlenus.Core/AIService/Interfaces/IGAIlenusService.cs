namespace gAIlenus.Core
{
    public interface IGAIlenusService
    {
        Task<GAIlenusResponseDto> AskToGAIlenus(GAIlenusRequestDto request);
    }
}
