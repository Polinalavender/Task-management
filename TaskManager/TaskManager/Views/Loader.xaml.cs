namespace TaskManager.Views;

public partial class LoaderPopup : Mopups.Pages.PopupPage
{
    public LoaderPopup(string title)
    {
        InitializeComponent();
        if (string.IsNullOrWhiteSpace(title))
        {
            this.loadingLabel.Text = "Please wait...";
        }
        else
        {
            this.loadingLabel.Text = title;
        }
    }
}