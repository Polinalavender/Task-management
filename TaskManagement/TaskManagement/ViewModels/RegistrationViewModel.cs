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
using DataAccessLayer.Utils;
using TaskManagement.Models;

namespace TaskManagement.ViewModels
{
    public partial class RegistrationViewModel: ObservableObject
    {
        IPopupNavigation popupNavigation;
        UserRepository userRepository;
        public RegistrationViewModel()
        {
            this.popupNavigation = Application.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupNavigation>();
            this.userRepository = Application.Current.MainPage.Handler.MauiContext.Services.GetService<UserRepository>();
        }
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string email;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string confirmPassword;

        [ObservableProperty]
        string bilalSaeed;

        IAsyncRelayCommand registerCommand;
        public IAsyncRelayCommand RegisterCommand => registerCommand ??= new AsyncRelayCommand(RegisterAsync);
        //LoginCommand


        IAsyncRelayCommand loginCommand;
        public IAsyncRelayCommand LoginCommand => loginCommand ??= new AsyncRelayCommand(LoginAsync);

        async Task RegisterAsync()
        {
            await this.popupNavigation.PushAsync(new LoaderPopup("Loader..."), true);
            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Username) ||
                     string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(ConfirmPassword))
            {
                await this.popupNavigation.PopAsync();
                Application.Current.MainPage.DisplayAlert("Error", "All fields are mandatory!", "Ok");
                return;
            }
            // await userRepository.GetByEmailAsync(Username);
            if (Password != ConfirmPassword)
            {
                await this.popupNavigation.PopAsync();  
                Application.Current.MainPage.DisplayAlert("Error", "Passwords do not match@", "Ok");
                return;
            }

            if (await this.userRepository.GetByUsernameAsync(Username) != null)
            {
                await this.popupNavigation.PopAsync();
                Application.Current.MainPage.DisplayAlert("Error", "User already exists!", "Ok");
                return;
            }
            
            await this.userRepository.AddAsync(new User { Username = Username, Email = Email, PasswordHash = PasswordHashUtility.GetHash(Password) });
            await this.popupNavigation.PopAsync();
            await Shell.Current.GoToAsync("//login");

            //if (user != null)
            //{
            //    // Registration successful, navigate to login page
            //    await Shell.Current.GoToAsync("//login");
            //}
            //else
            //{
            //    // Registration failed, display error message
            //    //await DisplayAlert("Error", "Registration failed", "OK");
            //}
        }

        async Task LoginAsync()
        {
            await Shell.Current.GoToAsync("//login");
        }
    }
}
