using System;
using System.Net.NetworkInformation;
using System.IO;
namespace Template
{
    public class CheckData
    {
        public static bool IsFileExists(string path)
        {
            bool flag = false;
            if (File.Exists(path))
            {
                flag = true;
            }
            return flag;
        }
        public static bool IsInternet(string host)
        {
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
