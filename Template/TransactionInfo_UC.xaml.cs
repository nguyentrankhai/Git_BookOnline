using System;
using System.Collections.Generic;
using System.Linq;

using System.Windows;
using System.Windows.Controls;

using DAL_BookOnline;
using DTO_Book;

namespace Template
{
    /// <summary>
    /// Interaction logic for TransactionInfo_UC.xaml
    /// </summary>
    public partial class TransactionInfo_UC : UserControl
    {
        public static string idTrans = "";
        public TransactionInfo_UC()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            DAL_Transaction dal = new DAL_Transaction();
            //gvData.ItemsSource = dal.getLichSuInfo(idTrans);
            // grid.DataContext = dal.getReport(idTrans);
            List<TransactionInfo> lst = dal.getReport(idTrans);
            gvData.ItemsSource = lst;
            grid.DataContext = lst;

        }



        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            Preview_Report.idTrans = idTrans;
            Preview_Report uc = new Preview_Report();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainContent.Content = uc;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Transaction_UC uc = new Transaction_UC();
            MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.MainContent.Content = uc;
        }
    }
}
