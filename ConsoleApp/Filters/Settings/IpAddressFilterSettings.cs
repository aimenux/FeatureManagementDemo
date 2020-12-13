using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp.Filters.Settings
{
    public class IpAddressFilterSettings
    {
        public ICollection<string> Values { get; set; }

        public static string GetCurrentIpAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());

            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }

            return null;
        }
    }
}
