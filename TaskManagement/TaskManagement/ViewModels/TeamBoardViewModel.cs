using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using DataAccessLayer.Models;
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
    public partial class TeamBoardViewModel : ObservableObject
    {
        private readonly TeamRepository teamRepository;
        IPopupNavigation popupNavigation;
        UserRepository userRepository;
        TaskRepository taskRepository;

        [ObservableProperty]
        ContentView currentView = new ContentView();

        [ObservableProperty]
        bool usersListVisibility = true;

        [ObservableProperty]
        bool teamsListVisibility = true;

        [ObservableProperty]
        int countUser = 0;

        [ObservableProperty]
        string teamsLabel = "My Teams";

        
        public ObservableCollection<Team> Teams { get; set; } = [];
        public ObservableCollection<UserTeam> TeamsUsers { get; set; } = [];

        private int? taskId;
        [ObservableProperty]
        private int? selectedTeamId;

        public TeamBoardViewModel()
        {
            this.popupNavigation = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupNavigation>();
            this.userRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<UserRepository>();
            this.taskRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TaskRepository>();
            this.teamRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TeamRepository>();
            LoadDataAsync();
            WeakReferenceMessenger.Default.Register<SendItemMessage<TaskModel>>(this, async (r, m) =>
            {
                await LoadDataAsync();
            });

            WeakReferenceMessenger.Default.Register<SendItemMessage<int>>(this, async (r, m) =>
            {
                taskId = m.Value;
            });

            WeakReferenceMessenger.Default.Register<SendItemMessage<string>>(this, async (r, m) =>
            {
                if (m.Value == "NewTeam")
                    await this.LoadTeams();
            });
        }

        async Task LoadDataAsync()
        {
            await this.LoadTeams();
            //await this.popupNavigation.PushAsync(new LoaderPopup("Loading tasks"));
            CurrentView.Content = new TaskContentView(SelectedTeamId);
            //await this.popupNavigation.PushAsync(new LoaderPopup("Loading tasks"));
        }

        async Task LoadTeams()
        {
            Teams.Clear();
            if (SelectedTeamId != null)
            {
                var team = await this.teamRepository.GetTeamByIdAsync(SelectedTeamId ?? default);
                if (team != null)
                {
                    Teams.Add(team);
                }
            }
            else
            {
                var teams = await this.teamRepository.GetAllTeamsAsync();
                foreach (var t in teams)
                {
                    Teams.Add(t);
                }
            }
        }

        [RelayCommand]
        private async Task LogoutAsync()
        {
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync("//login");
        }

        [RelayCommand]
        private async Task LoadCreateTaskScreen()
        {
            SelectedTeamId = null;
            currentView.Content = new CreateTaskView();
        }

        [RelayCommand]
        private async Task CreateTeam()
        {
            SelectedTeamId = null;
            currentView.Content = new CreateTeamView();
        }

        [RelayCommand]
        private async Task LoadBoards()
        {
            await LoadDataAsync();
        }

        [RelayCommand]
        private async Task RemoveTeamFilter()
        {
            TeamsUsers = [];
            UsersListVisibility = false;
            TeamsListVisibility = true;
            TeamsLabel = "My Teams";
            SelectedTeamId = null;
            taskId = null;
            await this.LoadDataAsync();
        }

        [RelayCommand]
        private async Task TeamSelect(object e)
        {
            TeamsUsers = [];
            if (e is Team teamObj)
            {
                var allUsersInTeam = await this.teamRepository.GetTeamByIdAsync(teamObj.TeamId);
                foreach (var g in allUsersInTeam?.UserTeam)
                {
                    TeamsUsers.Add(g);
                }
                UsersListVisibility = TeamsUsers.Count > 0;
                TeamsListVisibility = !UsersListVisibility;
                TeamsLabel = teamObj.Name;
                SelectedTeamId = teamObj.TeamId;
                await this.LoadDataAsync();
            }
        }

        [RelayCommand]
        private async Task HandleDelete()
        {
            if (taskId != null)
            {
                await this.taskRepository.DeleteTaskAsync(taskId ?? default);
                await LoadDataAsync();
                taskId = null;
            }
        }

        [RelayCommand]
        private async Task SettingsPage()
        {
            SelectedTeamId = null;
            currentView.Content = new SettingsView();
        }

    }
}
