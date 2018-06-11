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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private ObservableCollection<string> myVar = new ObservableCollection<string>();

        public ObservableCollection<string> MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        public Window1()
        {
            InitializeComponent();
            this.DataContext = this;
            List<User> users = new List<User>();
            users.Add(new User() { Id = 1, Name = "John Doe", Birthday = new DateTime(1971, 7, 23) });
            users.Add(new User() { Id = 2, Name = "Jane Doe", Birthday = new DateTime(1974, 1, 17) });
            users.Add(new User() { Id = 3, Name = "Sammy Doe", Birthday = new DateTime(1991, 9, 2) });
            MyProperty.Add("aaa");
            MyProperty.Add("aaa");
            MyProperty.Add("aaaa");
            MyProperty.Add("aaaaaa");
            MyProperty.Add("1");
            MyProperty.Add("1");
            //dgUsers.ItemsSource = users;
        }
    }

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public string Details
        {
            get
            {
                return String.Format("{0} was born on {1} and this is a long description of the person.", this.Name, this.Birthday.ToLongDateString());
            }
        }
    }
}
