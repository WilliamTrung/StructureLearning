using Assignment.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Policies
{
    public class ActiveEntityPolicy<T> : BaseFilterPolicy<T>, IInsertPolicy<T> where T : IActiveEntity
    {
        public override Expression<Func<T, bool>>? Predicate(Expression<Func<T, bool>>? expression)
        {
            return expression;
        }

        public void PrepareInsert(T entity)
        {
            entity.IsActive = true;
        }
    }
}
