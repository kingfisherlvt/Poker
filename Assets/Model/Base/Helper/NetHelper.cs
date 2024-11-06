﻿using System.Collections.Generic;
using System.Net;

namespace ETModel
{
	public static class NetHelper
	{
		public static string[] GetAddressIPs()
		{
			//获取本地的IP地址
			List<string> addressIPs = new List<string>();
			foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
			{
				if (address.AddressFamily.ToString() == "InterNetwork")
				{
					addressIPs.Add(address.ToString());
				}
			}
			return addressIPs.ToArray();
		}

		public static string[] GetAddressIPs(string hostNameOrAddress, bool useDns = true)
        {
            // Log.Debug($"address:{hostNameOrAddress}");
            if (!useDns)
                return new[] { hostNameOrAddress };

			List<string> addressIPs = new List<string>();
			foreach (IPAddress address in Dns.GetHostEntry(hostNameOrAddress).AddressList)
			{
				if (address.AddressFamily.ToString() == "InterNetwork")
				{
					addressIPs.Add(address.ToString());
				}
			}
			return addressIPs.ToArray();
		}
	}
}
