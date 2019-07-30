using DTO_BookOnline;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
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
    /// Interaction logic for UserInfor_UC.xaml
    /// </summary>
    public partial class UserInfor_UC : UserControl
    {
        User user = new User();
        public UserInfor_UC()
        {
            InitializeComponent();
            user = Session.User;
            getUser();
            getLstFollowing();
            getLstFollower();
        }
       
        private void getLstFollowing()
        {
            BUS_BookOnline.BUS_Follow bus = new BUS_BookOnline.BUS_Follow();
            lstFollowing.ItemsSource = bus.getListFollowing(user);
        }
        private void getLstFollower()
         {
            BUS_BookOnline.BUS_Follow bus = new BUS_BookOnline.BUS_Follow();
            lstFollower.ItemsSource = bus.getListFollower(user);
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
            //btnFollow.Visibility = Visibility.Hidden;
        }
        private Follow isFollow()
        {
            BUS_BookOnline.BUS_Follow bus = new BUS_BookOnline.BUS_Follow();
            return bus.getFlw(Session.User, user);
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Ellipse ellipse = sender as Ellipse;
            User us = ellipse.DataContext as User;
            if(us.ID1== Session.User.ID1)
            {
                return;
            }
            object uc = new OtherInfo_UC(us);
            mainWindow.MainContent.Content = uc;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        string followingID = "";
        private void Ellipse_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
             followingID = (sender as Ellipse).Tag + "";
        }

        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (followingID != "" && followingID!=Session.User.ID1)
            {
                var sampleMessageDialog = new unFollow_UC(followingID)
                {
                    //Message = { Text = "Tài khoản của bạn đã hết hạn đọc sách, vui lòng gia hạn để đọc tiếp." }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
            getLstFollowing();
        }

        private async void btnNapVi_Click(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new Payment_UC();

            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            object uc = new EditProfile_UC();
            mainWindow.MainContent.Content = uc;
        }
    }
    //, StringFormat='{}{0:dd/MM/yyyy}'
    public class UserInfo
    {
        public string Username { get; set; }
        public string ID1 { get; set; }
        public double Wallet { get; set; }
        public string Remaining { get; set; }
        public ImageSource Image1 { get; set; }
        public string Gen { get; set; }
        public string SigninDate { get; set; }
    }
}
