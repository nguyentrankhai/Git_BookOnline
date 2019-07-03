using DTO_BookOnline;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for OtherInfo_UC.xaml
    /// </summary>
    public partial class OtherInfo_UC : UserControl
    {

        User user = new User();
        public OtherInfo_UC(User us)
        {
            InitializeComponent();
            user = us;
            getUser(us);
            getLstFollowing();
            getLstFollower();
            setupControl();
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
        private void getUser(User us)
        {
            BUS_BookOnline.BUS_User bus = new BUS_BookOnline.BUS_User();
            UserInfo info = new UserInfo()
            {
                Image1 = us.Image1,
                Username = us.Username,
                ID1 = us.ID1,
                Wallet = us.Wallet,
                Remaining = DateTime.Compare(DateTime.Today, us.Remaining) > 0 ? "Hết hạn đọc sách" : "Còn hạn đến hết ngày " + us.Remaining.ToString("dd/MM/yyyy")
            };
            gridInfo.DataContext = info;
            txtID.Text = "@" + us.ID1;
            //tabInfo.Visibility = Visibility.Hidden;
        }
        private void setupControl()
        {
            if (isFollow() != null)
            {
                if (isFollow().Status == 1)
                {
                    txtFollow.Text = "Unfollow";
                }
                else txtFollow.Text = "Follow";
            }
            else txtFollow.Text = "Follow";
        }
        private Follow isFollow()
        {
            BUS_BookOnline.BUS_Follow bus = new BUS_BookOnline.BUS_Follow();
            return bus.getFlw(Session.User, user);
        }
        private async void btnFollow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bool check = false;
            if (isFollow() != null)
            {
                BUS_BookOnline.BUS_Follow bus = new BUS_BookOnline.BUS_Follow();
                check = bus.updateFollow(Session.User, user);
            }
            else
            {
                BUS_BookOnline.BUS_Follow bus = new BUS_BookOnline.BUS_Follow();
                check = bus.insertFollow(Session.User, user);
            }
            if (check)
            {
                string str = "";
                if (isFollow().Status == 1)
                {
                    str = "Bạn đã theo dõi @" + user.ID1 + " thành công";
                }
                else
                {
                    str = "Bạn đã bỏ theo dõi @" + user.ID1;
                }
                var sampleMessageDialog = new SampleMessageDialog
                {

                    Message = { Text = str }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
            else
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Error" }
                };
                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
            setupControl();
        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Ellipse ellipse = sender as Ellipse;
            User us = ellipse.DataContext as User;
            object uc = new OtherInfo_UC(us);
            mainWindow.MainContent.Content = uc;
            DialogHost.CloseDialogCommand.Execute(null, null);


        }
      
    }
    //, StringFormat='{}{0:dd/MM/yyyy}'
}
