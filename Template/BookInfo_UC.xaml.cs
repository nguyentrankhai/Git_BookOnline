using System.Windows;
using System.Windows.Controls;
using DTO_BookOnline;
using BUS_BookOnline;
using System;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;
using System.Windows.Input;
using System.Collections.Generic;
using System.Windows.Media;

namespace Template
{
    /// <summary>
    /// Interaction logic for BookInfo_UC.xaml
    /// </summary>
    public partial class BookInfo_UC : UserControl
    {
        bool valuechange = false; //mở UC là chỉ được đọc dữ liệu (phân biệt đọc và ghi dữ liệu) trên rating bar
        Book book = new Book();
        public BookInfo_UC(string idBook)
        {
            InitializeComponent();
            BUS_Book b_book = new BUS_Book();
            book = b_book.getBookWithID(idBook);
            grid.DataContext = book;
            loadComments(book.Id);
            //AddHandler(FrameworkElement.MouseDownEvent, new MouseButtonEventHandler(Button_MouseDown), true);

        }
        private void disable()
        {
            DataTemplate dt = ItemData.ItemTemplate;
            DependencyObject dio = dt.LoadContent();
           
        }
        private void loadComments(string bookId)
        {
            BUS_Comment busCmt = new BUS_Comment();

            List<Comment> cmt = busCmt.getCommentsWithBookID(bookId);
            ItemData.ItemsSource = cmt;

            //foreach (var rb in FindVisualChild<RatingBar>(this))
            //{
            //    if (rb.Name == "Rate")
            //    {
            //        RatingBar ratingbar = (RatingBar)rb;
            //       if(ratingbar.Value ==0)
            //        { ratingbar.Visibility = Visibility.Hidden; }
            //    }
            //}

        }

