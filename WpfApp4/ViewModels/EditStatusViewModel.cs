using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.ViewModels.BaseViewModel;
using WpfApp4.Views.Windows;

namespace WpfApp4.ViewModels
{
    public class EditStatusViewModel : ViewModel
    {
        private ObservableCollection<Status> _status;
        private Command _setStatus;
        public ObservableCollection<Status> Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged();
            }
        }

        private Status _selectedStatus;

        public Status SelectedStatus
        {
            get { return _selectedStatus; }
            set
            {
                _selectedStatus = value;
                OnPropertyChanged();
            }
        }
        public Task Task { get; set; }
        public EditStatusViewModel(Task task)
        {
            Status = new ObservableCollection<Status>(Connection.GetContext().Statuses);
            SelectedStatus = task.Status;
            Task = task;
           
        }

        public Command SetStatus
        {
            get
            {
                return _setStatus ?? (_setStatus = new Command(x =>
                {
                    Task.StatusId = SelectedStatus.StatusId;
                    Connection.GetContext().SaveChanges();
                    Application.Current.Windows.OfType<EditStatusWindow>().First().Close();
                }));
            }
        }

    }
}
