using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Services;
using Mopups.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Views;
using Mopups.PreBaked.Services;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TaskManager.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        IPopupNavigation popupNavigation;
        UserRepository userRepository;
        public LoginViewModel()
        {
            this.popupNavigation = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupNavigation>();
            this.userRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<UserRepository>(); ;
        }
        [ObservableProperty]
        string _username;

        [ObservableProperty]
        string _password;

        IAsyncRelayCommand _loginCommand;
        public IAsyncRelayCommand LoginCommand => _loginCommand ??= new AsyncRelayCommand(LoginAsync);

        IAsyncRelayCommand _register;
        public IAsyncRelayCommand RegisterCommand => _register ??= new AsyncRelayCommand(RegisterAsync);

        async Task LoginAsync()
        {
            await this.popupNavigation.PushAsync(new LoaderPopup("Loggin you in..."), true);
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
            {
                await this.popupNavigation.PopAsync();
                Application.Current.MainPage.DisplayAlert("Error", "Username or password is empty!", "Ok");
                return;
            }

            var user = await this.userRepository.CheckCredentialsAsync(Username, Password);

            if (user == null)
            {
                await this.popupNavigation.PopAsync();
                Application.Current.MainPage.DisplayAlert("Error", "Username or password is incorrect!", "Ok");
                return;
            }

            await SecureStorage.SetAsync("user", JsonConvert.SerializeObject(user.UserId));
            await this.popupNavigation.PopAsync();
            await Shell.Current.GoToAsync("home");
        }

        async Task RegisterAsync()
        {
            await Shell.Current.GoToAsync("register");
        }
    }
}
