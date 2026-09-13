using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace YGOCM_BACKEND.DTOs
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string Username { get; set; }
        public int UserId { get; set; }
    }
}
