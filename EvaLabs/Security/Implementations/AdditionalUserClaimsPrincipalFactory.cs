using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EvaLabs.Helper.Constant;
using EvaLabs.Helper.ExtensionMethod;
using EvaLabs.Security.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EvaLabs.Security.Implementations
{
    public class AdditionalUserClaimsPrincipalFactory
        : UserClaimsPrincipalFactory<User, Role>
    {
        private readonly UserManager<User> _userManager;

        public AdditionalUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
            _userManager = userManager;
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            if (identity == null) return principal;

            var dbUser = _userManager.Users
                .Include(e => e.UserRoles)
                .ThenInclude(e => e.Role)
                .FirstOrDefault(u => u.Id == user.Id);

            if (dbUser == null) return principal;

            var claims = new List<Claim>
            {
                new(AppValues.AppClaims.Id, dbUser.Id.ToString()),
                new(AppValues.AppClaims.FullName, dbUser.FullName),
                new(AppValues.AppClaims.User, dbUser.ToJson())
            };

            identity.AddClaims(claims);

            return principal;
        }
    }
}