using DTO_BookOnline;
using Microsoft.Win32;
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

namespace Template
{
    /// <summary>
    /// Interaction logic for EditProfile_UC.xaml
    /// </summary>
    public partial class EditProfile_UC : UserControl
    {
        User user = new User();
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        string filePath = null;
        public EditProfile_UC()
        {
            InitializeComponent();
            getUser();
            user = Session.User;
            
        }

        private void getGen(string gen)
        {
            if (gen == "Nam")
            {
                radNam.IsChecked = true;
            }
            else
                radNu.IsChecked = true;
        }

        private void getUser()
        {
            BUS_BookOnline.BUS_User bus = new BUS_BookOnline.BUS_User();
            Session.User = bus.getUser(Session.User.ID1);
            UserInfo info = new UserInfo()
            {
                SigninDate = Session.User.SigninDate,
                Gen = Session.User.Gen,
                Image1 = Session.User.Image1,
                Username = Session.User.Username,
                ID1 = Session.User.ID1,
                Wallet = Session.User.Wallet,
                Remaining = DateTime.Compare(DateTime.Today, Session.User.Remaining) > 0 ? "Hết hạn đọc sách" : "Còn hạn đến hết ngày " + Session.User.Remaining.ToString("dd/MM/yyyy")
            };
            gridInfo.DataContext = info;
            txtID.Text = Session.User.ID1;
            getGen(Session.User.Gen);
            //btnFollow.Visibility = Visibility.Hidden;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            object uc = new UserInfor_UC();
            mainWindow.MainContent.Content = uc;
        }
        private void saveThongTin()
        {
            if (oldPassword.Password.ToString() != user.Password)
            {
                MessageBox.Show("Mật khẩu cũ không chính xác", "Thông báo");
                oldPassword.Focus();
                return;
            }
            if (newPwd.Password.ToString() != "")
            {
                if (newPwd.Password.ToString() != confirmnewPwd.Password.ToString())
                {
                    MessageBox.Show("Mật khẩu mới không chính xác", "Thông báo");
                    newPwd.Focus();
                    newPwd.Clear();
                    confirmnewPwd.Clear();
                    return;
                }
                else
                {
                    User us = new User();
                    us.ID1 = txtID.Text.ToString();
                    us.Username = txtName.Text;
                    us.Password = newPwd.Password.ToString();
                    if (radNam.IsChecked == true)
                    {
                        us.Gen = "Nam";
                    }
                    else us.Gen = "Nữ";
                    BUS_BookOnline.BUS_User bus = new BUS_BookOnline.BUS_User();
                    if (bus.updateUser(us, filePath))
                    {
                        MessageBox.Show("Thay đổi thông tin thành công", "Chúc mừng bạn");
                        Session.User = bus.getUser(us.ID1);
                        object uc = new UserInfor_UC();
                        mainWindow.MainContent.Content = uc;
                    }
                    else
                        MessageBox.Show("Thay đổi thông tin không thành công", "Thông báo");
                }
            }
            else
            {
                User us = new User();
                us.ID1 = txtID.Text.ToString();
                us.Username = txtName.Text;
                us.Password = oldPassword.Password.ToString();
                if (radNam.IsChecked == true)
                {
                    us.Gen = "Nam";
                }
                else us.Gen = "Nữ";
                BUS_BookOnline.BUS_User bus = new BUS_BookOnline.BUS_User();
                if (bus.updateUser(us, filePath))
                {
                    MessageBox.Show("Thay đổi thông tin thành công", "Chúc mừng bạn");
                    Session.User = bus.getUser(us.ID1);
                    object uc = new UserInfor_UC();
                    mainWindow.MainContent.Content = uc;
                }
                else
                    MessageBox.Show("Thay đổi thông tin không thành công", "Thông báo");
            }
        }
      
        private void imgAvatar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BrowseImage();
        }

        private void BrowseImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dlg.Title = "Select avatar";
            if (dlg.ShowDialog() == true)
            {
                filePath = dlg.FileName;
                ImageBrush myBrush = new ImageBrush();
                myBrush.ImageSource =
                    (BitmapImage)ConverterImage.convertImage(dlg.FileName);

                avatar.Fill = myBrush;
            }
        }

        private void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            saveThongTin();
        }
    }
}
