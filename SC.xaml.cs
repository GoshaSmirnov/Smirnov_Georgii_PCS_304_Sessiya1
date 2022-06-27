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
    /// Логика взаимодействия для SC.xaml
    /// </summary>
    public partial class SC : Page
    {
        public SC()
        {
            InitializeComponent();
            DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(x => x.Status != "Удален").ToList();
            ComboCity.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Select(x => x.Sity).Distinct().ToList();
            ComboStatus.Items.Add("Планируемые");
            ComboStatus.Items.Add("Строющиеся");
            ComboStatus.Items.Add("Построенные");
        }
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridShopping.SelectedItems.Cast<Shop_Centers>().FirstOrDefault();
            SC_Frame.Content = new ADD_SC(upd);
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var ShoppingsForRemoving = DGridShopping.SelectedItems.Cast<Shop_Centers>().ToList();
            if (MessageBox.Show("Вы точно хотите Удалить эти(-от) ТЦ", "Внимание!", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    ShoppingsForRemoving.ForEach(x => x.Status = "Удален");
                    Ses1Entities.GetContext().SaveChanges();
                    MessageBox.Show("Записи удалены!");
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(x => x.Status != "Удален").ToList();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void Combo_Changed(object sender, SelectionChangedEventArgs e)
        {
            if (ComboStatus.Text != "" && ComboCity.Text == "")
            {
                if (ComboStatus.SelectedIndex == 0) //первый элемент списка
                {
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(x => x.Status == "План").ToList();
                }
                if (ComboStatus.SelectedIndex == 1)//второй элемент списка
                {
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(x => x.Status == "Строительсто").ToList();
                }
                if (ComboStatus.SelectedIndex == 2)//третий элемент списка
                {
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(x => x.Status == "Реализация").ToList();
                }
            }
            else if (ComboStatus.Text == "" && ComboCity.Text != "")
            {

                DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(b => b.Sity == ComboCity.SelectedItem && b.Status != "Удален").ToList();
            }
            else if (ComboCity.Text != "" && ComboStatus.Text != "")
            {
                var c = ComboCity.SelectedItem;
                if (ComboStatus.SelectedIndex == 0) //первый элемент списка
                {
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(b => b.Sity == c.ToString() && b.Status == "План").ToList();
                }
                if (ComboStatus.SelectedIndex == 1)//второй элемент списка
                {
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(b => b.Sity == c.ToString() && b.Status == "Строительство").ToList();
                }
                if (ComboStatus.SelectedIndex == 2)//третий элемент списка
                {
                    DGridShopping.ItemsSource = Ses1Entities.GetContext().Shop_Centers.Where(b => b.Sity == c.ToString() && b.Status == "Реализация").ToList();
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            SC_Frame.Content = new Employee_Page(null);
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            SC_Frame.Content = new SC_ADD(null);
        }

        private void BtnPavilions_Click(object sender, RoutedEventArgs e)
        {
            var upd = DGridShopping.SelectedItems.Cast<Shop_Centers>().FirstOrDefault();
            SC_Frame.Content = new PavilionsPage(upd);
        }
    }
}