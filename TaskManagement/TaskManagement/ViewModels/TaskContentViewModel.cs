using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataAccessLayer.Repositories;
using Mopups.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Messenger;
using TaskManagement.Models;
using TaskManagement.Views;

namespace TaskManagement.ViewModels
{
    public partial class TaskContentViewModel:ObservableObject
    {
        TaskRepository taskRepository;
        private TeamRepository? teamRepository;
        IPopupNavigation popupNavigation;

        public ObservableCollection<TaskModel> ToDoTasks { get; } = new();
        public ObservableCollection<TaskModel> InProcessTasks { get; } = new();
        public ObservableCollection<TaskModel> DoneTasks { get; } = new();

        private List<TaskModel> _tasks = [];
        private TaskModel _task;

        [ObservableProperty]
        string pageTitle;


        public TaskContentViewModel(int? teamId)
        {
            this.taskRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TaskRepository>();
            this.teamRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TeamRepository>();
            this.popupNavigation = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupNavigation>();
            LoadDataAsync(teamId);
        }

        async Task LoadDataAsync(int? teamId)
        {
            await this.popupNavigation.PushAsync(new LoaderPopup("Loading tasks"));
            if (teamId == null)
            {
                _tasks = await this.taskRepository.GetAllTasksAsync();
                PageTitle = string.Empty;
            }
            else
            {
                var team = await this.teamRepository.GetTeamByIdAsync(teamId ?? default);
                var userIds = team.UserTeam.Select(a => (int?)a.UserId).ToArray();
                _tasks = await this.taskRepository.GetTeamsTasksAsync(userIds);
                PageTitle = $"{team.Name}'s Tasks";
            }
           
            
            foreach (var task in _tasks)
            {
                switch (task.TaskStatus)
                {
                    case TaskEnum.Done:
                        DoneTasks.Add(task); break;
                    case TaskEnum.ToDo:
                        ToDoTasks.Add(task); break;
                    case TaskEnum.InProgress:
                        InProcessTasks.Add(task); break;
                }
            }
            await this.popupNavigation.PopAsync();
        }

        [RelayCommand]
        private async Task HandleDrop(string e)//0,1,2
        {
            var status = (TaskEnum)Convert.ToInt64(e);
            if (_task == null || ( _task != null && _task.TaskStatus == status))
                return;

            var currentStatus = _task.TaskStatus;

            _task.TaskStatus = status;
            switch (status)
            {
                case TaskEnum.ToDo:
                    ToDoTasks.Add(_task);
                    break;
                case TaskEnum.InProgress:
                    InProcessTasks.Add(_task);
                    break;
                case TaskEnum.Done:
                    DoneTasks.Add(_task);
                    break;
            }

            switch (currentStatus)
            {
                case TaskEnum.ToDo:
                    ToDoTasks.Remove(_task);
                    break;
                case TaskEnum.InProgress:
                    InProcessTasks.Remove(_task);
                    break;
                case TaskEnum.Done:
                    DoneTasks.Remove(_task);
                    break;
            }
            var task = await this.taskRepository.GetTaskByIdAsync(_task.Id);
            task.TaskStatus = status;
            await this.taskRepository.UpdateTaskAsync(task);
        }

        [RelayCommand]
        private async Task DragStarting(TaskModel taskModel)
        {
            _task = taskModel;
            WeakReferenceMessenger.Default.Send(new SendItemMessage<int>(taskModel.Id));
        }

        [RelayCommand]
        private async Task DropComplete(TaskModel taskModel)
        {

        }
    }
}
