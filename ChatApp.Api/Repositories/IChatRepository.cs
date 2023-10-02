namespace ChatApp.Api.Repositories
{
    public interface IChatRepository
    {
        Task<string> GetChatLink(string user1Id, string user2Id);
    }
}
