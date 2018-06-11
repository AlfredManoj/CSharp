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

namespace ComboBoxControl
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<ComboboxData> cmb = new List<ComboboxData>();
            cmb.Add(new ComboboxData("1.", "Tidle", "~"));
            cmb.Add(new ComboboxData("2.", "Exclamation", "!"));
            cmb.Add(new ComboboxData("3.", "Ampersat", "@"));
            cmb.Add(new ComboboxData("4.", "Ampersand", "&"));
            cmb.Add(new ComboboxData("5.", "Dollar", "$"));
            //Combobox.ItemsSource = cmb;
            Combobox1.ItemsSource = cmb;
        }
    }

    public class ComboboxData
    {
        public string index { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public ComboboxData(string index, string name, string symbol)
        {
            this.index = index;
            this.name = name;
            this.symbol = symbol;
        }
    }
}
