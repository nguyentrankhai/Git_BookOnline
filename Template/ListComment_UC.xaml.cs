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
namespace Template
{
    /// <summary>
    /// Interaction logic for ListComment_UC.xaml
    /// </summary>
    public partial class ListComment_UC : UserControl
    {
        Book book = new Book();
        public ListComment_UC(Book book)
        {
            InitializeComponent();
            this.book = book;
            loadComments(book.Id);

        }
        
        private void loadComments(string bookId)
        {
            BUS_Comment bus = new BUS_Comment();
           
            ItemData.ItemsSource = bus.getCommentsWithBookID(bookId);
        }
    }
}
