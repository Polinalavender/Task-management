using System.ComponentModel;
using System.Security.Cryptography;
using System.Text;

namespace TaskManagement
{
    public partial class RegistrationPage : ContentPage, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }

        public RegistrationPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        private async void Submit_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(RepeatPassword))
            {
                // Handle missing fields
                await DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            if (Password != RepeatPassword)
            {
                // Handle password mismatch
                await DisplayAlert("Error", "Passwords do not match.", "OK");
                return;
            }

            // Check if username and email are available (e.g., not already taken)
            bool isUsernameAvailable = CheckUsernameAvailability(Username);
            bool isEmailAvailable = CheckEmailAvailability(Email);

            if (!isUsernameAvailable)
            {
                // Handle username not available
                await DisplayAlert("Error", "Username is not available.", "OK");
                return;
            }

            if (!isEmailAvailable)
            {
                // Handle email not available
                await DisplayAlert("Error", "Email is not available.", "OK");
                return;
            }

            // Hash the password for security
            string hashedPassword = HashPassword(Password);

            // Save the registration data to the database
            SaveToDatabase(Username, Email, hashedPassword);

            // Navigate to the main screen
            await Navigation.PushAsync(new MainPage());
        }


        private bool CheckUsernameAvailability(string username)
        {
            // Placeholder method to check if the username is available
            return true; // Replace with actual implementation
        }

        private bool CheckEmailAvailability(string email)
        {
            // Placeholder method to check if the email is available
            return true; // Replace with actual implementation
        }

        private string HashPassword(string password)
        {
            // Hash the password using SHA256 algorithm
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void SaveToDatabase(string username, string email, string hashedPassword)
        {
            // Use Entity Framework Core or your preferred ORM to save data to the database
            using (var dbContext = new AppDbContext())
            {
                var user = new User { Username = username, Email = email, PasswordHash = hashedPassword };
                dbContext.Users.Add(user);
                dbContext.SaveChanges();
            }
        }
    }
}
