using Microsoft.AspNetCore.Identity;

namespace EvaLabs.Security.Entities
{
    public class UserClaim : IdentityUserClaim<int>
    {
        public User User { get; set; }
    }
}