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
using DTO_BookOnline;
using BUS_BookOnline;
using MaterialDesignThemes.Wpf;

namespace Template
{
    /// <summary>
    /// Interaction logic for delBookofUser_UC.xaml
    /// </summary>
    public partial class delBookofUser_UC : UserControl
    {
        Book book = new Book();
        public delBookofUser_UC(Book book)
        {
            InitializeComponent();
            this.book = book;
        }

        private void btnXoaSach_Click(object sender, RoutedEventArgs e)
        {
            BUS_Book bus = new BUS_Book();
            bool check = bus.removeBookofUser(book.Id, Session.User.ID1, book.Status);
            if(check!=true)
            {
                txtContent.Text = "Lỗi! Không xóa được quyển sách này!";
                btnXoaSach.Visibility = Visibility.Hidden;
            }
            else
            {
                txtContent.Text = "Xóa thành công!";
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }
    }
}
