using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Session
    {
        public static IPAddress IpAddress { get; set; }
        public static string UserID { get; set; }
        public static string LoginId { get; set; }
        public static string UserTP { get; set; }
        public static string UserStoreID { get; set; }
        public static string UserNM { get; set; }
    }
}
