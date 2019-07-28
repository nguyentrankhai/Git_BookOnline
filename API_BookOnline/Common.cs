using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
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
        public static bool IsInternet(string hostService, int portNumber)
        {
            string host = ConfigurationManager.AppSettings["PING"];
            Ping p = new Ping();
            try
            {
                PingReply pr = p.Send(host, 3000);
                if (pr.Status == IPStatus.Success)
                {
                    return PingHost(hostService, portNumber);
                }
            }
            catch (Exception ex)
            {
                string e = ex.Message;

            }
            return false;
        }

        public static bool PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                    return true;
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        public static string DownloadImage(string url)
        {
            string[] arrUrl = url.Split('/');
            using (WebClient client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(url), String.Format(@".\Store\img\{0}", arrUrl.Last()));
            }
            return String.Format(@".\Store\img\{0}", arrUrl.Last());
        }
    }
}
