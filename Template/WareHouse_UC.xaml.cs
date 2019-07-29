using BUS_BookOnline;
using DTO_BookOnline;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
namespace Template
{
    /// <summary>
    /// Interaction logic for WareHouse_UC.xaml
    /// </summary>
    public partial class WareHouse_UC : UserControl
    {
        private static List<Book> lst;
        public WareHouse_UC()
        {
            InitializeComponent();
            getDataBookStore();
        }
        private void getDataBookStore()
        {
            txbNameCollect.Text = "Tủ sách của " + Session.User.Username;
            lst = new List<Book>();
            BUS_Book bus = new BUS_Book();
            lst = bus.getBookOfUser(Session.User);
            ItemData.ItemsSource = lst;
        }

        private async void Card_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                string id = (string)(sender as MaterialDesignThemes.Wpf.Card).Tag;
                BUS_Book bus = new BUS_Book();
                Book book = lst.Where(x => x.Id == id).SingleOrDefault();
                if (book != null)
                {
                    if (book.Status == "T")
                    {
                        if (DateTime.Compare(Session.User.Remaining, DateTime.Today) < 0)
                        {
                            var sampleMessageDialog = new SampleMessageDialog
                            {
                                Message = { Text = "Tài khoản của bạn đã hết hạn đọc sách, vui lòng gia hạn để đọc tiếp." }
                            };

                            await DialogHost.Show(sampleMessageDialog, "RootDialog");
                        }
                        //MessageBox.Show("Tài khoản của bạn đã hết hạn đọc sách, vui lòng gia hạn để đọc tiếp");
                        //else MessageBox.Show("Bạn còn thời hạn đọc");
                    }
                    string path = "Warehouse\\" + book.Id + ".pdf";
                    if (CheckData.IsFileExists(path))
                    {
                        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                        mainWindow.WindowState = WindowState.Minimized;
                        PDFWindow.BOOK = book;
                        PDFWindow window = new PDFWindow();
                        window.Show();
                    }
                    else
                    {
                        if (CheckData.IsInternet("google.com"))
                        {
                            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                            mainWindow.WindowState = WindowState.Minimized;
                            DownloadMessageWindow w = new DownloadMessageWindow();
                            w.book = book;
                            w.path = path;
                            w.Show();
                        }
                        else
                        {
                            var sampleMessageDialog = new SampleMessageDialog
                            {
                                Message = { Text = "Vui lòng kiểm tra lại kết nối internet." }
                            };

                            await DialogHost.Show(sampleMessageDialog, "RootDialog");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra từ hệ thống!", "Có lỗi xảy ra");
                }
            }
        }


        private async void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (bookid != "")
            {
                Book book = lst.Where(x => x.Id == bookid).SingleOrDefault();
                var sampleMessageDialog = new delBookofUser_UC(book);

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
            getDataBookStore();
            //bus.removeBookofUser(book.Id, Session.User.ID1);
        }

        string bookid = "";
        private void Grid_MouseRightButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            bookid = grid.Tag as string;
        }
    }
}
