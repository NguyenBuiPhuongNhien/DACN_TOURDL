using DO_AN.Helpers;
using DO_AN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace DO_AN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IHubContext<MessageHub> _messageHub;

        public MessageController(DoAnContext context, IHubContext<MessageHub> messageHub)
        {
            _messageHub = messageHub;
        }

        [HttpPost]
        [Route("SendMessage")]
        public async Task<IActionResult> SendMessage([FromBody] MessageRequest request)
        {
            //Thuật toán cần lưu tin nhắn lại và gửi đi bao gồm 
            // senderId là thông tin định dạng của người gửi
            // receiverId là thông tin định dạng của người nhận
            await _messageHub.Clients.All.SendAsync("ReceiveMessage", "senderId", "receiverId", request.Content);
            return Ok(new { message = "Message sent successfully to all clients!" });
        }
    }
}
