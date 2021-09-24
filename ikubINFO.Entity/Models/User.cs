using System;
using System.Collections.Generic;

#nullable disable

namespace ikubINFO.DataAccess
{
    public class User
    {
        public int UserId { get; set; }
        public string UserGuid { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Initials { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string Image { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual Role Role { get; set; }
    }
}
