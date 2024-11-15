using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using DO_AN.Models;  // Lưu ý thay đổi theo namespace của bạn
using System.Threading.Tasks;

namespace DO_AN.Controllers
{
    // API controller cho Chat
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHubContext;

        // Khởi tạo với IHubContext để có thể gửi message đến SignalR Hub
        public ChatController(IHubContext<ChatHub> chatHubContext)
        {
            _chatHubContext = chatHubContext;
        }

        // API để gửi tin nhắn
        [HttpPost("sendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
        {
            if (message == null || string.IsNullOrEmpty(message.User) || string.IsNullOrEmpty(message.Content))
            {
                return BadRequest("Invalid message data.");
            }

            // Gửi tin nhắn tới tất cả người dùng qua SignalR Hub
            await _chatHubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Content);

            return Ok(new { status = "Message sent successfully!" });
        }
    }

    // Lớp mô tả tin nhắn (model)
    public class ChatMessage
    {
        public string User { get; set; }
        public string Content { get; set; }
    }
}

