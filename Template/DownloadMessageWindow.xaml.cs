using System;
using System.Windows;
using DTO_BookOnline;
using System.Net;
using System.Threading;
using System.IO;
using MaterialDesignColors.WpfExample.Domain;
using MaterialDesignThemes.Wpf;

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
        string drive = "drive";
        string download = "download";
        public DownloadMessageWindow()
        {
            InitializeComponent();
            this.Focus();
            checkExistFolder();
        }

        private void checkExistFolder()
        {
            string folderPath = @".\trial\";
            string folderPath2 = @".\Warehouse\";
            createFolder(folderPath);
            createFolder(folderPath2);
        }

        private void createFolder(string folderPath)
        {
            if (!File.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
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
                        if (book.Trial_url.Contains(drive) && book.Trial_url.Contains(download) == false)
                        {
                            book.Trial_url = book.Trial_url.Replace(" ", "");
                            int i = book.Trial_url.IndexOf("?");
                            string str = book.Url.Substring(i, book.Url.Length - i);
                            string uri = "https://drive.google.com/uc" + str + "&export=download";
                            webclient.DownloadFileAsync(new Uri(uri), path);
                        }
                        else
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
                    try { double percent = int.Parse((e.BytesReceived / e.TotalBytesToReceive * 100).ToString()); }
                    catch(Exception ex) { return; }
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
                    string str = "trial";
                    if (path.Contains(str))
                    {
                        this.Dispatcher.Invoke(() =>
                    {
                        if (cancel) return;
                        PDF_TrialWindow.BOOK = book;
                        PDF_TrialWindow w = new PDF_TrialWindow();
                        w.Show();
                        this.Close();
                    });
                    }
                    else
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
            string str = "trial";
            if (path.Contains(str))
            {
                downloadTrialFile(book);
            }
            else downloadFile(book);
        }

        private async void btnHuyDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                webclient.CancelAsync();
                              
            }
            catch(Exception ex)
            {
                System.Console.Write("----------------" + Session.User + "\n--------------------\n" + ex.ToString());
                var sampleMessageDialog = new SampleMessageDialog
                {
                    Message = { Text = "Đường dẫn sai" }
                };

                await DialogHost.Show(sampleMessageDialog, "RootDialog");

            }
            finally
            {   Close();
                MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.WindowState = WindowState.Normal;
                mainWindow.MainContent.Content = new BookInfo_UC(book.Id);
            }
        }

        private void downloadTrialFile(Book book)
        {
            if (book == null && book.Trial_url == "") return;

            try
            {
                book.Trial_url = book.Trial_url.Replace(" ", "");
                if (book.Trial_url.Replace(" ", "").Length > 0)
                {
                    thread = new Thread(() =>
                    {
                        string drive = "drive";
                        string download = "download";
                        if(book.Trial_url.Replace(" ", "").Length > 0)
                        webclient = new WebClient();
                        webclient.DownloadFileCompleted += Webclient_DownloadFileCompleted;
                        webclient.DownloadProgressChanged += Webclient_DownloadProgressChanged;
                        if (book.Trial_url.Contains(drive) && book.Trial_url.Contains(download)==false)
                        {
                            int i = book.Trial_url.IndexOf("?");
                            string str = book.Trial_url.Substring(i, book.Trial_url.Length- i);
                            string uri = "https://drive.google.com/uc" + str + "&export=download";
                            webclient.DownloadFileAsync(new Uri(uri), path);
                        }
                        else
                        webclient.DownloadFileAsync(new Uri(book.Trial_url), path);
                        

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
    }
}
