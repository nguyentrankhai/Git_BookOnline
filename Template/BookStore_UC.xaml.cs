
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BUS_BookOnline;
using DTO_BookOnline;
using System.Windows;

namespace Template
{
    /// <summary>
    /// Interaction logic for BookStore_UC.xaml
    /// </summary>
    public partial class BookStore_UC : UserControl
    {
        public static Catalog Catalogy;
        public static int Number = 0;
        MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public BookStore_UC()
        {
            InitializeComponent();
            getDataBookStore();
        }

        private void getDataBookStore()
        {          
            BUS_Book bus = new BUS_Book();
            if (Number == 0)
            {
                txbNameCollect.Text = "Trang chủ";
                ItemData.ItemsSource = bus.getAllBook();               
            }
            else
            {
                txbNameCollect.Text =Catalogy.Name;
                ItemData.ItemsSource = bus.getBookwithCatalo(Catalogy.Name);
            }
        }

        private void Card_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string idBook;
            MaterialDesignThemes.Wpf.Card btn = sender as MaterialDesignThemes.Wpf.Card;
            idBook = btn.Tag as string;
            object uc = new BookInfo_UC(idBook);
            mainWindow.MainContent.Content = uc;
        }
        
    }
}
