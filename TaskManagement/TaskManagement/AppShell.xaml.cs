using Mopups.Interfaces;
using Mopups.Services;
using TaskManagement.Views;

namespace TaskManagement
{
    public partial class AppShell : Shell
    {
        public AppShell(IPopupNavigation popupNavigation, UserRepository userRepository)
        {
            InitializeComponent();
            Routing.RegisterRoute("login", typeof(LogInPage));
            Routing.RegisterRoute("main", typeof(MainPage));
            Routing.RegisterRoute("home", typeof(TeamBoard));
            Routing.RegisterRoute("register", typeof(RegistrationPage));
            //Routing.RegisterRoute("settings", typeof(SettingsPage));
        }
    }
}
