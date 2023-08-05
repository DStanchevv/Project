using Microsoft.EntityFrameworkCore;
using SportWave.Data;
using SportWave.Data.Models;
using SportWave.Services.Contracts;
using SportWave.ViewModels.ChatViewModels;

namespace SportWave.Services
{
    public class ChatService : IChatService
    {
        private readonly SportWaveDbContext dbContext;

        public ChatService(SportWaveDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task SaveMsgAsync(string userName, string message)
        {
            Message msg = new Message()
            {
                UserName = userName,
                Msg = message,
                Time = DateTime.Now
            };

            if (!string.IsNullOrWhiteSpace(msg.Msg))
            {
                await dbContext.Messages.AddAsync(msg);
                await dbContext.SaveChangesAsync();
            }
        }

        public async Task<ChatViewModel> GetAllMsgsAsync(string user)
        {
            var messages = await this.dbContext.Messages.Select(m => new MsgViewModel
            {
                UserName = m.UserName,
                Msg = m.Msg,
                Time = m.Time
            }).ToListAsync();

            var model = new ChatViewModel()
            {
                Messages = messages,
                User = user
            };

            return model;
        }
    }
}
