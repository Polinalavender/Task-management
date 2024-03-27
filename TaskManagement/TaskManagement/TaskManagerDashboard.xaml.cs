using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement
{
    public partial class TaskManagerDashboard : ContentPage, INotifyPropertyChanged
    {
        public TaskManagerDashboard()
        {
            InitializeComponent();
            BindingContext = this;
        }
    }
}
