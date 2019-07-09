using DTO_BookOnline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_BookOnline
{
    public class GhiNhoTaiKhoan
    {
        public static void WriteUserLogin(User user)
        {
            BinaryWriter bw;
            string id = user.ID1, pass = user.Password;
            string token = id + "||" + pass;
            if (!File.Exists(@".\User\"))
            {
                Directory.CreateDirectory(@".\User\");
            }
            try
            {
                bw = new BinaryWriter(new FileStream(@".\User\account.txt", FileMode.Create));
                
                bw.Write(Base64Encode(token));
            }
            catch (IOException ex)
            {
                Console.Write(ex.Message);
            }
        }
        public static User ReadUserLogin()
        {
            BinaryReader br;
            try
            {
                br = new BinaryReader(new FileStream(@".\User\account.txt", FileMode.Open));
                string token = Base64Decode(br.ReadString());
                string id = token.Substring(0, token.IndexOf("||"));
                string pass = token.Substring(token.IndexOf("||"), token.Length - id.Length).Replace("||","");
                try
                {
                    User user = new User();
                    DAL_User dal = new DAL_User();
                    user = dal.getUser(id, pass);
                    return user;
                }
                catch(Exception ex)
                {
                    Console.Write(ex.Message);
                    return null;
                }
            }
            catch (IOException ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public static bool KiemTraLuuTaiKhoanTonTai()
        {
            bool kiemtra = false;
            string[] array2 = Directory.GetFiles(@".\User\");
            if (array2.Length > 0)
            {
                kiemtra = true;
            }
            return kiemtra;
        }
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static void ClearData()
        {
            try
            {
                File.Delete(@".\User\abcde.txt");
            }
            catch { }
        }
    }
}
