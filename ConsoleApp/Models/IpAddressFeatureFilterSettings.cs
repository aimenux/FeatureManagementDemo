﻿using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace ConsoleApp.Models;

public class IpAddressFeatureFilterSettings
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