using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCore.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        public string Login {get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public DateTime RegisteredDate { get; set; }

        public DateTime LastAuthedDate { get; set; }

        public DateTime LastUpdatedDate { get; set; }

        public UserRole Role { get; set; }

        public enum UserRole
        {
            USER = default,
            MODERATOR = 1,
            ADMIN = 2

        }
    }
}

    