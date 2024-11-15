using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class ChatHub : Hub
{
    // Phương thức gửi tin nhắn từ client đến server
    public async Task SendMessage(string user, string message)
    {
        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(message))
        {
            // Nếu không có tin nhắn hoặc tên người dùng, trả về lỗi
            throw new ArgumentException("User and message cannot be empty.");
        }

        try
        {
            // Gửi tin nhắn cho tất cả các client đang kết nối
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
        catch (Exception ex)
        {
            // Xử lý lỗi khi gửi tin nhắn
            Console.WriteLine($"Error sending message: {ex.Message}");
            throw new InvalidOperationException("Failed to send message.");
        }
    }


    // Phương thức xử lý sự kiện khi người dùng kết nối
    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
        // Xử lý khi người dùng kết nối thành công
        await Clients.Caller.SendAsync("ReceiveMessage", "System", "Welcome to the chat!");
    }

    // Phương thức xử lý sự kiện khi người dùng ngắt kết nối
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
        // Xử lý khi người dùng ngắt kết nối
        await Clients.All.SendAsync("ReceiveMessage", "System", $"A user has left the chat.");
    }
}

