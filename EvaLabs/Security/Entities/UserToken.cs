using Microsoft.AspNetCore.Identity;

namespace EvaLabs.Security.Entities
{
    public class UserToken : IdentityUserToken<int>
    {
        public User User { get; set; }
    }
}