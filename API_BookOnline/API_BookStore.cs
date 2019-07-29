using DAL_BookOnline;
using Newtonsoft.Json;
using System;
using System.Collections;
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
        private static int i = 0;
        public void GetBookStore()
        {
            string host = ConfigurationManager.AppSettings["API_HOST"];
            int portNumb = int.Parse(ConfigurationManager.AppSettings["API_PORT"]);
            string hostWs = ConfigurationManager.AppSettings["APIWS_HOST"];
            int portNumbWs = int.Parse(ConfigurationManager.AppSettings["APIWS_PORT"]);
            bool isInternet = false;

            string isService = ConfigurationManager.AppSettings["IS_WEBSERVICE"];
            
            if (isService == "1")
            {
                isInternet = Common.IsInternet(host, portNumb);
                if (!isInternet) return;
                callWebservice();
            }else if (isService == "0")
            {
                isInternet = Common.IsInternet(host, portNumb);
                if (!isInternet) return;
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
            string url = String.Format(API,2.ToString());
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                var totalItem = res.Headers.GetValues("STUBO-TotalItems");
                var totalPage = res.Headers.GetValues("STUBO-TotalPages");
                int page = Int32.Parse(totalPage.First());

                if (i == page || res.RequestMessage.Content == null)
                {
                    i = 0;
                    return;
                }

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
            i++;
        }
    }
}
