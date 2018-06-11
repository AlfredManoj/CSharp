using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ComboBoxControl
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        private ObservableCollection<City> cities = new ObservableCollection<City>();

        public ObservableCollection<City> Cities
        {
            get
            {
                return cities;
            }
            set
            {
                cities = value;
            }
        }

        public Window2()
        {
            InitializeComponent();
            cities.Add(new City() { Name = "Boston", State = "MA", Population = 3000000 });
            cities.Add(new City() { Name = "Los Angeles", State = "CA", Population = 7000000 });
            cities.Add(new City() { Name = "Frederick", State = "MD", Population = 65000 });
            cities.Add(new City() { Name = "Houston", State = "TX", Population = 5000000 });
            cbCities.DataContext = cities;
        }


    }

    public class City
    {
        public string State { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
    }
}
