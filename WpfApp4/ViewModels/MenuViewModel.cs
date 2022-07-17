using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp4.Commands;
using WpfApp4.ViewModels.BaseViewModel;
using WpfApp4.Models;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using WpfApp4.Views;
using WpfApp4.Views.Windows;
using System.Windows;
using WinForms = System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;

namespace WpfApp4.ViewModels
{
    internal class MenuViewModel : ViewModel
    {
        private ObservableCollection<User> _users;
        private Command _exit;
        private User _user = Connection.userAuth;
        private ObservableCollection<Task> _avalibaleTask;
        private Command _openTaskTake;
        private Command _setStatusCommand;
        private Task _task;
        private Task _selectedTask;
        private Command _createReport;

        public Task SelectedTask
        {
            get { return _selectedTask; }
            set
            {
                _selectedTask = value;
                OnPropertyChanged();
            }
        }

        public Task CurrentTask
        {
            get { return _task; }
            set
            {
                _task = value;
                OnPropertyChanged();
            }
        }
        public User User
        {
            get => _user;
            set => Set(ref _user, value);
        }

        private void CloseWindow()
        {
            foreach(var item in App.Current.Windows)
            {
                if(item is MenuWindow menuWindow)
                {
                    menuWindow.Close();
                }  
                    
            }
        }

        private ObservableCollection<Task> tasks = new ObservableCollection<Task>(Connection.GetContext().Tasks.Where(x => x.TaskMakerId == Connection.userAuth.UserId).Include(x => x.TaskMaker).Include(x => x.Tasker).Include(w => w.Status));
        public ObservableCollection<Task> Tasks 
        {
            get { return tasks; }
            set 
            { 
                tasks = value; 
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Task> AvalibaleTasks
        {
            get { return _avalibaleTask; }
            set { _avalibaleTask = value; OnPropertyChanged();}
        }

        public ObservableCollection<User> Users
        {
            get { return _users; }
            set { _users = value; OnPropertyChanged();}
        }

        private ObservableCollection<Task> _allTasks;

        public ObservableCollection<Task> AllTasks
        {
            get { return _allTasks; }
            set { _allTasks = value; OnPropertyChanged();}
        }

        public MenuViewModel()
        {
            _users = new ObservableCollection<User>(Connection.GetContext().Users);
            AvalibaleTasks = new ObservableCollection<Task>(Connection.GetContext().Tasks.Where(x=>x.StatusId == 2 && x.TaskMakerId == null));
            AllTasks = new ObservableCollection<Task>(Connection.GetContext().Tasks);
        }

        public Command SetStatusCommand
        {
            get
            {
                return _setStatusCommand ?? (_setStatusCommand = new Command(x =>
                {
                    new EditStatusWindow(CurrentTask).ShowDialog();
                    Tasks = new ObservableCollection<Task>(Connection.GetContext().Tasks.Where(x => x.TaskMakerId == Connection.userAuth.UserId).Include(x => x.TaskMaker).Include(x => x.Tasker).Include(w => w.Status));
                    //AvalibaleTasks = new ObservableCollection<Task>(Connection.GetContext().Tasks.Where(x => x.StatusId == 2 && x.TaskMakerId == null));
                    //Connection.GetContext().SaveChanges();
                }));
            }
        }

        public Command OpenTaskTake
        {
            get
            {
                return _openTaskTake ?? (_openTaskTake = new Command(x =>
                {
                    new TaskTake(SelectedTask).ShowDialog();
                    CloseWindow();
                }));
            }
        }
        

        public Command Exit
        {
            get
            {
                return _exit ?? (_exit = new Command( x=>
                {

                    new MainWindow().Show();
                    CloseWindow();

                    }));
            }
        }
        private void SaveToWord()
        {
            object start = 0;
            object end = 0;
            //Word.Application wordApp = new Word.Application();
            Word.Document wordDoc = new Word.Document();
            Word.Range tableLocation = wordDoc.Range(ref start, ref end);
            //tableLocation.SetRange(tableLocation.End, tableLocation.End);
            

            wordDoc.Tables.Add(tableLocation, AllTasks.Count, 7);
            var table = wordDoc.Tables[1];
            table.AllowAutoFit = true;
            table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
            table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;


            Word.Cell cell = table.Cell(1, 1);
            //cell.Range.Text = "aaaaaaaaaaaaaaaaaaaaa";
            //cell.Range.Text = "aaa";
                table.Cell(1, 1).Range.Text = "Id";
                table.Cell(1, 2).Range.Text = "Title";
                table.Cell(1, 3).Range.Text = "Description";
                table.Cell(1, 4).Range.Text = "Publication date";
                table.Cell(1, 5).Range.Text = "Creator user";
                table.Cell(1, 6).Range.Text = "Accepted user";
                table.Cell(1, 7).Range.Text = "Status";
            int rowWord = 2;

            for (int i = 0; i < AllTasks.Count; i++)
            {
                Task taskForAdd = AllTasks.ElementAt(i);

                table.Cell(rowWord, 1).Range.Text = taskForAdd.TaskId.ToString();
                table.Cell(rowWord, 2).Range.Text = taskForAdd.Title;
                table.Cell(rowWord, 3).Range.Text = taskForAdd.Description;
                table.Cell(rowWord, 4).Range.Text = taskForAdd.DateOfPublication.ToString();
                table.Cell(rowWord, 5).Range.Text = taskForAdd.Tasker.Surname + " " + taskForAdd.Tasker.Name + " " + taskForAdd.Tasker.Patronymic;
                if (taskForAdd.TaskMaker != null)
                    table.Cell(rowWord, 6).Range.Text = taskForAdd.TaskMaker.Surname + " " + taskForAdd.TaskMaker.Name + " " + taskForAdd.TaskMaker.Patronymic;
                table.Cell(rowWord, 7).Range.Text = taskForAdd.Status.CurrentStatus;

                ++rowWord;
            }



            string fileName = "\\Task_report_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss") + ".docx";
            wordDoc.SaveAs($"{_savingDirectory}{fileName}");
        }

        public Command CreateReport
        {
            get
            {
                return _createReport ?? (_createReport = new Command(x =>
                {
                    SaveToWord();
                }));
            }
        }
        private string _savingDirectory;
        private Command _chooseDirectory;
        public Command ChooseDirectory
        {
            get
            {
                return _chooseDirectory ?? (_chooseDirectory = new Command(x =>
                {
                    var dialog = new WinForms.FolderBrowserDialog();
                    dialog.ShowDialog();
                    _savingDirectory = dialog.SelectedPath;

                }));
            }
        }
    }
}
