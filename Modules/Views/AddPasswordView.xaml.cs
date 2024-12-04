/*
    Author: Malik Hasaan Ahmad
    ID: w10171527   
    Assignment: Password Manager Part 2

    Description:
    View for adding or editing a password in the app.
*/

namespace CSC317PassManagerP2Starter.Modules.Views;
using CSC317PassManagerP2Starter.Modules.Models;

public partial class AddPasswordView : ContentPage
{
    Color baseColorHighlight;
    bool generatedPassword;

    public AddPasswordView()
    {
        InitializeComponent();
        // Used to check if a password has been generated
        generatedPassword = false;
    }

    public AddPasswordView(PasswordModel password = null)
    {
        InitializeComponent();

        // Pre-fills fields if editing an existing password
        if (password != null)
        {
            txtNewPlatform.Text = password.PlatformName;
            txtNewUserName.Text = password.PlatformUserName;
            txtNewPassword.Text = App.PasswordController.DecryptPassword(password.PasswordText);
            txtNewPasswordVerify.Text = txtNewPassword.Text;
        }

        generatedPassword = false;
    }

    private void ButtonCancel(object sender, EventArgs e)
    {
        // Navigates back to the password list when Cancel is clicked
        Application.Current.MainPage = new PasswordListView();
    }

    private void ButtonSubmitExisting(object sender, EventArgs e)
    {
        // Validates and adds a manually entered password
        if (string.IsNullOrWhiteSpace(txtNewPlatform.Text) ||
            string.IsNullOrWhiteSpace(txtNewUserName.Text) ||
            string.IsNullOrWhiteSpace(txtNewPassword.Text) ||
            txtNewPassword.Text != txtNewPasswordVerify.Text)
        {
            lblErrorExistingPassword.Text = "Invalid input or passwords do not match!";
            lblErrorExistingPassword.IsVisible = true;
            return;
        }

        App.PasswordController.AddPassword(
            txtNewPlatform.Text,
            txtNewUserName.Text,
            txtNewPassword.Text
        );

        Application.Current.MainPage = new PasswordListView();
    }

    private void ButtonSubmitGenerated(object sender, EventArgs e)
    {
        // Validates and adds a generated password
        if (!generatedPassword || string.IsNullOrWhiteSpace(lblGenPassword.Text) || lblGenPassword.Text == "<No Password Generated>")
        {
            lblErrorGeneratedPassword.Text = "Please generate a password first.";
            lblErrorGeneratedPassword.IsVisible = true;
            return;
        }

        App.PasswordController.AddPassword(
            txtNewPlatform.Text,
            txtNewUserName.Text,
            lblGenPassword.Text
        );

        Application.Current.MainPage = new PasswordListView();
    }

    private void ButtonGeneratePassword(object sender, EventArgs e)
    {
        // Generates a new password based on the selected criteria
        string newPassword = PasswordGeneration.BuildPassword(
            chkUpperLetter.IsChecked,
            chkDigit.IsChecked,
            txtReqSymbols.Text,
            (int)sprPassLength.Value
        );

        // Displays the generated password
        lblGenPassword.Text = newPassword;
        generatedPassword = true;
        lblErrorGeneratedPassword.IsVisible = false;
    }
}
