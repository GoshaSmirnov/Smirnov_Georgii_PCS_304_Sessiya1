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
    /// Логика взаимодействия для ADD_SC.xaml
    /// </summary>
    public partial class ADD_SC : Page
    {
        private Shop_Centers currentSC = new Shop_Centers();
        private int reg = 0;
        int maxid = Ses1Entities.GetContext().Shop_Centers.Max(u => u.SC_ID);
        public ADD_SC(Shop_Centers selectedSC)
        {
            InitializeComponent();
            ComboStatusEdit.Items.Add("План");
            ComboStatusEdit.Items.Add("Строительсто");
            ComboStatusEdit.Items.Add("Реализация");

            if (selectedSC != null)
            {
                currentSC = selectedSC;
                reg = 1;
            }
            else
            {
                currentSC.SC_ID = maxid + 1;
            }

            DataContext = currentSC;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files | *.BMP;*.JPG;*.PNG";
            fileDialog.InitialDirectory = @"C:\Users\komar1511\Desktop\Practice\Image ТЦ";

            fileDialog.Title = "Выбор фото ТЦ";

            if (fileDialog.ShowDialog() == true)
            {

                currentSC.Photo = File.ReadAllBytes(fileDialog.FileName);
            }
            MessageBox.Show(" Файл выбран ");
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrWhiteSpace(currentSC.Name.ToString()))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(currentSC.Sity.ToString()))
                errors.AppendLine("Укажите город");
            if (string.IsNullOrWhiteSpace(currentSC.Number_Of_Pawilion.ToString()))
                errors.AppendLine("Укажите количество павильонов");
            if (string.IsNullOrWhiteSpace(currentSC.Price.ToString()))
                errors.AppendLine("Укажите стоимость тц");
            if (string.IsNullOrWhiteSpace(currentSC.Value_Adder_Factor.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(currentSC.Floor.ToString()))
                errors.AppendLine("Укажите этажность");
            if (reg == 0) Ses1Entities.GetContext().Shop_Centers.Add(currentSC);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                Ses1Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                currentSC = new Shop_Centers();
                ADD_SC_Frame.Content = new SC();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            ADD_SC_Frame.Content = new SC();

        }
    }
}
