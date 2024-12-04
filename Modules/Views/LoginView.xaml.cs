/*
    Author: Malik Hasaan Ahmad
    ID: w10171527   
    Assignment: Password Manager Part 2

    Description:
    Handles the login functionality for the app.
*/

namespace CSC317PassManagerP2Starter.Modules.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        // Triggered when the login button is clicked
        private async void ProcessLogin(object sender, EventArgs e)
        {
            // Get username and password input
            string username = txtUserName.Text?.Trim();
            string password = txtPassword.Text?.Trim();

            // Check if input fields are empty
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                ShowErrorMessage("Username and password cannot be empty.");
                return;
            }

            // Authenticate the input
            var authError = Authenticate(username, password);

            // Show error if authentication fails
            if (authError != AuthenticationError.NONE)
            {
                string errorMessage = authError switch
                {
                    AuthenticationError.INVALIDUSERNAME => "Invalid username.",
                    AuthenticationError.INVALIDPASSWORD => "Invalid password.",
                    _ => "Authentication failed. Please try again."
                };
                ShowErrorMessage(errorMessage);
            }
            else
            {
                // Navigate to the password list if login is successful
                Application.Current.MainPage = new PasswordListView();
            }
        }

        // Simulates authentication
        private AuthenticationError Authenticate(string username, string password)
        {
            // Simple check: replace with actual login logic
            if (username == "test" && password == "ab1234")
            {
                return AuthenticationError.NONE; // Login successful
            }
            else if (username != "test")
            {
                return AuthenticationError.INVALIDUSERNAME; // Username is wrong
            }
            else
            {
                return AuthenticationError.INVALIDPASSWORD; // Password is wrong
            }
        }

        // Displays an error message on the UI
        private void ShowErrorMessage(string message)
        {
            lblError.Text = message;
            lblError.IsVisible = true; // Make the error message visible
        }
    }

    // Enum for login error types
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }
}
