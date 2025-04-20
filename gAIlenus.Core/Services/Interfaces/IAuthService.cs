namespace gAIlenus.Core
{
    public interface IAuthService
    {
        Task<UserRegisterDto> RegisterAsync(UserRegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}
