using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace BulkyBook.Business.User
{
    public static class UserManager
    {
        public static string GetUserIp()
        {
            IPHostEntry iPHost = Dns.GetHostEntry(Dns.GetHostName());
            string ipAddress = Convert.ToString(iPHost.AddressList.
                FirstOrDefault(address => address.AddressFamily 
                == System.Net.Sockets.AddressFamily.InterNetwork));

            return ipAddress;
        }
    }
}
