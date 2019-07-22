using DAL_BookOnline;
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
    public class API_BookStore
    {
        public void GetBookStore()
        {
            string isService = ConfigurationManager.AppSettings["IS_WEBSERVICE"];
            bool isInternet = Common.IsInternet();
            if (!isInternet) return;
            if (isService == "1")
            {
                callWebservice();
            }else if (isService == "0")
            {
                callWebIIS();
            }
        }

        private async void callWebIIS()
        {
            ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["API_ROOT"] + ConfigurationManager.AppSettings["API_BOOKSTORE"];
            string url = API;
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    dynamic tbl = JsonConvert.DeserializeObject(result);
                    List<tbl_Book> lst = new List<tbl_Book>();
                    string strMap = ConfigurationManager.AppSettings["MAP_BOOKSTORE"];
                    string[] arrMapping = Common.GetArrayByString(strMap);
                    tbl_Book book = new tbl_Book();
                    foreach (dynamic o in tbl)
                    {
                        book = new tbl_Book();
                        book.BookID = o[arrMapping[0]];
                        book.BookNAME = o[arrMapping[1]];
                        book.catalo = o[arrMapping[2]];
                        book.LangID = o[arrMapping[3]];
                        book.AUTHOR = o[arrMapping[4]];
                        book.YEAR = o[arrMapping[5]];
                        book.SUMMARY = o[arrMapping[6]];
                        book.PRICE = o[arrMapping[7]];
                        book.TRIAL_URL = o[arrMapping[8]];
                        book.IMG = o[arrMapping[9]];
                        book.NOTE = o[arrMapping[10]];
                        book.DateUpdate = o[arrMapping[11]];
                        lst.Add(book);
                    }
                    DAL_Book dal = new DAL_Book();
                    dal.insertBookStoreFromAPI(lst);
                }
            }
        }

        private async void callWebservice()
        {
          ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["APIWS_ROOT"] + ConfigurationManager.AppSettings["APIWS_BOOKSTORE"];
            string url = API;
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    string result = res.Content.ReadAsStringAsync().Result;
                    dynamic tbl = JsonConvert.DeserializeObject(result);
                    List<tbl_Book> lst = new List<tbl_Book>();
                    string strMap = ConfigurationManager.AppSettings["MAPWS_BOOKSTORE"];
                    string[] arrMapping = Common.GetArrayByString(strMap);
                    tbl_Book book = new tbl_Book();

                    var api_books = tbl["Book"];
                    foreach(dynamic o in api_books)
                    {
                        try
                        {
                            book = new tbl_Book();
                            book.BookID = o[arrMapping[0]];
                            book.BookNAME = o[arrMapping[1]];
                            book.catalo = o[arrMapping[2]][arrMapping[3]];
                            book.LangID = o[arrMapping[4]];
                            book.AUTHOR = o[arrMapping[5]][arrMapping[6]];
                            DateTime dt = o[arrMapping[7]];
                            book.YEAR = dt.Year;
                            book.SUMMARY = o[arrMapping[8]];
                            book.PRICE = o[arrMapping[9]];
                            book.TRIAL_URL = o[arrMapping[10]];
                            book.IMG = Common.DownloadImage((string)o[arrMapping[11]]);
                            book.NOTE = o[arrMapping[12]];
                            book.DateUpdate = o[arrMapping[13]];
                            lst.Add(book);
                        }
                        catch (Exception e)
                        {
                            string a = e.Message;
                        }
                    }
                    DAL_Book dal = new DAL_Book();
                    dal.insertBookStoreFromAPI(lst);
                }
            }
        }
    }
}
