using System.Linq;
using System.Net;

namespace NetworkLayer.Logic
{
    public static class HelperFunctions
    {
        public static bool IsPortValid(uint port)
        {
            return port >= 1025 && port <= 65534;
        }

        public static bool IsIpAddressValid(string ipAddress)
        {
            if (string.IsNullOrEmpty(ipAddress) || ipAddress.Count(x => x == '.') != 3)
            {
                return false;
            }
            return IPAddress.TryParse(ipAddress, out _);
        }
    }
}
