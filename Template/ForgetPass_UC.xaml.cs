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


            ApiHelper.InitializeClient();
            string url = "http://localhost:49898/api/SendEmail/ResetPass?userid=" + user.ID1 + "&email=" + user.Email+ "&password="+txtPwd.Password.ToString();
            using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (res.IsSuccessStatusCode)
                {
                    string us = await res.Content.ReadAsAsync<string>();
                    if (us == "Success")
                    {
                        MessageBox.Show("Vui lòng kiểm tra email để hoàn tất việc khôi phục mật khẩu!");
                        DialogHost.CloseDialogCommand.Execute(null, null);
                    }

                }
            }
        }
    }
}
