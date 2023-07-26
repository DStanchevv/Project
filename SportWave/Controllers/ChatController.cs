using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportWave.Services.Contracts;

namespace SportWave.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class ChatController : Controller
    {
        private IChatService chatService;

        public ChatController(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public async Task<IActionResult> Chat()
        {
            var model = await chatService.GetAllMsgsAsync();
            return View(model);
        }
    }
}
