using ConsoleApp.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApp.Filters.Contexts.Identity
{
    public class InMemoryIdentityUserProvider : IIdentityUserProvider
    {
        private static readonly ICollection<IdentityUser> IdentityUsers = new IdentityUser[]
        {
            new IdentityUser
            {
                Id = "Tom",
                Groups = Enumerable.Empty<string>()
            },
            new IdentityUser
            {
                Id = "Jerry",
                Groups = Enumerable.Empty<string>()
            },
            new IdentityUser
            {
                Id = "Paul",
                Groups = new List<string>()
                {
                    "Techos"
                }
            },
            new IdentityUser
            {
                Id = "Julia",
                Groups = new List<string>()
                {
                    "Devops"
                }
            },
            new IdentityUser
            {
                Id = "Sophia",
                Groups = new List<string>()
                {
                    "BigData"
                }
            }
        };

        public object Randomise { get; private set; }

        public Task<IdentityUser> GetIdentityUserAsync(string id)
        {
            var identityUser = IdentityUsers.SingleOrDefault(x => x.Id == id);
            return Task.FromResult(identityUser);
        }

        public Task<IdentityUser> GetRandomIdentityUserAsync()
        {
            var identityUser = Randomize.RandomFrom(IdentityUsers);
            return Task.FromResult(identityUser);
        }
    }
}
