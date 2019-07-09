using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using BUS_BookOnline;
using System.Windows.Controls.Primitives;
using DTO_BookOnline;
using DAL_BookOnline;
using MaterialDesignThemes.Wpf;

namespace Template
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {        
        public void SetContentControl(UserControl uc)
        {
            this.MainContent.Content = uc;
        }
        public MainWindow()
        {
            InitializeComponent();            
            getCatalog();
           
        }
        private void mainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if(GhiNhoTaiKhoan.KiemTraLuuTaiKhoanTonTai())
                {
                    Session.User = GhiNhoTaiKhoan.ReadUserLogin();
                    ThayDoiTrangThaiPopup();
                }
            }
            catch(Exception ex)
            {
                Console.Write(ex.Message);
            }
        }
        private void getCatalog()
        {            
            List<Catalog> lst = new List<Catalog>();
            BUS_Catalog bus = new BUS_Catalog();
            lst = bus.getAllCatalog();
            DemoItemsListBox.ItemsSource =lst;
            ///
            BookStore_UC.Number = 0;
            BookStore_UC.Catalogy = null;

            MainContent.Content = new BookStore_UC();            
        }
        private void DemoItemsListBox_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var dependencyObject = Mouse.Captured as DependencyObject;
            while (dependencyObject != null)
            {
                if (dependencyObject is ScrollBar) return;
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);
            }
            MenuToggleButton.IsChecked = false;            
        }

        private void DemoItemsListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {           
            var item = ItemsControl.ContainerFromElement(sender as ListBox, e.OriginalSource as DependencyObject) as ListBoxItem;
            if (item != null && e.LeftButton==MouseButtonState.Pressed)
            {
                // ListBox item clicked - do some cool things here
                Catalog catalog = item.Content as Catalog;                
                BookStore_UC.Catalogy = catalog;
                BookStore_UC.Number = 1;
                BookStore_UC uc = new BookStore_UC();
                MainContent.Content = uc;
                //MessageBox.Show("World " + text.Name);
                MenuToggleButton.IsChecked = false;
            }
        }

        private void PackIcon_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            getCatalog();
            MenuToggleButton.IsChecked = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            BUS_User b_user = new BUS_User();
            User user = new User();
            //user = b_user.getUser("trinhlam");
            //Session.User = user;
            Button btn = sender as Button;
            if (Session.User != null)
            {                
                object uc;
                switch (btn.Tag.ToString())
                {
                    case "READMORE":
                        uc = new ReadMore_UC();
                        MainContent.Content = uc;
                        break;
                    case "WAREHOUSE":
                        uc = new WareHouse_UC();
                        MainContent.Content = uc;
                        break;
                    case "INFO":
                        uc = new UserInfor_UC();
                        MainContent.Content = uc;
                        break;
                    case "LOGOUT":
                        Session.User = null;
                        PDFWindow.BOOK = null;
                        MainContent.Content = null;
                        GhiNhoTaiKhoan.ClearData();
                        MainContent.Content = new BookStore_UC();
                        break;
                    case "BUYEXPIRY":
                        //uc = new ExpiryDate_UC();
                        //MainContent.Content = uc;
                        popupBuyExpiry();
                        break;
                    case "TRANSACTION":
                        uc = new Transaction_UC();
                        MainContent.Content = uc;
                        break;
                    default:
                        break;
                }               
            }
            else if (Session.User == null)
            {
                object uc;
                switch (btn.Tag.ToString())
                {
                    case "LOGIN":
                        uc = new Login_UC();
                        MainContent.Content = uc;                        
                        break;                    
                    default:
                        break;
                }
            }           
        }

        private async void popupBuyExpiry()
        {
            //if (DateTime.Compare(Session.User.Remaining, DateTime.Today) < 0)
            {
                var sampleMessageDialog = new ExpiryDate_UC()
                {
                    //Message = { Text = "Tài khoản của bạn đã hết hạn đọc sách, vui lòng gia hạn để đọc tiếp." }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
        }

        private void MainContent_LayoutUpdated(object sender, EventArgs e)
        {
            ThayDoiTrangThaiPopup();
        }
        private void ThayDoiTrangThaiPopup()
        {
            if (Session.User != null)
            {
                btnLogin.Visibility = Visibility.Collapsed;
                btnInfo.Visibility = Visibility.Visible;
                btnWareHouse.Visibility = Visibility.Visible;
                btnInfo.DataContext = Session.User;
                btnExpiry.Visibility = Visibility.Visible;
                btnTransaction.Visibility = Visibility.Visible;

                btnLogOut.IsEnabled = true;
            }
            else if (Session.User == null)
            {
                btnLogin.Visibility = Visibility.Visible;
                btnInfo.Visibility = Visibility.Collapsed;
                btnWareHouse.Visibility = Visibility.Collapsed;
                btnExpiry.Visibility = Visibility.Collapsed;
                btnTransaction.Visibility = Visibility.Collapsed;
                btnLogOut.IsEnabled = false;
            }
        }

        private void Logo_MouseDown(object sender, MouseButtonEventArgs e)
        {
            object uc;
            uc = new BookStore_UC();
            MainContent.Content = uc;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            Search_UC.strSearch = txtSearch.Text;
            MainContent.Content = new Search_UC();
        }
    }
}
