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
using MaterialDesignThemes.Wpf;

namespace Template
{
    /// <summary>
    /// Interaction logic for unFollow_UC.xaml
    /// </summary>
    public partial class unFollow_UC : UserControl
    {
        BUS_User bus = new BUS_User();
        User flwing = new User();
        public unFollow_UC(string followingID)
        {
            InitializeComponent();
            if (followingID != "")
            {
                flwing = bus.getUser(followingID);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void btnUnfollow_Click(object sender, RoutedEventArgs e)
        {
            BUS_Follow bus_flw = new BUS_Follow();
            bool check = bus_flw.updateFollow(Session.User, flwing);
            if (check != true)
            {
                txtContent.Text = "Lỗi! Không bỏ theo dõi được!";
                btnUnfollow.Visibility = Visibility.Hidden;
            }
            else
            {
                txtContent.Text = "Đã bỏ theo dõi!";
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }
    }
}
