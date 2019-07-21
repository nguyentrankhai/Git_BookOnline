using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace API_BookOnline
{
    class Common
    {
        public static string[] GetArrayByString(string str)
        {
            return str.Split('#');
        }
        public static bool IsInternet()
        {
            string host = ConfigurationManager.AppSettings["PING"];
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(host, 3000);
                if (pr.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception)
            {


            }
            return false;
        }
    }
}
