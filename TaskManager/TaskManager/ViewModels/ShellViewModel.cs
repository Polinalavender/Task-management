using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {
        public ShellViewModel()
        {

        }
        IAsyncRelayCommand _signoutCommand;
        public IAsyncRelayCommand SignoutCommand => _signoutCommand ??= new AsyncRelayCommand(LogoutAsync);

        async Task LogoutAsync()
        {
            SecureStorage.RemoveAll();
            await Shell.Current.GoToAsync("//login");
        }
    }
}
