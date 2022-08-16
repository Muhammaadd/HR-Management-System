namespace HRSystem.Repositories.ChatRepo
{
    public class ChatRepository : IChatRepository
    {
        private readonly HRDbContext context;

        public ChatRepository(HRDbContext context)
        {
            this.context = context;
        }
        public void SaveMessage(string currentId, string otherId, string message)
        {
            UsersMessages messageInfo = new UsersMessages
            {
                CurrentUserId = currentId,
                otherUserId = otherId,
                Message = message
            };
            context.Messages.Add(messageInfo);
            context.SaveChanges();
        }
        public List<UsersMessages> GetMessagesBetweenTwoConnection(string currentId, string otherId)
        {
            return context.Messages.Where(n => (n.CurrentUserId == currentId && n.otherUserId == otherId) || (n.CurrentUserId == otherId && n.otherUserId == currentId)).ToList();
        }
        public void ClearChat(string currentId, string otherId)
        {
            context.Messages.RemoveRange(GetMessagesBetweenTwoConnection(currentId, otherId));
        }
    }
}
