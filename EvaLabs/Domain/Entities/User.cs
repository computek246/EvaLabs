using System;
using System.Collections.Generic;
using EvaLabs.Domain.Models;
using EvaLabs.Domain.Models.Interfaces;

namespace EvaLabs.Domain.Entities
{
    public class User : Auditable<int?>, IIdentityUser<int>
    {
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserType { get; set; }
        public string UserPassword { get; set; }
        
        public string FullName => $"{FirstName} {LastName}".Trim();

        public ICollection<UserTest> UserTests { get; set; }
    }
}