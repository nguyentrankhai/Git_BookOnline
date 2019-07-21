using BUS_BookOnline;
using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Enterdata
{
    public partial class Book : Form
    {
        List<DTO_BookOnline.Book> lstBook = new List<DTO_BookOnline.Book>();
        List<Author> lstAuthor = new List<Author>();
        List<Catalog> lstCatalog = new List<Catalog>();
        List<Publisher> lstPbs = new List<Publisher>();
        List<Language> lstLang = new List<Language>();
        BUS_Book bus = new BUS_Book();
        public Book()
        {
            InitializeComponent();
            getCatalog();
            getAuthor();
            getCatalog();
            getPbs();
            getLang();
            getBook();
        }
        
        private void getBook()
        {
            lstBook = bus.getAllBook();
            int count = lstBook.Count();
            DTO_BookOnline.Book bk = lstBook[count - 1];
            txtMa.Text = Int32.Parse(bk.Id) + 1 + "";
            txtMa.Enabled = false;
        }
        private void getCatalog()
        {
            BUS_Catalog bus = new BUS_Catalog();
            lstCatalog = bus.getAllCatalog();

            cbbTheLoai.DataSource = lstCatalog;

            cbbTheLoai.DisplayMember = "Name";
            cbbTheLoai.ValueMember = "Name";
        }
        private void getAuthor()
        {
            BUS_Author bus = new BUS_Author();
            lstAuthor = bus.getAllAuthor();

            cbbTacGia.DataSource = lstAuthor;

            cbbTacGia.DisplayMember = "Name";
            cbbTacGia.ValueMember = "Name";
        }
        private void getPbs()
        {
            BUS_Publisher bus = new BUS_Publisher();
            lstPbs = bus.getAllPublisher();

            cbbNXB.DataSource = lstPbs;

            cbbNXB.DisplayMember = "Name";
            cbbNXB.ValueMember = "Id";
        }
        private void getLang()
        {
            BUS_Language bus = new BUS_Language();
            lstLang= bus.getAllLanguage();

            cbbNgonNgu.DataSource = lstLang;

            cbbNgonNgu.DisplayMember = "langName";
            cbbNgonNgu.ValueMember = "langID";
        }
        private void BrowseImage()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            dlg.Title = "Select avatar";
            if (dlg.ShowDialog() != null)
            {
                txtHinh.Text = dlg.FileName;
            }
        }

        private void txtHinh_Enter(object sender, EventArgs e)
        {
            BrowseImage();
        }

        private void txtURL_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(txtTen.Text!="")
            {
                DTO_BookOnline.Book bk = new DTO_BookOnline.Book();
                bk.Id = txtMa.Text;
                bk.Name = txtTen.Text;
                bk.Author = cbbTacGia.Text;
                bk.Language = cbbNgonNgu.SelectedItem as Language;
                bk.Note = txtGhiChu.Text;
                bk.Summary = txtTomTat.Text;
                bk.Price = Double.Parse( txtGia.Text);
                bk.Catalog = cbbTheLoai.Text;
                bk.Url = txtURL.Text;
                bk.Trial_url = txtTrial.Text;
                bk.Year = txtNamSX.Text;
                if(bus.insertBook(bk))
                {
                    MessageBox.Show("Thêm thành công");
                }
            }
        }
    }
}
