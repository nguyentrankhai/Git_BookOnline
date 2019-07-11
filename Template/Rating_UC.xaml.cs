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
    /// Interaction logic for Rating_UC.xaml
    /// </summary>
    public partial class Rating_UC : UserControl
    {
        BUS_RatingBook busR = new BUS_RatingBook();
        String bookID = "";
        public Rating_UC(String BookID)
        {
            InitializeComponent();
            bookID = BookID;
            LoadRating1();
            LoadRating2();
            LoadRating3();
            LoadRating4();
            LoadRating5();
        }

        private void LoadRating5()
        {
            LoadRating(5, ItemData5_0, ItemData5, text5);
        }

        private void LoadRating4()
        {
            LoadRating(4, ItemData4_0, ItemData4, text4);
        }

        private void LoadRating3()
        {
            LoadRating(3, ItemData3_0, ItemData3, text3);
        }

        private void LoadRating2()
        {
            LoadRating(2, ItemData2_0, ItemData2, text2);

        }

        private void LoadRating1()
        {

            LoadRating(1, ItemData1_0, ItemData1, text1);

        }

        private void LoadRating(int rate, ListView item_0, ListView item, Label label)
        {
            List<User> lst = busR.getUserRating(bookID, rate);
            List<User> sub = new List<User>();
            if (lst.Count > 0) {
                sub.Add(lst[lst.Count - 1]);
                item_0.ItemsSource = sub;
            }
            else { item_0.ItemsSource = lst; }
            if (lst.Count > 1)
            {
                lst.RemoveAt(lst.Count - 1);
                if (lst.Count > 5)
                {
                    List<User> lst1 = new List<User>();
                    int i = 1;
                    while (i < 4 && lst.Count > 0)
                    {
                        lst1.Add(lst[lst.Count - i]);
                        i++;
                    }
                    item.ItemsSource = lst1;

                    int count = lst.Count - lst1.Count;
                    if (count > 0)
                    {
                        label.Content = "+" + count;
                        label.Visibility = Visibility.Visible;
                    }
                }
                else
                {

                    item.ItemsSource = lst;
                }

            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Ellipse_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            Ellipse ellipse = sender as Ellipse;
            User us = ellipse.DataContext as User;
            if (us.ID1 == Session.User.ID1)
            {
                return;
            }
            object uc = new OtherInfo_UC(us);
            mainWindow.MainContent.Content = uc;
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        
    }
}