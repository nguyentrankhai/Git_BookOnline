using DAL_BookOnline;
using DTO_BookOnline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_BookOnline
{
    public class API_User
    {
        public async void Login(string iD, string pwd)
        {
            string host = ConfigurationManager.AppSettings["API_HOST"];
            int portNumb = int.Parse(ConfigurationManager.AppSettings["API_PORT"]);
            bool isInternet = Common.IsInternet(host, portNumb);
            if (!isInternet) return;

            ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["API_ROOT"] + ConfigurationManager.AppSettings["API_USER"];
            string url = API + "id=" + iD + "&pwd="+ GhiNhoTaiKhoan.Base64Encode(pwd);
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string[] map = Common.GetArrayByString(ConfigurationManager.AppSettings["MAP_USER"]);
                        string result = res.Content.ReadAsStringAsync().Result;
                        dynamic o = JsonConvert.DeserializeObject(result);
                        tbl_Account acc = new tbl_Account();
                        acc.AccountID = o[map[0]];
                        acc.NAME = o[map[1]];
                        acc.PWD = o[map[2]];
                        acc.GEN = o[map[3]];
                        acc.Wallet = o[map[4]];
                        //acc.IMG = o[map[5]];
                        acc.NOTE = o[map[6]];
                        acc.RemainingTime = o[map[7]];
                        acc.Status = o[map[8]];
                        acc.SignupDate = o[map[9]];
                        acc.Email = o[map[10]];
                        acc.EmailConfirmed = o[map[11]];
                        DAL_User user = new DAL_User();
                        if(user.getUser(acc.AccountID) == null)
                        {
                            user.insertUser(acc);
                        }
                        else
                        {
                            user.updateUser(acc);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string x = e.Message;
            }
        }
    }
}
