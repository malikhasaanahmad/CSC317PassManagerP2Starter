/*
    Author: Malik Hasaan Ahmad
    ID: w10171527   
    Assignment: Password Manager Part 2

    Description:
    View for displaying and managing a list of passwords.
*/

using System.Collections.ObjectModel;

namespace CSC317PassManagerP2Starter.Modules.Views;

public partial class PasswordListView : ContentPage
{
    private ObservableCollection<PasswordRow> _rows = new ObservableCollection<PasswordRow>();

    public PasswordListView()
    {
        InitializeComponent();

        // Generate test passwords for the user
        App.PasswordController.GenTestPasswords();

        // Load and display the passwords in the list
        App.PasswordController.PopulatePasswordView(_rows);

        // Bind the password list to the UI
        collPasswords.ItemsSource = _rows;
    }

    // Updates the password list based on search input
    private void TextSearchBar(object sender, TextChangedEventArgs e)
    {
        App.PasswordController.PopulatePasswordView(_rows, e.NewTextValue);
    }

    // Copies the selected password to the clipboard
    private void CopyPassToClipboard(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        var password = App.PasswordController.GetPassword(ID);
        if (password != null)
        {
            var decryptedPassword = App.PasswordController.DecryptPassword(password.PasswordText);
            Clipboard.SetTextAsync(decryptedPassword);
            DisplayAlert("Success", "Password copied to clipboard.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Password not found.", "OK");
        }
    }

    // Toggles between editing and saving a password entry
    private void EditPassword(object sender, EventArgs e)
    {
        var btnSender = sender as Button;
        if (btnSender == null) return;

        int ID = Convert.ToInt32(btnSender.CommandParameter);

        var passwordRow = _rows.FirstOrDefault(row => row.PasswordID == ID);
        if (passwordRow == null)
        {
            DisplayAlert("Error", "Password not found.", "OK");
            return;
        }

        if (passwordRow.Editing)
        {
            // Save changes and switch to ReadOnly mode
            passwordRow.SavePassword();
            btnSender.Text = "Edit";
        }
        else
        {
            // Enable edit mode
            passwordRow.Editing = true;
            btnSender.Text = "Save";
        }
    }

    // Deletes the selected password
    private void DeletePassword(object sender, EventArgs e)
    {
        int ID = Convert.ToInt32((sender as Button).CommandParameter);

        if (App.PasswordController.RemovePassword(ID))
        {
            App.PasswordController.PopulatePasswordView(_rows);
            DisplayAlert("Success", "Password deleted successfully.", "OK");
        }
        else
        {
            DisplayAlert("Error", "Password not found.", "OK");
        }
    }

    // Navigates to the Add Password screen
    private void ButtonAddPassword(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AddPasswordView();
    }

    // Toggles visibility of a password
    private void ToggleShowPassword(object sender, ToggledEventArgs e)
    {
        var switchControl = sender as Switch;
        if (switchControl == null) return;

        var passwordRow = switchControl.BindingContext as PasswordRow;
        if (passwordRow != null)
        {
            passwordRow.IsShown = switchControl.IsToggled;

            if (!passwordRow.IsShown)
            {
                passwordRow.PlatformPassword = "********"; // Mask password
            }
            else
            {
                var passwordModel = App.PasswordController.GetPassword(passwordRow.PasswordID);
                if (passwordModel != null)
                {
                    passwordRow.PlatformPassword = App.PasswordController.DecryptPassword(passwordModel.PasswordText);
                }
            }

            // Refresh the UI
            collPasswords.ItemsSource = null;
            collPasswords.ItemsSource = _rows;
        }
    }
}
