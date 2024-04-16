using Assignment.Shared.Models;
using Assignment.Shared.Provider.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Policies
{
    public class BaseEntityPolicy<T> : IInsertPolicy<T>, IUpdatePolicy<T> where T : IBaseEntity
    {
        private readonly ICoreProvider _coreProvider;
        public BaseEntityPolicy(ICoreProvider coreProvider)
        {
            _coreProvider = coreProvider;
        }
        public void PrepareInsert(T entity)
        {
            var userClaims = _coreProvider.IdentityProvider.ClaimsPrincipal;
            var userId = _coreProvider.IdentityProvider.UserManager.GetUserId(userClaims);
            if (userId == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                entity.CreatedBy = userId;
                entity.CreatedAt = DateTime.UtcNow;
            }
        }

        public void PrepareUpdate(T entity)
        {
            var user = _coreProvider.IdentityProvider.ClaimsPrincipal;
            var userId = _coreProvider.IdentityProvider.UserManager.GetUserId(user);
            if (userId == null)
            {
                throw new InvalidOperationException();
            }
            else
            {
                entity.LastUpdatedBy = userId;
                entity.LastUpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
