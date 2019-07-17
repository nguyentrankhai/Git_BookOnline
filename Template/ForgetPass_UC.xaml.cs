using DTO_BookOnline;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUS_BookOnline;
using System.Configuration;
using DAL_BookOnline;

namespace Template
{
    /// <summary>
    /// Interaction logic for ForgetPass_UC.xaml
    /// </summary>
    public partial class ForgetPass_UC : UserControl
    {
        public User user;
        public ForgetPass_UC()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            BUS_User bus = new BUS_User();
            if (String.IsNullOrEmpty(txtAccount.Text))
            {
                MessageBox.Show("Tên đăng nhập không được để trống");
                txtAccount.Focus();
                return;
            }
            else { user = bus.getUser(txtAccount.Text); }
            
            if(user==null)
            {
                MessageBox.Show("Sai tên đăng nhập");
                txtAccount.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtPwd.Password))
            {
                MessageBox.Show("Mật khẩu không được để trống");
                txtPwd.Focus();
                return;
            }
            if (txtConfirmPwd.Password != txtPwd.Password)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp");
                txtConfirmPwd.Focus();
                return;
            }
            if(txtEmail.Text != user.Email)
            {
                MessageBox.Show("Email bạn nhập không chính xác.");
                txtEmail.Focus();
                return;
            }

            ApiHelper.InitializeClient();
            string API = ConfigurationManager.AppSettings["API_RESETPWD"];
            string pwd = GhiNhoTaiKhoan.Base64Encode(txtPwd.Password.ToString());
            string url = API+ "userid=" + user.ID1 + "&email=" + user.Email+ "&password="+ pwd;
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    string us = await res.Content.ReadAsAsync<string>();
                    if (us == "Success")
                    {
                        string email = hideEmail(user.Email);
                        string noti = String.Format("Mật khẩu mới của bạn đã được gửi tới email: {0}\nVui lòng kiểm tra email để được cập nhật mật khẩu mới.", email);
                        MessageBox.Show("Vui lòng kiểm tra email để hoàn tất việc khôi phục mật khẩu!");
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }

                }
            }
        }
        private string hideEmail(string email)
        {
            string result = "";
            string[] temp = email.Split('@');
            if (temp.Length > 0 && temp.Length <2)
            {
                string hide = temp[0];
                string atTag = temp[1];
                int countHide = hide.Length;
                result = hide.Substring(0, 3);
                string asterisk = "";
                for (int i = 2; i< countHide; i++)
                {
                    asterisk += "*";
                }
                result = result + asterisk + atTag;
            }
            return result;
        }
    }
}
