using Microsoft.AspNetCore.Identity;

namespace EvaLabs.Security.Entities
{
    public class RoleClaim : IdentityRoleClaim<int>
    {
        public Role Role { get; set; }
    }
}