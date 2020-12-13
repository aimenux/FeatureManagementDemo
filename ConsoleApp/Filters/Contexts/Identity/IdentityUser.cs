using System.Collections.Generic;

namespace ConsoleApp.Filters.Contexts.Identity
{
    public class IdentityUser
    {
        public string Id { get; set; }

        public IEnumerable<string> Groups { get; set; }

        public override string ToString()
        {
            return $"Id = {Id} Groups = {string.Join(";", Groups)}";
        }
    }
}
