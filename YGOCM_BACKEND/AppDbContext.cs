using Microsoft.EntityFrameworkCore;

namespace YGOCM_BACKEND
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
    }
}
