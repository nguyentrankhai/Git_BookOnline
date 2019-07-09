using BUS_BookOnline;
using System;
using System.Configuration;
using System.Threading;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Template
{
    /// <summary>
    /// Interaction logic for WebView_UC.xaml
    /// </summary>
    public partial class WebView_UC : UserControl
    {
        public static string BranchCode;
        public static double Amount;
        private string _paymentUrl;
        public WebView_UC()
        {
            InitializeComponent();            
        }
        private void initWebView()
        {
            string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"]; //URL nhan ket qua tra ve 
            string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"]; //URL thanh toan cua VNPAY 
            string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"]; //Ma website
            string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                                                                                        //Get payment input
            OrderInfo order = new OrderInfo();
            //Save order to db
            order.OrderId = DateTime.Now.Ticks;
            order.Amount = (decimal)Amount;
            order.OrderDescription = Session.User.Username + "THANH TOAN MUA VI TAI KHOAN";
            order.CreatedDate = DateTime.Now;

            //Build URL for VNPAY
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", "2.0.0");
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);

            string locale = "vn";
            if (!string.IsNullOrEmpty(locale))
            {
                vnpay.AddRequestData("vnp_Locale", locale);
            }
            else
            {
                vnpay.AddRequestData("vnp_Locale", "vn");
            }

            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_TxnRef", order.OrderId.ToString());
            vnpay.AddRequestData("vnp_OrderInfo", order.OrderDescription);
            vnpay.AddRequestData("vnp_OrderType", "order"); //default value: other
            vnpay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
            vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_BankCode", BranchCode);
            this._paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            //webView.Navigate(@"https://www.facebook.com/");
        }
        private void webView_Navigated(object sender, NavigationEventArgs e)
        {
            String aa = e.Uri.Query;
            if (aa.IndexOf("vnp_ResponseCode") > 0)
            {
                string output = HttpUtility.ParseQueryString(aa).Get("vnp_ResponseCode");

                if (output == "00")
                {                    
                    BUS_Payment payment = new BUS_Payment();
                    bool b =payment.isRechargeWallet(Session.User.ID1, Amount);
                    Thread.Sleep(2500);                   
                }
                UserInfor_UC user = new UserInfor_UC();
                Session.GetMainWindow().SetContentControl(user);
            }
        }

        private void webView_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if(!String.IsNullOrEmpty(this._paymentUrl))
            {
                webView.Navigate(this._paymentUrl);
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            initWebView();
        }
    }
}
