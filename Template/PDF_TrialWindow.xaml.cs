using DTO_Book;
using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace Template
{
    /// <summary>
    /// Interaction logic for PDF_TrialWindow.xaml
    /// </summary>
    public partial class PDF_TrialWindow : Window
    {
        public static Book BOOK;
        private FileStream stream = null;
        //private static HighlightCollection highlightCollection = null;
        private Annotation Markup_Annotation = null;
        private List<Annotation> lstAnnotation = null; // lưu trữ toàn cục
        private List<Annotation> lstTemplate = null; // Dùng để open file và create annotation
        private Annotation AnnotationTemplate = null;// Dùng để open file và create annotation
        private static string Path = "";
        private bool Flag_OpenBook = false;
        public PDF_TrialWindow()
        {
            InitializeComponent();
            this.Focus();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            openfilePDF(BOOK);
        }
        private void openfilePDF(Book book)
        {

            Path = "trial\\" + book.Id + ".pdf";
            try
            {
                stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.None);
                pdfViewer.DocumentSource = stream;
            }
            catch (Exception ex)
            {
                //downloadPDF();
                MessageBox.Show(ex.Message);
            }
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (stream != null)
            {
                stream.Close();
            }
            try
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.MainContent.Content = new BookInfo_UC(BOOK.Id);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }
    }
}
