
using DTO_BookOnline;
using System.Configuration;
using System.Net.Http;

namespace API_BookOnline
{
    public class API_ResgisterAccount
    {
        public async System.Threading.Tasks.Task<bool> registerAccount(User user)
        {
            ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["API_ROOT"] + ConfigurationManager.AppSettings["API_REGISTER"];
            string url = API + "userid=" + user.ID1 + "&email=" + user.Email + "&isConfirm=true";
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
