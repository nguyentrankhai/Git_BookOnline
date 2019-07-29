﻿using System;
using System.Linq;
using DTO_BookOnline;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Collections.Generic;
using System.Globalization;

namespace DAL_BookOnline
{
    public class DAL_User
    {
        private User u = new User();
        public bool buyBook(User user, Book book)
        {
            DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
            if (user.Wallet >= book.Price)
            {
                tbl_Account us = context.tbl_Accounts.Where(x => x.AccountID == user.ID1).SingleOrDefault();
                tbl_Book bk = context.tbl_Books.Where(x => x.BookID == book.Id).SingleOrDefault();
                us.Wallet = us.Wallet - bk.PRICE;
                DAL_Book dal = new DAL_Book();
                book.Status = "M";
                dal.insertBookOfUser(user, book);
                DAL_Transaction dalTrans = new DAL_Transaction();
                List<tbl_Book> lstBook = new List<tbl_Book>();
                lstBook.Add(bk);
                bool b = dalTrans.buyBooks(user.ID1, 0, "", "", lstBook);
                context.SubmitChanges();
                return b;
            }
            return false;
        }

        private User parseSQLtoDTO(tbl_Account user)
        {

            //DateTime dt = DateTime.ParseExact(user.SignupDate.ToString(), "yyyy/MM/dd hh:mm:ss", CultureInfo.InvariantCulture);
            //string s = dt.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            u.ID1 = user.AccountID;
            u.Password = user.PWD;
            u.Username = user.NAME;
            u.Gen = user.GEN == true ? "Nam" : "Nữ";
            u.Status = user.Status == 1 ? 1 : 0;
            u.Wallet = (double)user.Wallet;
            u.Remaining = user.RemainingTime;
            u.SigninDate = DateTime.Now.ToString();
            u.Note = user.NOTE;
            u.Email = user.Email;
            u.Image1 = (ImageSource)convertImage("imgBanner/avatar.jpg");
            if (user.IMG != null)
            {
                u.Image1 = ByteToImage(user.IMG.ToArray());
            }
            return u;
        }
        public User getUser(string id, string pass)
        {
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                var user = context.tbl_Accounts.Where(n => n.AccountID == id && n.PWD == pass && n.EmailConfirmed == true && n.Status == 1).SingleOrDefault();
                if (user != null)
                {
                    return parseSQLtoDTO(user);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return null;
        }
        public User getUser(string id)
        {
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                var user = context.tbl_Accounts.Where(n => n.AccountID == id && n.EmailConfirmed == true && n.Status == 1).SingleOrDefault();
                if (user != null)
                {

                    return parseSQLtoDTO(user);
                }
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
            }
            return null;
        }
        public static object convertImage(object value)
        {
            string path = value as string;
            if (path != null)
            {
                BitmapImage image = new BitmapImage();
                using (FileStream stream = File.OpenRead(path))
                {
                    image.BeginInit();   //bắt đầu khởi tạo vùng nhớ Bit Map
                    image.StreamSource = stream;  //Sử dụng Stream để lấy hình ảnh
                    image.CacheOption = BitmapCacheOption.OnLoad; //load the image from the stream
                    image.EndInit(); // Đóng kết nối 
                }
                return image;
            }
            return null;
        }

        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage biImg = new BitmapImage();
            MemoryStream ms = new MemoryStream(imageData);
            biImg.BeginInit();
            biImg.StreamSource = ms;
            biImg.EndInit();

            ImageSource imgSrc = biImg as ImageSource;

            return imgSrc;
        }
        public static byte[] ImageToBinary(string path)
        {
            FileStream fS = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
        }
        public bool insertUser(User user)
        {
            bool result = false;
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Account us = new tbl_Account();
                us.AccountID = user.ID1;
                us.PWD = user.Password;
                us.NAME = user.Username;
                us.GEN = user.Gen == "1" ? true : false;
                us.Wallet = (decimal)user.Wallet;
                us.RemainingTime = user.Remaining;
                us.SignupDate = DateTime.Now;
                us.Wallet = 0;
                us.Status = 0;
                context.tbl_Accounts.InsertOnSubmit(us);
                context.SubmitChanges();
                result = true;
            }
            catch (Exception ex)
            {
                System.Console.Write(ex.Message);
                result = false;
            }
            return result;
        }
        public bool insertUser(tbl_Account acc)
        {
            bool result = false;
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                context.tbl_Accounts.InsertOnSubmit(acc);
                context.SubmitChanges();
                result = true;

            }
            catch (Exception e)
            {
                result = false;
            }
            return result;
        }
        public bool updateUser(User user, string path)
        {
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Account us = context.tbl_Accounts.Single(x => x.AccountID == user.ID1);
                us.NAME = user.Username;
                us.PWD = user.Password;
                if (user.Gen == "Nữ")
                {
                    us.GEN = false;
                }
                else us.GEN = true;
                if (path != "" && path != null)
                    us.IMG = ImageToBinary(path);

                context.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        } 



        public bool updateUser(tbl_Account user)
        {
            try
            {
                DataClassesBookOnlineDataContext context = new DataClassesBookOnlineDataContext();
                tbl_Account acc = context.tbl_Accounts.Single(u => u.AccountID == user.AccountID);
                acc.Email = user.Email;
                acc.NAME = user.NAME;
                acc.Wallet = user.Wallet;
                acc.RemainingTime = user.RemainingTime;
                acc.PWD = user.PWD;
                acc.Status = user.Status;
                context.SubmitChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
