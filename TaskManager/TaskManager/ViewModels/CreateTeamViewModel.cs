using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Models;
using TaskManager.Popups;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using System.Threading;
using Mopups.Interfaces;
using TaskManager.Views;
using CommunityToolkit.Mvvm.Messaging;
using TaskManager.Messenger;

namespace TaskManager.ViewModels
{
    partial class CreateTeamViewModel : ObservableObject
    {
        public ObservableCollection<Team> Teams { get; set; } = [];
        private readonly TeamRepository teamRepository;
        private readonly UserRepository userRepository;
        private readonly IPopupNavigation? popupNavigation;

        public CreateTeamViewModel()
        {
            teamRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<TeamRepository>();
            userRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<UserRepository>();
            this.popupNavigation = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupNavigation>();
            LoadData();
        }

        private async Task LoadData()
        {
            Teams.Clear();
            await this.popupNavigation.PushAsync(new LoaderPopup("Loading teams"));
            //Teams = [];
            var allteams = await this.teamRepository.GetAllTeamsAsync();
            foreach (var t in allteams)
            {
                Teams.Add(t);
            }
            await this.popupNavigation.PopAsync();

        }

        [RelayCommand]
        private async Task AddMember(object e)
        {
            var result = await Application.Current?.MainPage.ShowPopupAsync(new AddNewMemberPopup());
            if (result is User user)
            {
                if (user != null)
                {

                    var team = await this.teamRepository.GetTeamByIdAsync(Convert.ToInt32(e));
                    if (team?.UserTeam?.FirstOrDefault(a => a.UserId == user.UserId) == null)
                    {
                        var userTeam = new UserTeam() { TeamId = Convert.ToInt32(e), UserId = user.UserId };
                        await this.teamRepository.AddTeamUserAsync(userTeam);

                        await this.LoadData();
                        WeakReferenceMessenger.Default.Send<SendItemMessage<string>>(new SendItemMessage<string>("NewTeam"));
                    }
                    else
                    {
                        await Toast.Make("User already added to this team",
                                  ToastDuration.Long,
                        16)
                            .Show();
                    }
                }
            }
        }

        [RelayCommand]
        private async Task AddTeam()
        {
            string teamName = await Application.Current.MainPage.DisplayPromptAsync("Add Team", "Enter team name", "OK", "Cancel", keyboard: Keyboard.Text);
            if (!string.IsNullOrEmpty(teamName))
            {
                var newTeam = new Team() { Name = teamName };
                await this.teamRepository.AddTeamAsync(newTeam);
                Teams.Add(newTeam);
                WeakReferenceMessenger.Default.Send<SendItemMessage<string>>(new SendItemMessage<string>("NewTeam"));
            }
        }

        [RelayCommand]
        private async Task DeleteMember(object e)
        {
            if (e is Array arr)
            {
                var team = Teams.FirstOrDefault(a => a.TeamId == (int)arr.GetValue(0));
                var userTeam = team.UserTeam.FirstOrDefault(a => a.UserId == (int)arr.GetValue(1) && a.TeamId == (int)arr.GetValue(0));
                team.UserTeam.Remove(team.UserTeam.FirstOrDefault(a => a.UserId == (int)arr.GetValue(1) && a.TeamId == (int)arr.GetValue(0)));
                await this.teamRepository.DeleteUserTeam(userTeam);
                WeakReferenceMessenger.Default.Send<SendItemMessage<string>>(new SendItemMessage<string>("NewTeam"));
                //await this.LoadData();
            }

        }
    }
}
