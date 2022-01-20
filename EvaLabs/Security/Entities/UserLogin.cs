using Microsoft.AspNetCore.Identity;

namespace EvaLabs.Security.Entities
{
    public class UserLogin : IdentityUserLogin<int>
    {
        public User User { get; set; }
    }
}