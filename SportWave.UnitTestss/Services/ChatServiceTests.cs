using SportWave.Data;
using SportWave.Services.Contracts;
using SportWave.Services;

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

            await context.SaveChangesAsync();

            await service.SaveMsgAsync("Test", "Test");

            var msgs = await service.GetAllMsgsAsync();

            Assert.Single(msgs);

            context.Dispose();
        }
    }
}
