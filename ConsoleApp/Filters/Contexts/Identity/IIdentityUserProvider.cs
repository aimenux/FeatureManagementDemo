using System.Threading.Tasks;

namespace ConsoleApp.Filters.Contexts.Identity
{
    public interface IIdentityUserProvider
    {
        Task<IdentityUser> GetIdentityUserAsync(string id);
        Task<IdentityUser> GetRandomIdentityUserAsync();
    }
}
