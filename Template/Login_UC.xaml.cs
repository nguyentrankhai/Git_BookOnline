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

        private void Button_Click(object sender, RoutedEventArgs e)
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
                        uc = new BookStore_UC();
                        Session.User = user;
                        mainWindow.MainContent.Content = uc;
                    }
                    break;
            }

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
            if (txtUserName.Text == "")
            {
                MessageBox.Show("Tên đăng nhập không được để trống.");
            }
            if (txtPassword.Password.ToString() == "")
            {
                MessageBox.Show("Mật khẩu không được để trống.");
            }
            if (txtRePassword.Password.ToString() != txtPassword.Password.ToString())
            {
                MessageBox.Show("Mật khẩu không khớp.");
            }
            if(txtUserName.Text != "" && txtPassword.Password.ToString()!="")
            {
                if (b_user.insertUser(user) != false)
                {   
                    return true;
                    
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại.");
                }
            }
            return b;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            txtID.Focus();
            txtFullName.Focus();
        }
    }
}
