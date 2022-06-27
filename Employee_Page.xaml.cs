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
    /// Логика взаимодействия для Employee_Page.xaml
    /// </summary>
    public partial class Employee_Page : Page
    {
        public Employee_Page(Employees _currentEmp)
        {
            int idEmp = 0;
            InitializeComponent();
            idEmp = _currentEmp?.Employee_ID ?? 0;
            DGridEmp.ItemsSource = Ses1Entities.GetContext().Employees.Where(x => x.Role != "Удален").ToList();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow mainWindow = new MainWindow();
            //mainWindow.Show();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeePageFrame.Content = new EmployeAdd(null);
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridEmp.SelectedItems.Cast<Employees>().FirstOrDefault();
            EmployeePageFrame.Content = new EmployeAdd(upd);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var EmpForRemoving = DGridEmp.SelectedItems.Cast<Employees>().ToList();
            if (MessageBox.Show("Вы точно хотите Удалить этих(-ого) Сотрудников", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EmpForRemoving.ForEach(x => x.Role = "Удален");
                    Ses1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridEmp.ItemsSource = Ses1Entities.GetContext().Employees.Where(x => x.Role != "Удален").ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb.Text != "")
            {
                var filteredList = Ses1Entities.GetContext().Employees.Where(t => t.Surname.ToLower().Contains(tb.Text.ToLower())).ToList();
                DGridEmp.ItemsSource = null; //Обнуляем список
                DGridEmp.ItemsSource = filteredList; //Обновляем список
            }
            else
            {
                DGridEmp.ItemsSource = Ses1Entities.GetContext().Employees.Where(x => x.Role != "Удален").ToList(); //Первоначальный список
            }
        }
    }
}
