/*
    Author: Malik Hasaan Ahmad
    ID: w10171527  
    Assignment: Password Manager Part 2

    Description:
    Defines the model for storing password details.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSC317PassManagerP2Starter.Modules.Models
{
    public class PasswordModel
    {
        // Represents a password entry
        public int ID { get; set; } // Unique ID for the password
        public int UserID { get; set; } // ID of the user who owns the password
        public string PlatformName { get; set; } // Name of the platform (e.g., Twitter)
        public string PlatformUserName { get; set; } // Username on the platform
        public byte[] PasswordText { get; set; } // Encrypted password
    }
}

