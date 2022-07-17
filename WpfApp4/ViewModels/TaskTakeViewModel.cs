using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp4.Commands;
using WpfApp4.Models;
using WpfApp4.ViewModels.BaseViewModel;
using WpfApp4.Views.Windows;

namespace WpfApp4.ViewModels
{
    public class TaskTakeViewModel : ViewModel
    {
        private Command _takeTaskCommand;
        public Task TaskForEdit { get; set; }
        public TaskTakeViewModel(Task selectedTask)
        {
            TaskForEdit = selectedTask;
        }

        private void CloseWindow()
        {
            foreach(var item in App.Current.Windows)
            {
                if(item is TaskTake taskTake)
                {
                    taskTake.Close();
                }  
                    
            }
        }
        public ICommand TakeTaskCommand => _takeTaskCommand ??= new Command(TakeTask);

        private void TakeTask(object commandParameter)
        {
            Connection.GetContext().Tasks.Where(x => x.TaskId == TaskForEdit.TaskId).First().StatusId = 3;
            Connection.GetContext().Tasks.Where(x => x.TaskId == TaskForEdit.TaskId).First().TaskMakerId = Connection.userAuth.UserId;
            Connection.GetContext().SaveChanges();
            new MenuWindow().Show();
            CloseWindow();
        }
        
    }
}
