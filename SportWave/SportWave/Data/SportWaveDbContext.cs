using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SportWave.Data
{
    public class SportWaveDbContext : IdentityDbContext
    {
        public SportWaveDbContext(DbContextOptions<SportWaveDbContext> options)
            : base(options)
        {
        }
    }
}