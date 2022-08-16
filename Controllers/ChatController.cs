using Microsoft.AspNetCore.Mvc;

namespace HRSystem.Controllers
{
    public class ChatController : Controller
    {
        private readonly IAccountService accountService;
        private readonly IChatService chatService;
        private readonly UserManager<Hr> userManager;

        public ChatController(IAccountService accountService,
            IChatService chatService,
            UserManager<Hr> userManager)
        {
            this.accountService = accountService;
            this.chatService = chatService;
            this.userManager = userManager;
        }
        [HttpGet]
        [Authorize(Permissions.chat.View)]

        public async Task<IActionResult> Index()
        {
            if (User.Identity.Name == null)
                return RedirectToAction("Login", "Account");
            Hr user = await userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.Id = user.Id;
            return View(accountService.GetAllUsersNames().Where(n => n.UserName != User.Identity.Name).ToList());
        }
        [HttpGet]
        public IActionResult GetMessages(string currentId, string otherId)
        {
            return Json(chatService.GetMessagesBetweenTwoConnection(currentId, otherId));
        }
        [HttpPost]
        public IActionResult SaveMessagesInDatabase(string senderId, string receiveId, string message)
        {
            chatService.SaveMessage(senderId, receiveId, message);
            return RedirectToAction("index");
        }

    }
}
