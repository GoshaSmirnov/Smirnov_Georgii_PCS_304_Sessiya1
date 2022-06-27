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
    /// Логика взаимодействия для autorization.xaml
    /// </summary>
    public partial class autorization : Page
    {
        public static int Emp_ID = 1;
        public autorization()
        {
            InitializeComponent();
        }
        public static string AutRole = "";
        int i = 0;
        private void Autorization_Click(object sender, RoutedEventArgs e)
        {
            if (i < 2)
            {
                Employees User = null;

                string _login = LoginText.Text.ToLower();
                string _password = PasswordText.Password.ToLower();
                User = Ses1Entities.GetContext().Employees.Where(b => b.Password == _password && b.Login == _login).FirstOrDefault();

                if (User == null)
                {
                    i++;
                    MessageBox.Show("Не найдено");
                }
                else
                {
                    Emp_ID = User.Employee_ID;
                    AutRole = User.Role;
                    if (AutRole == "Менеджер С")
                    {
                        MessageBox.Show("Добро пожаловать Менеджер С!");
                        AutorizationFrame.Content = new SC();
                    }
                }
            }
            else
            {
                MessageBox.Show("Ваши попытки исчерпаны, пройдите капчу");
                AutorizationFrame.Content = new Captcha();
            }
        }
    }
}
