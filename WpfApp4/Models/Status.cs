using System;
using System.Collections.Generic;
using WpfApp4.ViewModels.BaseViewModel;

namespace WpfApp4
{
    public partial class Status : ViewModel
    {
        private int _statusId;
        private string _currentStatus;

        public Status()
        {
            Tasks = new HashSet<Task>();
        }

        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value; OnPropertyChanged("StatusId"); }
        }

        public string CurrentStatus
        {
            get { return _currentStatus; }
            set { _currentStatus = value; OnPropertyChanged("CurrentStatus"); }
        }


        public virtual ICollection<Task> Tasks { get; set; }
    }
}
