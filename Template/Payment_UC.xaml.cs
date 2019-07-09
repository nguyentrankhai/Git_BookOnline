using BUS_BookOnline;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using DTO_Book;
using MaterialDesignThemes.Wpf;
using Template.Domain.VNPayment;

namespace Template
{
    /// <summary>
    /// Interaction logic for Payment_UC.xaml
    /// </summary>
    public partial class Payment_UC : UserControl
    {
        public Payment_UC()
        {
            InitializeComponent();
            loadBranch();
        }

        private void loadBranch()
        {
            BUS_Payment bus = new BUS_Payment();
            List<BranchPayment> lst = bus.getBranchPayment();
            cbbBranch.ItemsSource = lst;
            cbbBranch.DisplayMemberPath = "BranchName";
            cbbBranch.SelectedValuePath = "BranchCode";
        }

        private void btnPayment_Click(object sender, RoutedEventArgs e)
        {
            
            double amount = 0;
            if (double.TryParse(txtAmount.Text, out amount))
            {
                if (UtilPayment.isValidateAmountPayment(amount))
                {
                    WebView_UC.Amount = amount;
                    WebView_UC.BranchCode = ((BranchPayment)cbbBranch.SelectedItem).BranchCode;
                    //Command = "{x:Static materialDesign:DialogHost.CloseDialogCommand}"
                    DialogHost.CloseDialogCommand.Execute(null, null);
                    WebView_UC uc = new WebView_UC();
                    Session.GetMainWindow().SetContentControl(uc);
                }
                else
                {
                    MessageBox.Show("Số tiền không hợp lệ");
                }
            }
        }
    }
}
