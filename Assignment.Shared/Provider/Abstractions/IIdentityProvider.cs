using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Provider.Abstractions
{
    public interface IIdentityProvider
    {
        UserManager<IdentityUser> UserManager { get; }
        RoleManager<IdentityRole> RoleManager { get; }
        ClaimsPrincipal ClaimsPrincipal { get; }
        void UpdateIdentity(ClaimsPrincipal user);
    }
}
