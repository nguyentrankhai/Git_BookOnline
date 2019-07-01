using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline.Web_Service
{
    class Call_Service
    {
        public Call_Service()
        {

        }
        /// <summary>
        /// function call service to test
        /// </summary>
        /// <param name="input">parameter</param>
        /// <returns>response from service</returns>
        public String Get_Test(String input)
        {
            //string response = "";
            var client = new RestClient(Service_Defination.URL_SERVICE_DEV.Replace("#PARAM", input));
            var request = new RestRequest(Method.GET);
            request.AddHeader("Postman-Token", "7f415488-8eee-47b4-892f-47a7aef6d521");
            request.AddHeader("cache-control", "no-cache");
            IRestResponse response = client.Execute(request);
            return response.Content.ToString();
        }
    }
}
