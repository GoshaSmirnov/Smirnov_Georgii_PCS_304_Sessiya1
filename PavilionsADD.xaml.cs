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
    /// Логика взаимодействия для PavilionsADD.xaml
    /// </summary>
    public partial class PavilionsADD : Page
    {
        private Pavilions currentPV = new Pavilions();
        private int reg = 0;
        public PavilionsADD(Pavilions selectedPV)
        {
            InitializeComponent();
            ComboStatusEdit.ItemsSource = Ses1Entities.GetContext().Pavilions.Select(x => x.Status).Distinct().ToList();
            ComboSC.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Select(x => x.Name).Distinct().ToList();
            
            if (selectedPV != null)
            {
                currentPV = selectedPV;
                reg = 1;          
            }

            DataContext = currentPV;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();
            currentPV.SC_ID = Ses1Entities.GetContext().Shop_Centers.Where(b => b.Name == ComboSC.Text).Select(x => x.SC_ID).FirstOrDefault();

            if (string.IsNullOrWhiteSpace(currentPV.Pavilion_Number.ToString()))
                errors.AppendLine("Укажите номер");
            if (string.IsNullOrWhiteSpace(currentPV.Square.ToString()))
                errors.AppendLine("Укажите площадь");
            if (string.IsNullOrWhiteSpace(currentPV.Price_M2.ToString()))
                errors.AppendLine("Укажите стоимость за метр");
            if (string.IsNullOrWhiteSpace(currentPV.Value_Adder_Factor.ToString()))
                errors.AppendLine("Укажите коэф.добав.стоим.");
            if (string.IsNullOrWhiteSpace(currentPV.Floor.ToString()))
                errors.AppendLine("Укажите этажность");
            if (string.IsNullOrWhiteSpace(currentPV.SC_ID.ToString()))
                errors.AppendLine("Выберите Торговый Центр");
            if (reg == 0) Ses1Entities.GetContext().Pavilions.Add(currentPV);

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            try
            {
                Ses1Entities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена. Обновите таблицу");
                ADD_PV_Frame.Content = new PavilionsPage(null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            ADD_PV_Frame.Content = new PavilionsPage(null);
        }
    }
}
