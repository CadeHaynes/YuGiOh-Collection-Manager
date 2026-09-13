namespace YGOCM_BACKEND.Services
{
    public class AuthService : IAuthService
    {
        AppDbContext _context;
        IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
    }
}
