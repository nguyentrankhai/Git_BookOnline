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

namespace Template
{
    /// <summary>
    /// Interaction logic for Preview_Report.xaml
    /// </summary>
    public partial class Preview_Report : UserControl
    {
        public static string idTrans = "";

        public Preview_Report()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            XtraReport_TransactionReport xtra = new XtraReport_TransactionReport();
            DevExpress.XtraReports.Parameters.Parameter param = new DevExpress.XtraReports.Parameters.Parameter();

            xtra.Parameters["TransactionID"].Value = idTrans;

            documentPreview.DocumentSource = xtra;

            xtra.RequestParameters = false;
            xtra.CreateDocument();
        }
    }
}
