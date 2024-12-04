/*
    Author: Malik Hasaan Ahmad
    ID: w10171527   
    Assignment: Password Manager Part 2

    Description:
    Backend implementation of a simple password manager app.
*/

using CSC317PassManagerP2Starter.Modules.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Controllers
{
    public enum AuthenticationError { NONE, INVALIDUSERNAME, INVALIDPASSWORD }

    public class LoginController
    {
        private User _user = new User();
        private bool _loggedIn = false;

        public LoginController()
        {
            // Create a test user for initial setup
            _user = new User
            {
                ID = 1,
                FirstName = "John",
                LastName = "Doe",
                UserName = "test",
                PasswordHash = HashPassword("ab1234"),
                Key = GenerateKey(),
                IV = GenerateIV()
            };
        }

        // Returns the current user's details if logged in, otherwise null
        public User? CurrentUser
        {
            get
            {
                return _loggedIn ? new User
                {
                    ID = _user.ID,
                    FirstName = _user.FirstName,
                    LastName = _user.LastName,
                    UserName = _user.UserName,
                    Key = _user.Key,
                    IV = _user.IV
                } : null;
            }
        }

        // Validates username and password, sets logged-in status, and returns error if any
        public AuthenticationError Authenticate(string username, string password)
        {
            if (username != _user.UserName)
                return AuthenticationError.INVALIDUSERNAME;

            if (!CompareHashes(HashPassword(password), _user.PasswordHash))
                return AuthenticationError.INVALIDPASSWORD;

            _loggedIn = true;
            return AuthenticationError.NONE;
        }

        // Hashes the password using MD5
        private byte[] HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                return md5.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        // Compares two byte arrays to check if they match
        private bool CompareHashes(byte[] hash1, byte[] hash2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(hash1, hash2);
        }

        // Generates a dummy encryption key
        private byte[] GenerateKey()
        {
            return Encoding.UTF8.GetBytes("SampleKey1234567");
        }

        // Generates a dummy encryption IV
        private byte[] GenerateIV()
        {
            return Encoding.UTF8.GetBytes("SampleIV1234567");
        }
    }
}

