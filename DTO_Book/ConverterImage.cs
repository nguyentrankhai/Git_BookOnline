using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DTO_BookOnline
{
    public class ConverterImage
    {
        public static byte[] ImageToBinary(string path)
        {
            FileStream fS = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] b = new byte[fS.Length];
            fS.Read(b, 0, (int)fS.Length);
            fS.Close();
            return b;
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
        public static byte[] ToBytes(string hex)
        {
            var shb = SoapHexBinary.Parse(hex);
            return shb.Value;
        }
    }
}
