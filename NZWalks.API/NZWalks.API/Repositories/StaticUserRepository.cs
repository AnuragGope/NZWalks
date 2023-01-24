 using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class StaticUserRepository : IUserRepository
    {

        private List<User> Users = new List<User>()
        {
            new User()
            {
                FirstName = "Read Only", LastName = "User", Email = "readonly@user.com", Id = Guid.NewGuid(),
                UserName = "readonly@user.com", Password = "readonly@user.com", Roles = new List<string> {"reader"}
            },
            new User()
            {
                FirstName = "Read write", LastName = "User", Email = "readwrite@user.com", Id = Guid.NewGuid(),
                UserName = "readwrite@user.com", Password = "readwrite@user.com", Roles = new List<string> {"reader","write"}
            }
        };
        public async Task<bool> Authenticate(string username, string password)
        {
            var user = Users.Find(x => x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
            x.Password == password);

            if (user != null)
            {
                return true;
            }

            return false;
        }
    }
}
