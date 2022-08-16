namespace HRSystem.Models
{
    public class UsersMessages
    {
        public int Id { get; set; }
        public string CurrentUserId { get; set; }
        public string otherUserId { get; set; }
        public string Message { get; set; }
    }
}
