using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Shared.Policies
{
    public interface IDataPolicy
    {
    }
    public interface IFilterPolicy<T> : IDataPolicy
    {
        Expression<Func<T, bool>>? Predicate(Expression<Func<T, bool>>? expression);
    }

    public interface IInsertPolicy<T> : IDataPolicy
    {
        void PrepareInsert(T entity);
    }

    public interface IUpdatePolicy<T> : IDataPolicy
    {
        void PrepareUpdate(T entity);
    }

    public abstract class BaseFilterPolicy<T> : IFilterPolicy<T>
    {
        public abstract Expression<Func<T, bool>>? Predicate(Expression<Func<T, bool>>? expression);

        protected Expression<Func<T, bool>>? Predicate(ref Expression<Func<T, bool>> predicate, Expression<Func<T, bool>> expression)
        {
            return expression is null ? (ExpressionType.Constant.Equals(predicate.Body.NodeType) ? null : predicate) : predicate.And(expression);
        }
    }
}
