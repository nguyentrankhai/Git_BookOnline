using System.Windows.Controls;
using BUS_BookOnline;
using DTO_Book;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Win32;
using Template.Domain.SaveFile;
namespace Template
{
    /// <summary>
    /// Interaction logic for ReadMore_UC.xaml
    /// </summary>
    public partial class ReadMore_UC : UserControl
    {
        private static string[] color;
        public ReadMore_UC()
        {
            InitializeComponent();
            addColor();
            fillData();
        }

        private void addColor()
        {
            color = new string[5];
            color[0] = "#1E88E5";
            color[1] = "#3949AB";
            color[2] = "#E53935";
            color[3] = "#00ACC1";
            color[4] = "#039EB5";
        }

        private void fillData()
        {
            BUS_Annotation bus = new BUS_Annotation();
            List<Annotation> lstAnno = new List<Annotation>();
            lstAnno = bus.getAnnotationData(Session.User.ID1);
            //treeAnnotation.ItemsSource = lstAnno;
            treeAnnotation.ItemsSource = (generateTreeView(lstAnno));
        }
        private List<TreeAnno> generateTreeView(List<Annotation> lst)
        {
            List<TreeAnno> tree = new List<TreeAnno>();
            var booknames = lst.GroupBy(x => x.Book_name).Select(g => g.First()).ToList();
            
            for(int i = 0; i< booknames.Count; i++)
            {
                TreeAnno root = new TreeAnno() { Title = booknames[i].Book_name };
                List<Annotation> lstTemp = new List<Annotation>();
                lstTemp = lst.Where(x => x.Book_name == booknames[i].Book_name).ToList();
                foreach (Annotation ann in lstTemp)
                {
                    root.Items.Add(new TreeAnno() { Title = ann.Text });
                }
                tree.Add(root);
            }
            return tree;
        }

        private void treeAnnotation_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            TreeAnno tree = sender as TreeAnno;
            string t = tree.Title;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Xmind file (*.xmind)|*.xmind";
            saveFileDialog.FileName = "AnnotationFor" + Session.User.ID1 + ".xmind";
            if (saveFileDialog.ShowDialog() == true)
            {
                string fileName = saveFileDialog.FileName;
                SaveXmindFile saveFile = new SaveXmindFile();
                BUS_Annotation BusAnn = new BUS_Annotation();
                List<Annotation> lstAnno = new List<Annotation>();
                lstAnno = BusAnn.getAnnotationData(Session.User.ID1);
                saveFile.SaveXmind(lstAnno, fileName);
            }
        }
    }
    public class TreeAnno
    {
        public TreeAnno()
        {
            this.Items = new ObservableCollection<TreeAnno>();
        }

        public string Title { get; set; }

        public ObservableCollection<TreeAnno> Items { get; set; }
    }
}
