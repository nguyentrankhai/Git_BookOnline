
using DTO_BookOnline;
using System.Windows;

namespace Template
{
    public class Session
    {
        public static User User = null;
        public Session()
        {
            User = new User();            
        }
        public static MainWindow GetMainWindow()
        {
            return (MainWindow)Application.Current.MainWindow;
        }
    }
}
