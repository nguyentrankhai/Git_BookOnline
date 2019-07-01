using System;
using System.Windows;
using DTO_BookOnline;
using System.Net;
using System.Threading;
using System.IO;

namespace Template
{
    /// <summary>
    /// Interaction logic for DownloadMessageWindow.xaml
    /// </summary>
    public partial class DownloadMessageWindow : Window
    {
        public Book book;
        public string path; //Đường dẫn lưu file down vào thư mục Warehouse\\tênfile.pdf
        private WebClient webclient;
        private Thread thread;
        private bool cancel = false;
        public DownloadMessageWindow()
        {
            InitializeComponent();
        }

        private void downloadFile(Book book)
        {
            if (book == null && book.Url == "") return;

            try
            {
                if (book.Url.Replace(" ", "").Length > 0)
                {
                    thread = new Thread(() =>
                    {
                    
                            webclient = new WebClient();
                            webclient.DownloadFileCompleted += Webclient_DownloadFileCompleted;
                            webclient.DownloadProgressChanged += Webclient_DownloadProgressChanged;                    
                            webclient.DownloadFileAsync(new Uri(book.Url), path);
                    
                    
                    });
                    thread.Start();
                }
                else
                {
                    MessageBox.Show("Có lỗi xuất phát từ hệ thống \n Không tìm thấy địa chỉ download.", "Có lỗi xảy ra");
                    this.Close();
                    MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.WindowState = WindowState.Normal;
                    return;
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
        }

        private void Webclient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            try
            {
                this.Dispatcher.Invoke(() => {
                    if (cancel) return;
                    txtDetailDownload.Text = "Đang tải sách: " + book.Name + " - " + e.ProgressPercentage.ToString() + "%";
                    double percent = int.Parse((e.BytesReceived / e.TotalBytesToReceive * 100).ToString());
                    pgbDownloading.Value = int.Parse(e.ProgressPercentage.ToString()); ;
                });
            }catch(Exception ex)
            {
                MessageBox.Show("ERROR \nĐã có lỗi phát sinh, vui lòng thử lại!");
                File.Delete(path);
            }
            
        }

        private void Webclient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Cancelled == false)
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        if (cancel) return;
                        PDFWindow.BOOK = book;
                        PDFWindow w = new PDFWindow();
                        w.Show();
                        this.Close();
                    });
                }
                if (e.Cancelled)
                {
                    File.Delete(path);
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                System.Console.Write("----------------" + Session.User + "\n--------------------\n" + ex.ToString());
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Title = "Đang tải sách: " + book.Name;
            downloadFile(book);
        }

        private void btnHuyDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                webclient.CancelAsync();
                Close();               
            }
            catch(Exception ex)
            {
                System.Console.Write("----------------" + Session.User + "\n--------------------\n" + ex.ToString());
            }
        }
    }
}
