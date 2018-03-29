using Caliburn.Micro;
using CBMicro.Models;
using CBMicro.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CBMicro.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        public ShellViewModel()
        {
            Persons.Add(new Person { FirstName = "Alfred", LastName = "Manoj" });
            Persons.Add(new Person { FirstName = "Arvin", LastName = "Manoj" });
            Persons.Add(new Person { FirstName = "Manoj", LastName = "Thomas" });
            IsExtended = true;
        }


        private bool _isExtended;

        private string _firstName;

        private string _lastName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                NotifyOfPropertyChange(() => FirstName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public bool IsExtended
        {
            get { return _isExtended; }
            set
            {
                _isExtended = value;
                NotifyOfPropertyChange(() => IsExtended);  
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                NotifyOfPropertyChange(() => LastName);
                NotifyOfPropertyChange(() => FullName);
            }
        }

        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        private ObservableCollection<Person> _persons = new ObservableCollection<Person>();

        public ObservableCollection<Person> Persons
        {
            get { return _persons; }
            set { _persons = value; }
        }

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set { _selectedPerson = value; NotifyOfPropertyChange(() => SelectedPerson); }
        }

        public bool CanAddUser(string firstName, string lastName)
        {
            return !string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName);
        }

        public void AddUser(string firstName, string lastName)
        {
            Persons.Add(new Person { FirstName = FirstName, LastName = LastName });
        }
        
        public void FirstChild()
        {
            ActivateItem(new FirstChildViewModel());
        }

        public void RemoveChild()
        {
            var childrens = GetChildren();
            foreach (var item in childrens)
            {
                DeactivateItem(item, true);
            }
        }

        public void SecondChild()
        {
            ActivateItem(new SecondChildViewModel());
        }

        public void SayHello(object obj)
        {

        }

        public void ClickThisMethod(object obj,object value,int valueName) //supports automatic type conversion
        {                                                  

        }

        public void LostFocusMethod()
        {

        }
    }
}
