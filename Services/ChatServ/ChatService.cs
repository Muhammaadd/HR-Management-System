namespace HRSystem.Services.ChatServ
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository chatRepository;

        public ChatService(IChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        public List<UsersMessages> GetMessagesBetweenTwoConnection(string currentId, string otherId)
        {
            return chatRepository.GetMessagesBetweenTwoConnection(currentId, otherId);
        }

        public void SaveMessage(string currentId, string otherId, string message)
        {
            chatRepository.SaveMessage(currentId, otherId, message);
        }
    }
}
