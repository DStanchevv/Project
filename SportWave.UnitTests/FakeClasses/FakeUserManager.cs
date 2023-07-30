using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using SportWave.Data;
using SportWave.Data.Models;

namespace SportWave.UnitTests.FakeClasses
{
    public class FakeUserManager : UserManager<ApplicationUser>
    {
        public FakeUserManager(SportWaveDbContext context) : base(
            new Mock<IUserSecurityStampStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object
        )
        {
        }
    }
}
