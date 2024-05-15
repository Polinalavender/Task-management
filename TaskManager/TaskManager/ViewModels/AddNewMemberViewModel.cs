using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Models;

namespace TaskManager.ViewModels
{
    public partial class AddNewMemberViewModel : ObservableObject
    {
        public ObservableCollection<User> Users { get; } = [];
        [ObservableProperty]
        User assignedUser;
        UserRepository userRepository;
        public AddNewMemberViewModel()
        {
            this.userRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<UserRepository>();
            LoadDataAsync();
        }

        async Task LoadDataAsync()
        {
            var allusers = await this.userRepository.GetUsers();
            foreach (var user in allusers)
            {
                Users.Add(user);
            }
        }

        [RelayCommand]
        private async Task AddMember(object popup)
        {
            if (popup != null)
            {
                var pop = (Popup)popup;
                pop.Close(assignedUser);
            }
        }


    }
}
