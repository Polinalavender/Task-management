using Mopups.Pages;
using System.Threading.Tasks;

namespace TaskManagement.Views;

public partial class LoadingPage: ContentPage
{
    public string LoadingText { get; set; }
    private string title;
    TaskCompletionSource<string> _taskCompletionSource;
    public Task<string> PopupDismissedTask => _taskCompletionSource.Task;
    public string ReturnValue { get; set; }
    public LoadingPage()
	{
		InitializeComponent();
        this.title = "Loading...";
	}

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        if (await isAuthenticated())
        {
            await Shell.Current.GoToAsync("///home");
        }
        else
        {
            await Shell.Current.GoToAsync("login");
        }
        base.OnNavigatedTo(args);
    }

    async Task<bool> isAuthenticated()
    {
        await Task.Delay(2000);
        var hasAuth = await SecureStorage.GetAsync("user");
        return !(hasAuth == null);
    }
}