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
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace Smirnov_Georgii_PCS_304_Sessiya1
{
    /// <summary>
    /// Логика взаимодействия для EmployeAdd.xaml
    /// </summary>
    public partial class EmployeAdd : Page
    {
        private static readonly char[] SpecialChars = "!@#$%^&*()_/-+=".ToCharArray();
        private Employees _currentEmp;
        private int reg = 0, idEmp;
        public EmployeAdd(Employees upd)
        {
            InitializeComponent();
            if (upd != null)
                _currentEmp = upd;
                //reg = 1;
            else
                _currentEmp = new Employees();
            _currentEmp.Employee_ID = Ses1Entities.GetContext().Employees.Max(x => x.Employee_ID) + 1;
            ComboRole.ItemsSource = Ses1Entities.GetContext().Employees.Select(x => x.Role).Distinct().ToList();
            DataContext = _currentEmp;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(_currentEmp.Surname.ToString()))
                errors.AppendLine("Укажите Фамилия");
            if (string.IsNullOrWhiteSpace(_currentEmp.Name.ToString()))
                errors.AppendLine("Укажите Имя");
            if (string.IsNullOrWhiteSpace(_currentEmp.Pastname.ToString()))
                errors.AppendLine("Укажите Отчество");
            if (string.IsNullOrWhiteSpace(_currentEmp.Login.ToString()))
                errors.AppendLine("Укажите Логин");
            if (string.IsNullOrWhiteSpace(_currentEmp.Password.ToString()))
                errors.AppendLine("Укажите Пароль");
            if (string.IsNullOrWhiteSpace(_currentEmp.Role.ToString()))
                errors.AppendLine("Укажите Роль");
            //проверка пароля:
            string str2; int i = 0; bool boo; int yes = 0;
            if (_currentEmp.Password.Length < 6) errors.AppendLine("Пароль должен быть длиннее 6 символов");
            str2 = _currentEmp.Password.ToLower();
            if (_currentEmp.Password == str2) errors.AppendLine("В пароле должны быть большие буквы");
            int indexOf = str2.IndexOfAny(SpecialChars);
            if (indexOf == -1) errors.AppendLine("В пароле должны быть специальные символы");
            char[] array = _currentEmp.Password.ToCharArray();
            while (_currentEmp.Password.Length > i)
            {
                boo = Char.IsDigit(array[i]);
                if (boo == true) yes = yes + 1;
                i = i + 1;
            }
            if (_currentEmp.Password.Length <= yes) errors.AppendLine("Пароль должен включать в себя ещё и буквы, большие и маленькие");
            if (yes == 0) errors.AppendLine("Пароль должен включать в себя ещё и цифры");           
            if (reg == 0) Ses1Entities.GetContext().Employees.Add(_currentEmp);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }
            try
            {
                Ses1Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                _currentEmp = new Employees();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\komar1511\Desktop\Practice\Sotrudniki_IMG";

            fileDialog.Title = "Выбор фото Сотрудника";

            if (fileDialog.ShowDialog() == true)
            {

                _currentEmp.Photo = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show(" Файл выбран ");
        }
    private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            ADD_Emp_Frame.Content = new Employee_Page(null);
        }
    }
}
