namespace NZWalks.API.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Authenticate(string username, string password);
    }
}
