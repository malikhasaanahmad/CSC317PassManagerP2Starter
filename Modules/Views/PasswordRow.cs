/*
    Author: Malik Hasaan Ahmad
    ID: w10171527   
    Assignment: Password Manager Part 2

    Description:
    Defines a row in the password list and handles bindings for UI interactions.
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Models;

namespace CSC317PassManagerP2Starter.Modules.Views
{
    public class PasswordRow : BindableObject, INotifyPropertyChanged
    {
        private PasswordModel _pass;
        private bool _isVisible = false;
        private bool _editing = false;

        public PasswordRow(PasswordModel source)
        {
            _pass = source;
        }

        // Binding property for platform name
        public string Platform
        {
            get => _pass?.PlatformName ?? ""; // Get platform name or empty string
            set
            {
                if (_pass.PlatformName != value)
                {
                    _pass.PlatformName = value;
                    RefreshRow(); // Update UI bindings
                }
            }
        }

        // Binding property for platform username
        public string PlatformUserName
        {
            get => _pass?.PlatformUserName ?? ""; // Get username or empty string
            set
            {
                if (_pass.PlatformUserName != value)
                {
                    _pass.PlatformUserName = value;
                    RefreshRow(); // Update UI bindings
                }
            }
        }

        // Binding property for platform password
        public string PlatformPassword
        {
            get => App.PasswordController.DecryptPassword(_pass.PasswordText); // Get decrypted password
            set
            {
                _pass.PasswordText = App.PasswordController.EncryptPassword(value); // Encrypt and set password
                RefreshRow(); // Update UI bindings
            }
        }

        // Binding property for password ID
        public int PasswordID => _pass?.ID ?? -1; // Get password ID or -1 if null

        // Binding property to toggle password visibility
        public bool IsShown
        {
            get => _isVisible; // Get visibility state
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    RefreshRow(); // Update UI bindings
                }
            }
        }

        // Binding property to toggle editing mode
        public bool Editing
        {
            get => _editing; // Get editing state
            set
            {
                if (_editing != value)
                {
                    _editing = value;
                    RefreshRow(); // Update UI bindings
                }
            }
        }

        // Updates UI when a property changes
        private void RefreshRow()
        {
            OnPropertyChanged(nameof(Platform));
            OnPropertyChanged(nameof(PlatformUserName));
            OnPropertyChanged(nameof(PlatformPassword));
            OnPropertyChanged(nameof(IsShown));
            OnPropertyChanged(nameof(Editing));
        }

        // Saves password changes when "save" is clicked
        public void SavePassword()
        {
            if (Editing)
            {
                App.PasswordController.UpdatePassword(_pass); // Save changes to the model
                Editing = false; // Turn off editing mode
            }
        }
    }
}
