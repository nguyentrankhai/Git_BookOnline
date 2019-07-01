
using System.Linq;

using System.Windows;
using System.Windows.Controls;
using DAL_BookOnline;

namespace Template
{
    /// <summary>
    /// Interaction logic for Transaction_UC.xaml
    /// </summary>
    public partial class Transaction_UC : UserControl
    {
        public Transaction_UC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DAL_Transaction dal = new DAL_Transaction();
            gvData.ItemsSource = dal.getLichSuMuaHang(Session.User.ID1);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            TransactionInfo_UC uc = new TransactionInfo_UC();
            TransactionInfo_UC.idTrans = btn.Tag.ToString();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainContent.Content = uc;
        }
    }
}
