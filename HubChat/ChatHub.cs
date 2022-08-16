namespace HRSystem.HubChat
{
    public class ChatHub : Hub
    {

        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(System.Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
        public void SendMessage(string userId, String Message)
        {
            Clients.User(userId).SendAsync("RecieveMessage", Message);
        }
    }

}

