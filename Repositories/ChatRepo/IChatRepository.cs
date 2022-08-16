namespace HRSystem.Repositories.ChatRepo
{
    public interface IChatRepository
    {
        List<UsersMessages> GetMessagesBetweenTwoConnection(string currentId, string otherId);
        void SaveMessage(string currentId, string otherId, string message);
    }
}
