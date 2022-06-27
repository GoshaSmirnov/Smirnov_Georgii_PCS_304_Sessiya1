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

    public partial class RentalPage : Page
    {
        private Pavilions pavilion;
        int Tent_ID;
        public List<Tenant> tenantsCollection { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public RentalPage(Pavilions selectedPavilon)
        
            
        {
            InitializeComponent();
            Start = DateTime.Today;
            Stop = DateTime.Today;
            tenantsCollection = Ses1Entities.GetContext().Tenant.ToList();
            ComboTenants.ItemsSource = Ses1Entities.GetContext().Tenant.Select(x => x.Название).Distinct().ToList();
            pavilion = selectedPavilon;
            DataContext = pavilion;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Start <= Stop && Start >= DateTime.Today)
            {
                
                Start = StartPick.SelectedDate.GetValueOrDefault();
                bool stat = Start == DateTime.Today;
                Stop = EndPick.SelectedDate.GetValueOrDefault();
                Tent_ID = Ses1Entities.GetContext().Tenant.Where(x => x.Название == ComboTenants.Text).Select(x => x.Код_Арендатора).FirstOrDefault();

                try
                {
                    Ses1Entities.GetContext().RentPr(Tent_ID, autorization.Emp_ID, pavilion.SC_ID, pavilion.Pavilion_Number, Start, Stop);
                    MessageBox.Show(stat ? "Арендовано" : "Забронировано");
                }
                catch 
                {
                    MessageBox.Show("Вероятно вы пытаетесь арендовать уже арендованный павильон");
                }
            }
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            RentalPageFrame.Content = new PavilionsPage(null);
        }
    }
}
