using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Data.Contexts
{
    public class IdentityContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public IdentityContext(DbContextOptions options) : base(options)
        {
        }
    }
}
