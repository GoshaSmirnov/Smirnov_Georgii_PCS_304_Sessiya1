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
    /// Логика взаимодействия для TestPV.xaml
    /// </summary>
    public partial class TestPV : Page
    {
        string _name;
        string _name2;
        int idShop = 0;
        public TestPV(Shop_Centers currentShopping)
        {
            InitializeComponent();
            idShop = currentShopping.SC_ID;
            DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(x => x.SC_ID == currentShopping.SC_ID).ToList();
            ComboFloor.ItemsSource = Ses1Entities.GetContext().Pavilions.Select(x => x.Floor).Distinct().ToList();
            ComboStatus.ItemsSource = Ses1Entities.GetContext().Pavilions.Select(x => x.Status).Distinct().ToList();

        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            //AddEditPageShopping win = new AddEditPageShopping(PavilionsEntities.GetContext().Shoppings.Find(idShop));
        }

        private void ComboFloor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboFloor.SelectedItem;
            List<Pavilions> SearchType = null;
            SearchType = Ses1Entities.GetContext().Pavilions.Where(b => b.Floor.ToString() == c.ToString() && b.Value_Adder_Factor > 0.1 && b.Value_Adder_Factor > 0.1 && b.SC_ID == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }

        private void ComboStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var c = ComboStatus.SelectedItem;
            List<Pavilions> SearchType = null;
            SearchType = Ses1Entities.GetContext().Pavilions.Where(b => b.Status.ToString() == c.ToString() && b.Value_Adder_Factor > 0.1 && b.SC_ID == idShop).ToList();
            DGridPavilions.ItemsSource = SearchType;
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //AddEditPagePavilion win = new AddEditPagePavilion(null, PavilionsEntities.GetContext().Shoppings.Find(idShop));

        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var PavilionsForRemoving = DGridPavilions.SelectedItems.Cast<Pavilions>().ToList();
            if (MessageBox.Show("Вы точно хотите удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    PavilionsForRemoving.ForEach(x => x.Status = "Удален");
                    Ses1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Value_Adder_Factor > 0.1).ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            //var upd = DGridPavilions.SelectedItems.Cast<Pavilions>().FirstOrDefault();
            //AddEditPagePavilion win = new AddEditPagePavilion(upd, PavilionsEntities.GetContext().Shoppings.Find(idShop));
        }


        private void SquareTextTo_TextChanged(object sender, TextChangedEventArgs e)
        {

            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);

            DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == idShop && b.Value_Adder_Factor > 0.1).ToList();
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            _name = SquareTextFrom.Text;
            _name2 = SquareTextTo.Text;
            double num1 = 0;
            double.TryParse(_name, out num1);
            double num2 = 0;
            double.TryParse(_name2, out num2);
            var c = ComboStatus.SelectedItem;
            var a = ComboFloor.SelectedItem;


            // ТУТ ДОЛЖНЫ БЫТЬ IF НА ВСЕ УСЛОВИЯ НИЖЕ
            try
            {
                if (c == null)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == idShop
            && b.Value_Adder_Factor > 0.1 && b.Floor.ToString() == a.ToString()).ToList();
                if (a == null && num1.ToString() != null || num2.ToString() != null)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == idShop
                    && b.Value_Adder_Factor > 0.1 && b.Status.ToString() == c.ToString()).ToList();
                if (num1 == 0 && num2 == 0)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.SC_ID == idShop && b.Floor.ToString() == a.ToString()
                    && b.Value_Adder_Factor > 0.1 && b.Status.ToString() == c.ToString()).ToList();
                if (num1.ToString() == null && num2.ToString() == null)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.SC_ID == idShop && b.Floor.ToString() == a.ToString()
                    && b.Value_Adder_Factor > 0.1 && b.Status.ToString() == c.ToString()).ToList();
                if (num1 != 0 || num2 != 0 && c != null && a != null)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == idShop && b.Floor.ToString() == a.ToString()
                    && b.Value_Adder_Factor > 0.1 && b.Status.ToString() == c.ToString()).ToList();
                if (a == null && c != null && num1.ToString() != null && num2.ToString() != null)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == idShop
                    && b.Value_Adder_Factor > 0.1 && b.Status.ToString() == c.ToString()).ToList();
                if (a == null && num1 != 0 && num2 != 0)
                    DGridPavilions.ItemsSource = Ses1Entities.GetContext().Pavilions.Where(b => b.Square > num1 && b.Square < num2 && b.SC_ID == idShop
                    && b.Value_Adder_Factor > 0.1 && b.Status.ToString() == c.ToString()).ToList();

            }
            catch
            {
            }
        }
    }
}
