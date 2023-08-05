using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;
using SportWave.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace SportWave.UnitTestss.Services
{
    public class ChatServiceTests
    {
        [Fact]
        public async Task SaveMsgAsyncSavesNotNullMessages()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IChatService service = new ChatService(context);

            await context.SaveChangesAsync();

            await service.SaveMsgAsync("Test", "Test");

            var msg = context.Messages.Where(m => m.UserName == "Test" && m.Msg == "Test");

            Assert.Equal(1, msg.Count());

            context.Dispose();
        }

        [Fact]
        public async Task SaveMsgAsyncDoesNotSaveNullMessages()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IChatService service = new ChatService(context);

            await context.SaveChangesAsync();

            await service.SaveMsgAsync("Test", null);

            var msg = context.Messages.Where(m => m.UserName == "Test");

            Assert.Equal(0, msg.Count());

            context.Dispose();
        }

        [Fact]
        public async Task SaveMsgAsyncDoesNotSaveEmptyMessages()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IChatService service = new ChatService(context);

            await context.SaveChangesAsync();

            await service.SaveMsgAsync("Test", "");

            var msg = context.Messages.Where(m => m.UserName == "Test");

            Assert.Equal(0, msg.Count());

            context.Dispose();
        }

        [Fact]
        public async Task SaveMsgAsyncDoesNotSaveWhitespaceMessages()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IChatService service = new ChatService(context);

            await context.SaveChangesAsync();

            await service.SaveMsgAsync("Test", " ");

            var msg = context.Messages.Where(m => m.UserName == "Test");

            Assert.Equal(0, msg.Count());

            context.Dispose();
        }

        [Fact]
        public async Task GetAllMsgsAsyncReturnsAllMessages()
        {
            SportWaveDbContext context = new SportWaveDbContext(DbContextOptions.Options);
            IChatService service = new ChatService(context);

            context.Users.Add(new ApplicationUser
            {
                Id = Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9"),
                UserName = "test@gmail.com",
                NormalizedUserName = "TEST@GMAIL.COM",
                Email = "test@gmail.com",
                NormalizedEmail = "TEST@GMAIL.COM",
                EmailConfirmed = false,
                PasswordHash = "AQAAAAEAACcQAAAAEHyipAH79RA/Wg+CUvmeFxVsOIm2zlnApqzSeSEtHgKPARYIni9m+EyBMv5XjsGq5Q==",
                SecurityStamp = "34CRR52GZU54BMGOKQLXNSZ3TE5VI7ZT",
                ConcurrencyStamp = "88a2c0c0-8884-4045-a4e0-154b1d1cd30d",
                PhoneNumber = null,
                PhoneNumberConfirmed = false,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            });

            await context.SaveChangesAsync();

            await service.SaveMsgAsync("Test", "Test");
            var user = await context.Users.Where(u => u.Id == Guid.Parse("591C3FC7-2E0A-498A-9693-713DC1C10DD9")).FirstOrDefaultAsync();
            var userName = user.Email.Split("@").ToArray()[0];
            var msgs = await service.GetAllMsgsAsync(userName);

            Assert.NotEmpty(msgs.Messages);

            context.Dispose();
        }
    }
}
