using System;
using System.Collections.Generic;
using WpfApp4.ViewModels.BaseViewModel;
namespace WpfApp4
{
    public partial class User : ViewModel
    {
        private int _userId;
        private string _surname;
        private string _name;
        private string _patronymic;
        private string _login;
        private string _password;
        private string _numberPhone;

        public User()
        {
            TaskTaskMakers = new HashSet<Task>();
            TaskTaskers = new HashSet<Task>();
        }

        public int UserId
        {
            get { return _userId; }
            set { _userId = value; OnPropertyChanged();}
        }
        
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; OnPropertyChanged();}
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged();}
        }
        
        public string Patronymic
        {
            get { return _patronymic; }
            set { _patronymic = value; OnPropertyChanged();}
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged();}
        }
        public string Password
        {
            get { return _password; }
            set { _password = value; OnPropertyChanged();}
        }
        public string NumberPhone
        {
            get { return _numberPhone; }
            set { _numberPhone = value; OnPropertyChanged();}
        }
       

        public virtual ICollection<Task> TaskTaskMakers { get; set; }
        public virtual ICollection<Task> TaskTaskers { get; set; }
    }
}
