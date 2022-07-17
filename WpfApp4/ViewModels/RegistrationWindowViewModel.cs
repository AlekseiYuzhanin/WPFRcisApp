using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.ViewModels.BaseViewModel;
using WpfApp4.Views.Windows;
using WpfApp4.Models;
using System.Windows;

namespace WpfApp4.ViewModels
{
    public class RegistrationWindowViewModel : ViewModel
    {
        private User _user;
        private Command _signUpCommand;

        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        public RegistrationWindowViewModel()
        {
            _user = new User();
        }

        public void CloseWindow()
        {
            foreach (var win in App.Current.Windows)
            {
                if (win is RegistrationWindow registrationWindow)
                {
                    registrationWindow.Close();
                }
            }
        }

        public Command SignUpCommand
        {
            get
            {
                return _signUpCommand ?? (_signUpCommand = new Command(o =>
                {
                    UserTaskBdContext context = new UserTaskBdContext();
                    context.Add(User);
                    context.SaveChanges();
                    CloseWindow();
                }));
            }
        }
    }
}
