using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Smirnov_Georgii_PCS_304_Sessiya1
{
    /// <summary>
    /// Логика взаимодействия для PhotoADD.xaml
    /// </summary>
    public partial class PhotoADD : Page
    {
        public PhotoADD()
        {
            InitializeComponent();
        }
        private void BtnPhoto_Click(object sender, RoutedEventArgs e)
        {
            string path = "C:\\Users\\Student\\Desktop\\Сессия 1\\Sotrudniki_IMG";
            //string path = "C:\\Users\\das44\\Desktop\\Сессия 1\\Sotrudniki_IMG";
            var photos = Directory.EnumerateFiles(path);
            using (Ses1Entities context = new Ses1Entities())
            {
                foreach (var photo in photos)
                {
                    string s = photo.Substring(photo.LastIndexOf('\\') + 1).Split(' ')[0];
                    var employ = context.Employees.Where(x => x.Surname == s).FirstOrDefault();
                    if (employ != null)
                        employ.Photo = File.ReadAllBytes(photo);
                }
                context.SaveChanges();
            }
            var path2 = "C:\\Users\\Student\\Desktop\\Сессия 1\\Sotrudniki_IMG";
            //var path2 = "C:\\Users\\das44\\Desktop\\Сессия 1\\Image ТЦ";
            var photos2 = Directory.EnumerateFiles(path2);
            using (Ses1Entities context = new Ses1Entities())
            {
                foreach (var photo in photos2)
                {
                    string s = photo.Substring(photo.LastIndexOf('\\') + 1);
                    string s2 = s.Substring(0, s.Length - 4);
                    var employ = context.Shop_Centers.Where(x => x.Name == s2).FirstOrDefault();
                    if (employ != null)
                        employ.Photo = File.ReadAllBytes(photo);
                }
                context.SaveChanges();
            }
            MessageBox.Show("Попытка загрузки завершена");
        }
    }
}