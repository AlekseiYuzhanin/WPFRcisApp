using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.ViewModels.BaseViewModel;
using WpfApp4.Commands;
using WpfApp4.Models;
using System.Windows.Controls;
using WpfApp4.Views.Windows;
using System.Windows;

namespace WpfApp4.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private User _user;
        private Command _authCommand;
        private Command _openRegistrationWindow;
        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        public MainWindowViewModel()
        {
            _user = new User();
        }

        public void CloseWindow()
        {
            foreach (var win in App.Current.Windows)
            {
                if (win is MainWindow mainWindow)
                {
                    mainWindow.Close();
                }
            }
        }
        public Command AuthCommand
        {
            get
            {
                return _authCommand ?? (_authCommand = new Command(x =>
                {
                    var password = (x as PasswordBox).Password;
                    _user.Password = password;

                    var user = Connection.GetContext().Users.SingleOrDefault(x => x.Login == _user.Login && x.Password == _user.Password);
                    if (user != null)
                    {
                        Connection.userAuth = user;
                        new MenuWindow().Show();
                        CloseWindow();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка авторизации!");
                    }
                    }, x =>
                    {
                        return !string.IsNullOrWhiteSpace(_user.Login);
                    }));
            }
        }

        public Command OpenRegistrationWindow
        {
            get
            {
                return _openRegistrationWindow ?? (_openRegistrationWindow = new Command(x =>
                {
                    new RegistrationWindow().Show();
                   
                }));
                
            }
        }
    }
}
