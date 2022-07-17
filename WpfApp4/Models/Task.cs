using System;
using System.Collections.Generic;
using WpfApp4.Models;
using WpfApp4.ViewModels.BaseViewModel;
namespace WpfApp4
{
    public partial class Task : ViewModel
    {
        private int _taskId;
        private string _title;
        private string _description;
        private DateTime _dateOfPublication;
        private int _taskerId;
        private int? _taskMakerId;
        private int _statusId;

        public int TaskId
        {
            get { return _taskId; }
            set { _taskId = value;OnPropertyChanged(); }
        }

        public string Title
        {
            get { return _title; }
            set { _title = value;OnPropertyChanged();}
        }
        
        public string Description
        {
            get { return _description; }
            set { _description = value;OnPropertyChanged();}

        }
       public DateTime DateOfPublication
        {
            get { return _dateOfPublication; }
            set { _dateOfPublication = value;OnPropertyChanged();}
        }
        
        public int TaskerId
        {
            get { return _taskerId; }
            set { _taskerId = value;OnPropertyChanged();}
        }
       
        public int? TaskMakerId
        {
            get { return _taskMakerId; }
            set { _taskMakerId = value;OnPropertyChanged();}
        }
        
        public int StatusId
        {
            get { return _statusId; }
            set { _statusId = value;OnPropertyChanged(); Connection.GetContext().SaveChanges(); }
        }

        public virtual Status Status { get; set; }
        public virtual User? TaskMaker { get; set; }
        public virtual User Tasker { get; set; } = null!;
    }
}
