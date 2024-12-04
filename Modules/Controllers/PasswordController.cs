/*
    Author: Malik Hasaan Ahmad
    ID: w10171527
    Assignment: Password Manager Part 2

    Description:
    Backend for a password manager app.
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSC317PassManagerP2Starter.Modules.Controllers;
using CSC317PassManagerP2Starter.Modules.Models;
using CSC317PassManagerP2Starter.Modules.Views;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public class PasswordController
    {
        // Stores a list of passwords for the test user
        public List<PasswordModel> _passwords = new List<PasswordModel>();
        private int counter = 1;

        // Copies passwords to the view and filters them by search criteria if provided
        public void PopulatePasswordView(ObservableCollection<PasswordRow> source, string search_criteria = "")
        {
            source.Clear();

            // Filter passwords if a search term is provided
            var filteredPasswords = string.IsNullOrEmpty(search_criteria)
                ? _passwords
                : _passwords.Where(p =>
                    p.PlatformName.Contains(search_criteria, StringComparison.OrdinalIgnoreCase) ||
                    p.PlatformUserName.Contains(search_criteria, StringComparison.OrdinalIgnoreCase)).ToList();

            // Add filtered passwords to the view
            foreach (var password in filteredPasswords)
            {
                source.Add(new PasswordRow(password));
            }
        }

        // Adds a new password to the list
        public void AddPassword(string platform, string username, string password)
        {
            var encryptedPassword = EncryptPassword(password);
            _passwords.Add(new PasswordModel
            {
                ID = counter++,
                PlatformName = platform,
                PlatformUserName = username,
                PasswordText = encryptedPassword
            });
        }

        // Finds a password by ID
        public PasswordModel? GetPassword(int ID)
        {
            return _passwords.FirstOrDefault(p => p.ID == ID);
        }

        // Updates an existing password
        public bool UpdatePassword(PasswordModel changes)
        {
            var existingPassword = GetPassword(changes.ID);
            if (existingPassword != null)
            {
                existingPassword.PlatformName = changes.PlatformName;
                existingPassword.PlatformUserName = changes.PlatformUserName;
                existingPassword.PasswordText = changes.PasswordText;
                return true;
            }
            return false;
        }

        // Removes a password by ID
        public bool RemovePassword(int ID)
        {
            var password = GetPassword(ID);
            if (password != null)
            {
                _passwords.Remove(password);
                return true;
            }
            return false;
        }

        // Adds sample passwords for testing
        public void GenTestPasswords()
        {
            AddPassword("Facebook", "user1", "password1");
            AddPassword("Google", "user2", "password2");
            AddPassword("Twitter", "user3", "password3");
        }

        // Encrypts a password (basic implementation)
        public byte[] EncryptPassword(string password)
        {
            return Encoding.UTF8.GetBytes(password); 
        }

        // Decrypts a password (basic implementation)
        public string DecryptPassword(byte[] encryptedPassword)
        {
            return Encoding.UTF8.GetString(encryptedPassword); 
        }
    }
}

