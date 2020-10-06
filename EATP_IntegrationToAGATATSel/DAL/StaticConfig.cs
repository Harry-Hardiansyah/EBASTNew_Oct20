using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1.DAL
{
    public static class StaticConfig
    {
        public static string ConnectionTI2G = "TI2G";
        public static string ConnectionTI3G = "TI3G";
        public static string ConnectionLTE = "LTE";

        public static string IntegrationStatus_Succeed = "succeed";
        public static string IntegrationStatus_Error = "error";
        public static string IntegrationStatus_Hold = "hold";
    }
}
