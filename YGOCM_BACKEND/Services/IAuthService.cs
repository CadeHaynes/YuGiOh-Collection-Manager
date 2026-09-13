using YGOCM_BACKEND.DTOs;

namespace YGOCM_BACKEND.Services
{
    public interface IAuthService
    {
        public Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
        public Task<AuthResponse?> RegisterAsync(RegisterRequest registerRequest);

        public string HashPassword(string password);
        public bool VerifyPassword(string password, string hashedPassword);
    }
}
