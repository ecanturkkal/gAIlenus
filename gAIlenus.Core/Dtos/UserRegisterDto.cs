namespace gAIlenus.Core
{
    public record UserRegisterDto
    {
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
