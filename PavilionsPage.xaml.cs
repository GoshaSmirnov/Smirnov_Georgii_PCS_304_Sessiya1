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
    /// Логика взаимодействия для PavilionsPage.xaml
    /// </summary>
    public partial class PavilionsPage : Page
    {
        private string _name2, _name;
        int Current_SC_ID, reg = 0;
        public PavilionsPage(Shop_Centers currentSC)
        {
            InitializeComponent();
            if (currentSC != null)
            {
                reg = 1;
                Current_SC_ID = currentSC.SC_ID;
                DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(x => x.SC_ID == currentSC.SC_ID).ToList();
            }
            else
            {
                DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(x => x.Status != "Удален").ToList();
            }
            ComboFloor.ItemsSource = Ses1Entities.GetContext().Pavilions.Select(x => x.Floor).Distinct().ToList();
            ComboStatus.ItemsSource = Ses1Entities.GetContext().Pavilions.Select(x => x.Status).Distinct().ToList();
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridPavilions.SelectedItems.Cast<Pavilions>().FirstOrDefault();
            SC_Frame.Content = new PavilionsADD(upd);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ShoppingsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilions>().ToList();
            if (MessageBox.Show("Вы точно хотите Удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ShoppingsForRemoving.ForEach(x => x.Status = "Удален");
                    Ses1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(x => x.Status != "Удален").ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Combo_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (ComboFloor.Text == "" && ComboStatus.Text != "")
            {
                if (reg == 1) DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Status.ToString() == ComboStatus.SelectedItem.ToString() && b.SC_ID == Current_SC_ID).ToList();
                else DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Status.ToString() == ComboStatus.SelectedItem.ToString()).ToList();
            }
            else if (ComboFloor.Text != "" && ComboStatus.Text == "")
            {
                if (reg == 1) DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Floor == ComboFloor.SelectedIndex + 1 && b.Status != "Удален" && b.SC_ID == Current_SC_ID).ToList();
                else DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Floor == ComboFloor.SelectedIndex+1 && b.Status != "Удален").ToList();
            }
            else if (ComboStatus.Text != "" && ComboFloor.Text != "")
            {
                if (reg == 1) DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Floor == ComboFloor.SelectedIndex + 1 && b.Status.ToString() == ComboStatus.SelectedItem.ToString() && b.SC_ID == Current_SC_ID).ToList();
                else DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Floor == ComboFloor.SelectedIndex+1 && b.Status.ToString() == ComboStatus.SelectedItem.ToString()).ToList();
            }
        }
        private void SquareTextTo_TextChanged(object sender, TextChangedEventArgs e)
        {

             _name = SquareTextMin.Text;
             _name2 = SquareTextMax.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);

            if (reg == 1) DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == Current_SC_ID).ToList();
            else DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2).ToList();
        }
        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            SC_Frame.Content = new SC();
        }
        private void BtnRent_Click(object sender, RoutedEventArgs e)
        {
            var rnt = DGridPavilions.SelectedItems.Cast<Pavilions>().FirstOrDefault();
            SC_Frame.Content = new RentalPage(rnt);
        }

        private void SC_Frame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SC_Frame.Content = new PavilionsADD(null);
        }

        private void BtnPavilions_Click(object sender, RoutedEventArgs e)
        {
            SC_Frame.Content = new SC();
        }
    }
}
