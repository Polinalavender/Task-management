using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
    public partial class CreateTaskViewModel: ObservableObject
    {
        public ObservableCollection<User> Users { get; } = new();

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        DateTime _deadline = DateTime.Now;

        [ObservableProperty]
        int? _assignedUserID;

        [ObservableProperty]
        string _taskDescription;

        [ObservableProperty]
        User _assignedUser;

        IPopupNavigation popupNavigation;
        UserRepository userRepository;
        TaskRepository taskRepository;

        IAsyncRelayCommand _createTaskCommand;
        public IAsyncRelayCommand CreateTaskCommand => _createTaskCommand ??= new AsyncRelayCommand(CreateTask);
        public CreateTaskViewModel()
        {
            this.popupNavigation = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupNavigation>();
            this.userRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<UserRepository>();
            this.taskRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TaskRepository>();
            LoadDataAsync();
        }

        async Task LoadDataAsync()
        {
            var allusers = await this.userRepository.GetUsers();
            foreach(var user in allusers)
            {
                Users.Add(user);
            }
        }
        private async Task CreateTask()
        {
            await this.popupNavigation.PushAsync(new LoaderPopup("Loader..."), true);
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(TaskDescription) || AssignedUser == null)
            {
                await this.popupNavigation.PopAsync();
                Application.Current.MainPage.DisplayAlert("Error", "Username or password is empty!", "Ok");
                return;
            }
            var task = new TaskModel { AssignedUserID = AssignedUser.UserId, Deadline = Deadline, Name = Name, TaskDescription = TaskDescription };
            await this.taskRepository.CreateTaskAsync(task);
            await this.popupNavigation.PopAsync();
            WeakReferenceMessenger.Default.Send(new SendItemMessage<TaskModel>(task));
        }
    }
}
