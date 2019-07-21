using System;
using System.Collections.Generic;
using System.Linq;
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
using DTO_BookOnline;
using DAL_BookOnline;
using System.Net.Mail;
using System.Net.Http;
using MaterialDesignThemes.Wpf;
using System.Configuration;

namespace Template
{
    /// <summary>
    /// Interaction logic for Login_UC.xaml
    /// </summary>
    public partial class Login_UC : UserControl
    {
        public Login_UC()
        {
            InitializeComponent();            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            BUS_User b_user = new BUS_User();
            User user = new User();
            Button btn = sender as Button;
            object uc;
            switch (btn.Tag.ToString())
            {
                case "LOGIN":
                    Boolean b = CheckLogin(b_user, user);
                    if (b)
                    {
                        
                        uc = new BookStore_UC();
                        mainWindow.MainContent.Content = uc;
                        if (chkLuuTK.IsChecked == true)
                        {
                            SaveDataLogin(Session.User);
                        }
                    }                    
                    break;
                case "SIGNUP":
                    Boolean check = SignUp(b_user, user);
                    if (check)
                    {
                        //uc = new BookStore_UC();

                        //Session.User = user;

                        //mainWindow.MainContent.Content = uc;

                        ApiHelper.InitializeClient();
                        string API = ConfigurationManager.AppSettings["API_REGISTER"];
                        string url = API+ "userid=" + user.ID1 + "&email=" + user.Email + "&isConfirm=true";
                        using (HttpResponseMessage res = await ApiHelper.ApiClient.GetAsync(url))
                        {
                            if (res.IsSuccessStatusCode)
                            {
                                string us = await res.Content.ReadAsAsync<string>();
                                if(us == "Success")
                                {
                                    MessageBox.Show("Vui lòng kiểm tra email để hoàn tất việc đăng ký!");
                                }
                                clearSignup();
                                tabLogin.Focus();
                            }
                        }
                    }
                    break;
            }

        }

        

        private void clearSignup()
        {
            txtEmail.Text = "";
            txtUserName.Text = "";
            txtPassword.Password = "";
            txtRePassword.Password = "";
            txtFullName.Text = "";
        }
        private void SaveDataLogin(User user)
        {
            GhiNhoTaiKhoan.WriteUserLogin(user);
        }

        //Check Login
        private Boolean CheckLogin(BUS_User b_user, User user)
        {
            Boolean b = false;
            if (txtID.Text == "" && txtPwd.Password.ToString() == "")
            {
                MessageBox.Show("Bạn cần điền tên đăng nhập và mật khẩu");
            }
            else
            {
                user.ID1 = txtID.Text;
                user.Password = txtPwd.Password.ToString();
                User us = b_user.getUser(txtID.Text);
                if (us != null)
                {
                    if (user.Password == us.Password)
                    {
                        //MessageBox.Show("Đăng nhập thành công");
                        user = us;
                        Session.User = us;
                        return true;
                    }
                    else if (user.Password != us.Password)
                    {
                        MessageBox.Show("Sai Mật Khẩu");
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập không tồn tại");
                }
            }
            return b;
        }
        private Boolean SignUp(BUS_User b_user, User user)
        {
            Boolean b = false;
            user.ID1 = txtUserName.Text;
            user.Password = txtPassword.Password.ToString();
            user.Username = txtFullName.Text;
            user.Gen = rdbMen.IsChecked == true ? "1" : "0";
            user.Remaining = DateTime.Now;
            user.Wallet = 0;
            if(checkValidEmail() == true)
            {
                user.Email = txtEmail.Text;
            }
            b = checkInfoSignUp(b_user, user);

            return b;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Focus();
            txtFullName.Focus();
        }

        private bool checkValidEmail()
        {
            try
            {
                MailAddress mail = new MailAddress(txtEmail.Text);
                return true;
            }
            catch(Exception ex)
            { 
            return false;
            }
        }

        private bool checkInfoSignUp(BUS_User b_user, User user)
        {
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống.");
                return false;
            }
            else
            {
                if (b_user.getUser(txtUserName.Text) != null)
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại.");
                    return false;
                }
            }
            if(String.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Email không được để trống");
                return false;
            }
            if (txtPassword.Password.ToString() == "")
            {
                MessageBox.Show("Mật khẩu không được để trống.");
                return false;
            }
            if (txtRePassword.Password.ToString() != txtPassword.Password.ToString())
            {
                MessageBox.Show("Mật khẩu không khớp.");
                return false;
            }
            if (checkValidEmail() == false)
            {
                MessageBox.Show("Email không hợp lệ");
                return false;
            }
            if (txtUserName.Text != "" && txtPassword.Password.ToString() != "" && txtEmail.Text != "")
            {
                if (b_user.insertUser(user) != false)
                {
                    return true;

                }
                else
                {
                    MessageBox.Show("Quá trình đăng ký bị lỗi!");
                    return false;
                }
            }
            return false;
        }

        private void txtPhone_LostFocus(object sender, RoutedEventArgs e)
        {
        }

        private async void popupForgetPass()
        {
            //if (DateTime.Compare(Session.User.Remaining, DateTime.Today) < 0)
            {
                var sampleMessageDialog = new ForgetPass_UC()
                {
                    //Message = { Text = "Tài khoản của bạn đã hết hạn đọc sách, vui lòng gia hạn để đọc tiếp." }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
        }
        private void btnQuenMK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupForgetPass();
        }
    }
}
