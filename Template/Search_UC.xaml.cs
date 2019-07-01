using System.Windows;
using System.Windows.Controls;
using BUS_BookOnline;
using System.Collections.Generic;
using DTO_BookOnline;

namespace Template
{
    /// <summary>
    /// Interaction logic for Search_UC.xaml
    /// </summary>
    public partial class Search_UC : UserControl
    {
        public static string strSearch = "";
        public Search_UC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            BUS_Book bus = new BUS_Book();
            List<Book> lst = bus.getBook(strSearch);
            ItemData.ItemsSource = lst;
        }
    }
}
