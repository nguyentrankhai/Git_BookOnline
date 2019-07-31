using DAL_BookOnline;
using DTO_BookOnline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;

namespace API_BookOnline
{
    public class API_BookOfUser
    {
        public async void GetBookOfUser(User user)
        {
            string host = ConfigurationManager.AppSettings["API_HOST"];
            int portNumb = int.Parse(ConfigurationManager.AppSettings["API_PORT"]);
            bool isInternet = Common.IsInternet(host, portNumb);
            if (!isInternet) return;

            ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["API_ROOT"] + ConfigurationManager.AppSettings["API_BOOKOFUSER"];
            string url = API + "userid=" + user.ID1;
            try
            {
                using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (res.IsSuccessStatusCode)
                    {
                        string result = res.Content.ReadAsStringAsync().Result;
                        dynamic tbl = JsonConvert.DeserializeObject(result);
                        List<tbl_BookofUser> lst = new List<tbl_BookofUser>();
                        string strMap = ConfigurationManager.AppSettings["MAP_BOOKOFUSER"];
                        string[] arrMapping = Common.GetArrayByString(strMap);
                        tbl_BookofUser book = new tbl_BookofUser();
                        foreach (dynamic o in tbl)
                        {
                            book = new tbl_BookofUser();
                            book.ID = o[arrMapping[0]];
                            book.USERID = o[arrMapping[1]];
                            book.BOOKID = o[arrMapping[2]];
                            book.PAGE = o[arrMapping[3]];
                            book.LastView = o[arrMapping[4]];
                            book.Status = o[arrMapping[5]];
                            book.NOTE = o[arrMapping[6]];
                            book._Date = o[arrMapping[7]];
                            lst.Add(book);
                        }
                        DAL_Book dal = new DAL_Book();
                        dal.insertBookOfUserFromAPI(lst, user.ID1);
                    }
                }
            }catch(Exception ex)
            {
                
            }
        }
    }
}
