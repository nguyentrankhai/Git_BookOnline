using System.Windows;
using DTO_BookOnline;
using System.IO;
using System;
using DevExpress.Xpf.DocumentViewer;
using DevExpress.Xpf.PdfViewer;
using DevExpress.Pdf;
using Template.Domain.Highlight;
using Template.Domain;
using System.Collections.Generic;
using DTO_Book;
using BUS_BookOnline;
namespace Template
{
    /// <summary>
    /// Interaction logic for PDFWindow.xaml
    /// </summary>
    public partial class PDFWindow : Window
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
        public PDFWindow()
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
            Path = "Warehouse\\" + book.Id + ".pdf";
            try
            {
                stream = File.Open(Path, FileMode.Open, FileAccess.Read, FileShare.None);
                pdfViewer.DocumentSource = stream;
                if (book.Page != 0)
                {
                    var message = MessageBox.Show("Bạn muốn mở lại lại trang đọc trước đó không?", "Mở lại trang đã đọc", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (message == MessageBoxResult.Yes)
                    {
                        GoToPage(book.Page);
                    }
                }
            }
            catch (Exception ex)
            {
                //downloadPDF();
                MessageBox.Show(ex.Message);
            }
        }
        private void GoToPage(int page)
        {
            Dispatcher.BeginInvoke(new Action(() => { pdfViewer.CurrentPageNumber = page; })); //cho chạy đến trang đã lưu từ sql
                                                                                               //pdfViewer.SetPageNumberCommand.Execute(book.Page);
                                                                                               // pdfViewer.CurrentPageNumber = book.Page;

        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (stream != null)
            {
                BOOK.Page = pdfViewer.CurrentPageNumber;
                BUS_Book bus = new BUS_Book();
                bus.updateBookOfUser(Session.User, BOOK);
                stream.Close();
            }
            try
            {
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.MainContent.Content = new WareHouse_UC();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
           
        }

        private void openHighlight()
        {
            BUS_Annotation bus = new BUS_Annotation();
            lstTemplate = new List<Annotation>();
            lstTemplate = bus.getAnnotationData(Session.User.ID1, BOOK.Id);
           
            if (lstTemplate != null)
            {
                Flag_OpenBook = true;
                foreach (Annotation item in lstTemplate)
                {
                    pdfViewer.FindText(new TextSearchParameter() { Text = item.Text, CurrentPage = item.Page });
                    //pdfViewer.HighlightSelectionColor = item.Annotation_color;
                    AnnotationTemplate = new Annotation();
                    AnnotationTemplate = item;
                    switch (item.Annotation_type)
                    {
                        case PdfTextMarkupAnnotationType.Highlight:
                            pdfViewer.HighlightSelectedText(item.Text);
                            break;
                        case PdfTextMarkupAnnotationType.Underline:
                            pdfViewer.UnderlineSelectedText(item.Text);
                            break;
                        case PdfTextMarkupAnnotationType.StrikeOut:
                            pdfViewer.StrikethroughSelectedText(item.Text);
                            break;
                        default:
                            pdfViewer.HighlightSelectedText(item.Text);
                            break;
                    }
                }
                bus.deleteAnnotationData(Session.User.ID1, BOOK.Id);
                bus.insertAnnotationData(Session.User.ID1, lstTemplate);
                lstAnnotation = new List<Annotation>();
                lstAnnotation = lstTemplate;
                Flag_OpenBook = false;
            }

        }
        private void pdfViewer_DocumentLoaded(object sender, RoutedEventArgs e)
        {
            openHighlight();
        }

        private void pdfViewer_TextMarkupAnnotationCreating(DependencyObject d, PdfTextMarkupAnnotationCreatingEventArgs e)
        {
            if (Flag_OpenBook)
            {
                e = openBook_CreatingAnnotation(e);
            }
            else
            {
                if (lstAnnotation == null)
                {
                    lstAnnotation = new List<Annotation>();
                }
                Markup_Annotation = new Annotation();
                Markup_Annotation.Annotation_name = e.Name;
                Markup_Annotation.Text = e.SelectedText;
                Markup_Annotation.User_name = e.Author;
                Markup_Annotation.Annotation_type = e.Style;
                Markup_Annotation.Subject = e.Subject;
                Markup_Annotation.Comment = e.Comment;
                Markup_Annotation.Book_name = BOOK.Name;
                Markup_Annotation.Annotation_color = e.Color;
                Markup_Annotation.Iduser = Session.User.ID1;
                Markup_Annotation.Idbook = BOOK.Id;
                Markup_Annotation.CreationDate = DateTime.Parse(e.CreationDate.Value.ToString());
                Markup_Annotation.ModificationDate = DateTime.Parse(e.ModificationDate.Value.ToString());
                Markup_Annotation.Page = pdfViewer.CurrentPageNumber;
                foreach (PdfQuadrilateral quad in e.Quads)
                {
                    Markup_Annotation.Quart = new double[8];
                    Markup_Annotation.Quart[0] = quad.P1.X;
                    Markup_Annotation.Quart[1] = quad.P1.Y;
                    Markup_Annotation.Quart[2] = quad.P2.X;
                    Markup_Annotation.Quart[3] = quad.P2.Y;
                    Markup_Annotation.Quart[4] = quad.P3.X;
                    Markup_Annotation.Quart[5] = quad.P3.Y;
                    Markup_Annotation.Quart[6] = quad.P4.X;
                    Markup_Annotation.Quart[7] = quad.P4.Y;
                }
                lstAnnotation.Add(Markup_Annotation);
            }            
        }

        private PdfTextMarkupAnnotationCreatingEventArgs openBook_CreatingAnnotation(PdfTextMarkupAnnotationCreatingEventArgs e)
        {
            AnnotationTemplate.Annotation_name = e.Name;
            e.Color = AnnotationTemplate.Annotation_color;
            e.Comment = AnnotationTemplate.Comment;
            e.Subject = AnnotationTemplate.Subject;
            e.Author = AnnotationTemplate.User_name;
            e.CreationDate = AnnotationTemplate.CreationDate;
            e.ModificationDate = AnnotationTemplate.ModificationDate;
            return e;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BUS_Annotation bus = new BUS_Annotation();
            bus.deleteAnnotationData(Session.User.ID1, BOOK.Id);
            bus.insertAnnotationData(Session.User.ID1, lstAnnotation);
        }

        private void pdfViewer_TextMarkupAnnotationChanged(object sender, PdfTextMarkupAnnotationChangedEventArgs e)
        {
            //MessageBox.Show(e.Color.ToString());
            try
            {
                Markup_Annotation = new Annotation();
                Markup_Annotation = lstAnnotation.Find(x => x.Annotation_name == e.Name);
                if (Markup_Annotation == null)
                {
                    Markup_Annotation = lstAnnotation.Find(x => x.CreationDate == e.CreationDate);
                    if(Markup_Annotation == null)
                    {
                        return;
                    }
                }
                int index = lstAnnotation.IndexOf(Markup_Annotation);
               
                Markup_Annotation.User_name = e.Author;
                Markup_Annotation.Annotation_type = e.Style;
                Markup_Annotation.Subject = e.Subject;
                Markup_Annotation.Comment = e.Comment;
                Markup_Annotation.Book_name = BOOK.Name;
                Markup_Annotation.Annotation_color = e.Color;
                Markup_Annotation.Iduser = Session.User.ID1;
                Markup_Annotation.Idbook = BOOK.Id;
                Markup_Annotation.CreationDate = DateTime.Parse(e.CreationDate.Value.ToString());
                Markup_Annotation.ModificationDate = DateTime.Parse(e.ModificationDate.Value.ToString());
                Markup_Annotation.Page = pdfViewer.CurrentPageNumber;
                foreach (PdfQuadrilateral quad in e.Quads)
                {
                    Markup_Annotation.Quart = new double[8];
                    Markup_Annotation.Quart[0] = quad.P1.X;
                    Markup_Annotation.Quart[1] = quad.P1.Y;
                    Markup_Annotation.Quart[2] = quad.P2.X;
                    Markup_Annotation.Quart[3] = quad.P2.Y;
                    Markup_Annotation.Quart[4] = quad.P3.X;
                    Markup_Annotation.Quart[5] = quad.P3.Y;
                    Markup_Annotation.Quart[6] = quad.P4.X;
                    Markup_Annotation.Quart[7] = quad.P4.Y;
                }

                lstAnnotation[index] = Markup_Annotation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void pdfViewer_AnnotationDeleting(DependencyObject d, AnnotationDeletingEventArgs e)
        {
            Markup_Annotation = new Annotation();

            Markup_Annotation = lstAnnotation.Find(x => x.Annotation_name == e.Name);
            if(Markup_Annotation != null)
            {
                lstAnnotation.Remove(Markup_Annotation);
            }
            else
            {
                
            }
        }
    }
}
