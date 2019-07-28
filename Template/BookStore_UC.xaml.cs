
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using BUS_BookOnline;
using DTO_BookOnline;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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

      
        private void ItemData_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollBar sb = e.OriginalSource as ScrollBar;

            if (sb.Orientation == Orientation.Horizontal)
                return;

            if (sb.Value == sb.Maximum)
            {
                MessageBox.Show("Bottom rồi nè");
            }
        }

        private void ItemData_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }
    }
}
