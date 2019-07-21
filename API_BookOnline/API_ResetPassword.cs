using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace API_BookOnline
{
    public class API_ResetPassword
    {
        public async Task<bool> forgotPassword(User user, string newPassword)
        {
            ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["API_ROOT"] + ConfigurationManager.AppSettings["API_RESETPWD"];
            string url = API + "userid=" + user.ID1 + "&email=" + user.Email + "&password=" + newPassword;
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    string us = await res.Content.ReadAsAsync<string>();
                    if (us == "Success")
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
