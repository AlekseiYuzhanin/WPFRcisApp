using System;
using System.Collections.Generic;
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
using WpfApp4.ViewModels;

namespace WpfApp4.Views.Windows
{
    /// <summary>
    /// Логика взаимодействия для TaskTake.xaml
    /// </summary>
    public partial class TaskTake : Window
    {
        public TaskTake(Task selectedTask)
        {
            InitializeComponent();
            GridTask.DataContext = new TaskTakeViewModel(selectedTask);
        }

       
    }
}
