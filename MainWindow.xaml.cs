using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Photo_Click (object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new PhotoADD();
        }

        private void Autorization(object sender, RoutedEventArgs e)
        {
            MainFrame.Content = new autorization();
        }
    }
}