        private async void btnMua_Click(object sender, RoutedEventArgs e)
        {
            if (Session.User != null)
            {

                BUS_User bus = new BUS_User();
                BUS_Book bus_book = new BUS_Book();
                if (Session.User.Wallet > book.Price)
                {
                    book.Status = "M";
                    if (bus.buyBook(Session.User, book))
                    {
                        Session.User = bus.getUser(Session.User.ID1);
                        //MessageBox.Show("Đã thêm sách vào kho. \nSố tiền còn lại trong tài khoản: " + Session.User.Wallet);

                        var sampleMessageDialog = new SampleMessageDialog
                        {
                            Message = { Text = "Đã thêm sách vào kho. \nSố tiền còn lại trong tài khoản: " + Session.User.Wallet }
                        };

                        await DialogHost.Show(sampleMessageDialog, "RootDialog");
                    }
                    else
                    {
                        //MessageBox.Show("Phiên giao dịch bị lỗi");
                        var sampleMessageDialog = new SampleMessageDialog
                        {
                            Message = { Text = "Phiên giao dịch bị lỗi." }
                        };

                        await DialogHost.Show(sampleMessageDialog, "RootDialog");
                    }
                }
                else
                {
                    //MessageBox.Show("Phiên giao dịch bị lỗi");
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Số tiền trong tài khoản không đủ để thực hiện giao dịch" }
                    };

                    await DialogHost.Show(sampleMessageDialog, "RootDialog");
                }

            }
            else
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Chức năng này cần được đăng nhập mới có thể sử dụng." }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
                //MessageBox.Show("Chức năng này cần được đăng nhập mới có thể sử dụng.", "Thông báo đăng nhập");
            }
        }

        private async void btnDocThu_Click(object sender, RoutedEventArgs e)
        {
            if (Session.User != null)
            {
                //var sampleMessageDialog = new SampleMessageDialog
                //{
                //    Message = { Text = "Chức năng này đang phát triển." }
                //};

                //await DialogHost.Show(sampleMessageDialog, "RootDialog");
                string path = "trial\\" + book.Id + ".pdf";
                if (CheckData.IsFileExists(path))
                {
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.WindowState = WindowState.Minimized;
                    PDF_TrialWindow.BOOK = book;
                    PDF_TrialWindow window = new PDF_TrialWindow();
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
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Chức năng này cần được đăng nhập mới có thể sử dụng." }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
        }
        private async void ThueSach()
        {
            DateTime today = DateTime.Now;
            if (Session.User != null)
            {
                if (DateTime.Compare(Session.User.Remaining, today) < 0)
                {
                    //MessageBox.Show("Tài khoản của bạn đã hết hạn.\nHãy gia hạn thêm.");
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Tài khoản của bạn đã hết hạn.\nHãy gia hạn thêm." }
                    };

                    await DialogHost.Show(sampleMessageDialog, "RootDialog");
                }
                else
                {
                    BUS_Book b_book = new BUS_Book();
                    book.Status = "T";
                    b_book.insertBookOfUser(Session.User, book);
                    var sampleMessageDialog = new SampleMessageDialog
                    {
                        Message = { Text = "Sách đã được thêm vào tủ sách." }
                    };

                    await DialogHost.Show(sampleMessageDialog, "RootDialog");
                }

            }
            else
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Chức năng này cần được đăng nhập mới có thể sử dụng." }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
        }

        private void btnThue_Click(object sender, RoutedEventArgs e)
        {
            ThueSach();

        }

        /// <summary>
        /// if value_change = false  <=> Read data;
        /// if value_change = true <=> Write data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void BasicRatingBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<int> e)
        {
            //if (valuechange == true) //nếu Rating bar cho ghi dữ liệu
            //{
            //    string mess = "";
            //    if (Session.User == null)
            //    {
            //        mess = "Chức năng này cần được đăng nhập mới có thể sử dụng.";
            //    }

            //    else
            //    {
            //        bool chk = false;
            //        Rating rating = new Rating(Session.User.ID1, book.Id, BasicRatingBar.Value);
            //        BUS_RatingBook bus_rating = new BUS_RatingBook();
            //        Rating r = bus_rating.findRating(rating);
            //        if (r != null)
            //        {
            //            chk = bus_rating.updateRating(rating);
            //        }
            //        else
            //        {
            //            chk = bus_rating.insertRating(rating);
            //        }

            //        if (chk == false) //nếu không thể update hoặc không thể insert
            //        {
            //            mess = "Đánh giá không thành công.";

            //        }
            //        else //nếu update hoặc insert thành công
            //        {
            //            mess = "Đánh giá thành công.";

            //            Book bk = new Book();
            //            BUS_Book bus_Book = new BUS_Book();
            //            bk = bus_Book.getBookWithID(book.Id);
            //            book.Rating = bk.Rating; //gán lại điểm rating mới
            //            book.RatingCount = bk.RatingCount; //gán lại lượt đánh giá mới
            //            valuechange = false; //lúc này chỉ cho đọc không cho ghi vì khi tránh gây nhầm lẫn giữa việc đọc và ghi khi giá trị của rating bar thay đổi
            //            BasicRatingBar.Value = (int)book.Rating; //cập nhật dữ liệu lên rating bar
            //            BasicRatingBar.ToolTip = book.Rating + "";

            //        }
            //        if (mess != "")
            //        {
            //            var sampleMessageDialog = new SampleMessageDialog
            //            {
            //                Message = { Text = mess }
            //            };
            //            await DialogHost.Show(sampleMessageDialog, "RootDialog");
            //        }
            //    }

            //}
            //else //khi rating bar đã đọc xong dữ liệu
            //{
            //    valuechange = true; //bật chế độ ghi
            //}
        }
        private async void BasicRatingBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            string mess = "";
            bool chk = false;
            Rating rating = new Rating(Session.User.ID1, book.Id, BasicRatingBar.Value);
            BUS_RatingBook bus_rating = new BUS_RatingBook();
            Rating r = bus_rating.findRating(rating);
            if (r != null)
            {
                chk = bus_rating.updateRating(rating);
            }
            else
            {
                chk = bus_rating.insertRating(rating);
            }

            if (chk == false) //nếu không thể update hoặc không thể insert
            {
                mess = "Đánh giá không thành công.";

            }
            else //nếu update hoặc insert thành công
            {
                mess = "Đánh giá thành công.";

                Book bk = new Book();
                BUS_Book bus_Book = new BUS_Book();
                bk = bus_Book.getBookWithID(book.Id);
                book.Rating = bk.Rating; //gán lại điểm rating mới
                book.RatingCount = bk.RatingCount; //gán lại lượt đánh giá mới
                valuechange = false; //lúc này chỉ cho đọc không cho ghi vì khi tránh gây nhầm lẫn giữa việc đọc và ghi khi giá trị của rating bar thay đổi
                BasicRatingBar.Value = (int)book.Rating; //cập nhật dữ liệu lên rating bar
                BasicRatingBar.ToolTip = book.Rating + "";

            }
            if (mess != "")
            {
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = mess }
                };
                await DialogHost.Show(sampleMessageDialog, "RootDialog");
            }
        }
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Session.User.ID1 != null)
                {

                    if (txtComment.Text != "")
                    {
                        Comment comment = new Comment();
                        comment.BookID = book.Id;
                        comment.UserID= Session.User.ID1;
                        comment.Content = txtComment.Text;
                        comment.Date = DateTime.Now;
                        BUS_Comment bus = new BUS_Comment();
                        if (bus.insertComment(comment) == false)
                        {
                            MessageBox.Show("Error!");
                        }
                        else
                        {
                            loadComments(book.Id);
                            txtComment.Text = "";
                        }
                    }
                }
            }
            catch (Exception ex)
            { MessageBox.Show("Đăng nhập để comment!"); }
        }

        private void Comment_Executed(object sender, ExecutedRoutedEventArgs e)
        {

        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            disable();

        }

        private void BasicRatingBar_MouseEnter(object sender, MouseEventArgs e)
        {
          
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var sampleMessageDialog = new Rating_UC(book.Id);
            await DialogHost.Show(sampleMessageDialog, "RootDialog");
        }
    }
}
