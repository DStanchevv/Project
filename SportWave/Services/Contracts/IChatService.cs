using SportWave.ViewModels.ChatViewModels;

namespace SportWave.Services.Contracts
{
    public interface IChatService
    {
        Task SaveMsgAsync(string userName, string message);
        Task<ChatViewModel> GetAllMsgsAsync(string user);
    }
}
