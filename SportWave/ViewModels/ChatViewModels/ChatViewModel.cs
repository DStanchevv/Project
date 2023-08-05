namespace SportWave.ViewModels.ChatViewModels
{
    public class ChatViewModel
    {
        public string User { get; set; } = null!;
        public List<MsgViewModel> Messages { get; set; } =  new List<MsgViewModel>();
    }
}
