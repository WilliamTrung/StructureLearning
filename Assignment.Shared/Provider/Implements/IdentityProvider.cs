using Assignment.Shared.Provider.Abstractions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Provider.Implements
{
    public class IdentityProvider : IIdentityProvider
    {
        public ClaimsPrincipal ClaimsPrincipal { get; private set; } = null!;

        public UserManager<IdentityUser> UserManager { get; private set; }

        public RoleManager<IdentityRole> RoleManager { get; private set; }
        public IdentityProvider(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }

        public void UpdateIdentity(ClaimsPrincipal user)
        {
            if (user == null)
            {
                return;
            }
            ClaimsPrincipal = user;
        }
    }
}
