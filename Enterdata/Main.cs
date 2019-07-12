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
    public partial class Main : Form
    {
        List<DTO_BookOnline.Book> lstBook = new List<DTO_BookOnline.Book>();
        List<Author> lstAuthor = new List<Author>();
        List<Catalog> lstCatalog = new List<Catalog>();
        List<Publisher> lstPbs = new List<Publisher>();
        List<Language> lstLang = new List<Language>();
        BUS_Book bus = new BUS_Book();

        public Main()
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
            txtTen.Text = "";
            txtGia.Text = 0 + "";
            txtURL.Text = "";
            txtTrial.Text = "";
            txtTomTat.Text = "";
            txtGhiChu.Text = "";
        }

        BUS_Catalog busCata = new BUS_Catalog();
        private void getCatalog()
        {

            lstCatalog = busCata.getAllCatalog();

            cbbTheLoai.DataSource = lstCatalog;

            cbbTheLoai.DisplayMember = "Name";
            cbbTheLoai.ValueMember = "Name";

            txtMaTL.Text = "";
            txtTenTL.Text = "";
        }

        BUS_Author busAuth = new BUS_Author();
        private void getAuthor()
        {

            lstAuthor = busAuth.getAllAuthor();

            cbbTacGia.DataSource = lstAuthor;

            cbbTacGia.DisplayMember = "Name";
            cbbTacGia.ValueMember = "Name";

            string str = lstAuthor[lstAuthor.Count - 1].Id;
            int id = Int32.Parse(str.Substring(2, 3))+1;
            if(id>9 && id <100)
            {
                txtMaTG.Text = "TG0" + (id);
            }
            else if (id <= 9)
            {
                txtMaTG.Text = "TG00" + (id);
            }
             else txtMaTG.Text = "TG" + (id);
            txtMaTG.Enabled = false;
            txtTenTG.Text = "";
            txtQuocTich.Text = 0+"";
            txtTomTatTG.Text = "";
            txtNoteTG.Text = "";
        }

        BUS_Publisher busPbl = new BUS_Publisher();
        private void getPbs()
        {

            lstPbs = busPbl.getAllPublisher();

            cbbNXB.DataSource = lstPbs;

            cbbNXB.DisplayMember = "Name";
            cbbNXB.ValueMember = "Id";

            string str = lstPbs[lstPbs.Count - 1].Id;
            int id = Int32.Parse(str.Substring(3, 3)) + 1;
            if (id > 9 && id<100)
            {
                txtMaNXB.Text = "NXB0" + (id);
            }
            else if (id <= 9)
            {
                txtMaNXB.Text = "NXB00" + (id);
            }
            else txtMaNXB.Text = "NXB" + (id);
            txtMaNXB.Enabled = false;
            txtTenNXB.Text = "";
            txtDiaChiNXB.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtFax.Text = "";
         }

        BUS_Language busLang = new BUS_Language();
        private void getLang()
        {

            lstLang = busLang.getAllLanguage();

            cbbNgonNgu.DataSource = lstLang;

            cbbNgonNgu.DisplayMember = "langName";
            cbbNgonNgu.ValueMember = "langID";

            txtMaNN.Text = "";
            txtTenNN.Text = "";
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

        private void btnThemSach_Click(object sender, EventArgs e)
        {
            if (txtTen.Text != "")
            {
                DTO_BookOnline.Book bk = new DTO_BookOnline.Book();
                bk.Id = txtMa.Text;
                bk.Name = txtTen.Text;
                bk.Author = cbbTacGia.Text;
                bk.Language = cbbNgonNgu.SelectedItem as Language;
                bk.Note = txtGhiChu.Text;
                bk.Summary = txtTomTat.Text;
                bk.Price = Double.Parse(txtGia.Text);
                bk.Catalog = cbbTheLoai.Text;
                bk.Url = txtURL.Text;
                bk.Trial_url = txtTrial.Text;
                bk.Year = txtNamSX.Text;
                if (bus.insertBook(bk))
                {
                    MessageBox.Show("Thêm thành công");
                    getBook();
                }
            }
        }

        private void btnThemTG_Click(object sender, EventArgs e)
        {
            if (txtTenTG.Text != "")
            {
                Author a = new Author();
                a.Id = txtMaTG.Text;
                a.Name = txtTenTG.Text;
                a.Country = txtQuocTich.Text;
                a.Date = DateTime.Parse(dpNgaySinh.Text);
                if (cbbGioiTinh.Text == "Nam")
                {
                    a.Gen = true;
                }
                else a.Gen = false;
                a.Summary = txtTomTatTG.Text;
                if (busAuth.insertAuthor(a))
                {
                    MessageBox.Show("Thêm thành công");
                    getAuthor();
                }
            }
        }

        private void btnThemTL_Click(object sender, EventArgs e)
        {
            if (txtTenTL.Text != "" && txtMaTL.Text != "")
            {
                Catalog cat = new Catalog();
                cat.Id = txtMaTL.Text;
                cat.Name = txtTenTL.Text;
                if (busCata.insertCatalog(cat))
                {
                    MessageBox.Show("Thêm thành công");
                    getCatalog();
                }
            }
        }

        private void btnThemNN_Click(object sender, EventArgs e)
        {
            if (txtTenNN.Text != "" && txtMaNN.Text != "")
            {
                Language cat = new Language();
                cat.LangID = txtMaNN.Text;
                cat.LangName = txtTenNN.Text;
                if (busLang.insertLang(cat))
                {
                    MessageBox.Show("Thêm thành công");
                    getLang();
                }
            }
        }
    }
}
