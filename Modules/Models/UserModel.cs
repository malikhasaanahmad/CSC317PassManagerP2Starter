/*
    Author: Malik Hasaan Ahmad
    ID: w10171527   
    Assignment: Password Manager Part 2

    Description:
    Defines the model for a user in the app.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class User
    {
        // Represents a user in the app
        public int ID { get; set; } // Unique user ID
        public string FirstName { get; set; } // User's first name
        public string LastName { get; set; } // User's last name
        public string UserName { get; set; } // User's username
        public byte[] PasswordHash { get; set; } // Hashed password
        public byte[] Key { get; set; } // Encryption key for user data
        public byte[] IV { get; set; } // Initialization vector for encryption
    }
}
