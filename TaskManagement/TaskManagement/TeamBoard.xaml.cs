using Microsoft.Maui.Controls;

namespace TaskManagement
{
    public partial class TeamBoard : ContentPage
    {
        public TeamBoard()
        {
            InitializeComponent();
        }

        // Event handler for when a sidebar button is pressed
        private void OnSideButtonPressed(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundColor = Colors.LavenderBlush;
        }

        // Event handler for when a sidebar button is released
        private void OnSideButtonReleased(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundColor = Colors.White; // This should match the original button color
            DisplayAlert("Side Bar", $"{button.Text} was clicked", "OK");
        }

        // Event handler for when a top bar button is clicked

        private void OnNavButtonPressed(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundColor = Colors.LavenderBlush;
        }

        private void OnNavButtonReleased(object sender, EventArgs e)
        {
            var button = (Button)sender;
            button.BackgroundColor = Colors.White; // This should match the original button color
            DisplayAlert("Navigation", $"{button.Text} was clicked", "OK");
        }
    }
}
