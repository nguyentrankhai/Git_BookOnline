using System.Windows.Controls;
using System.Windows.Documents;
using BUS_BookOnline;
using DTO_Book;
using System.Windows;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;

namespace Template
{
    /// <summary>
    /// Interaction logic for ExpiryDate_UC.xaml
    /// </summary>
    public partial class ExpiryDate_UC : UserControl
    {
        private PackageExpiry packageSelected;
        public ExpiryDate_UC()
        {
            InitializeComponent();
            fillData();
        }

        private void fillData()
        {
            BUS_PackageExpiry bus = new BUS_PackageExpiry();
            lstExpiryData.ItemsSource = bus.getAllPackageExpiry();
            txtWallet.Text = Session.User.Wallet.ToString("N0");
        }

        private void lstExpiryData_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            packageSelected = new PackageExpiry();
            packageSelected = item.Content as PackageExpiry;
            
            
            txtAmount.Text = packageSelected.Amount.ToString("N0");
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {          
            checkOut();
        }

        private void checkOut()
        {             
            if(packageSelected != null)
            {
                BUS_PackageExpiry bus = new BUS_PackageExpiry();
                if(bus.buyExpiry(Session.User.ID1, packageSelected.PackageID))
                {
                    MessageBox.Show("Bạn đã gia hạn thành công!");
                }
                else
                {
                    MessageBox.Show("Giao dịch thực hiện không thành công, vui lòng thử lại");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa lựa chọn gói cước.");
            }
        }
    }
}
