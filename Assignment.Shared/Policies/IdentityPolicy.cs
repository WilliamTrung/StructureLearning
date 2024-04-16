using Assignment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Policies
{
    public class IdentityPolicy<T> : IInsertPolicy<T> where T : IIdentity
    {
        public IdentityPolicy()
        {

        }
        public void PrepareInsert(T entity)
        {
            entity.GenerateId();
        }
    }
}
