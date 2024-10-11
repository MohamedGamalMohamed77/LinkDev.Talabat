using LinkDev.Talabat.Core.Domain.Contracts;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Entities
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity,bool>>? Critria { get; set; } = null;
        public List<Expression<Func<TEntity,object>>> Includes { get; set; } = new();
        public Expression<Func<TEntity, object>>? OredrBy { get; set; } = null;
		public Expression<Func<TEntity, object>>? OrderByDescending { get; set; } = null;

		public BaseSpecifications()
        {

        }
        public BaseSpecifications(TKey id)
        {
            Critria = E => E.Id.Equals(id);
        }

        private protected virtual void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        {
            OredrBy = orderByExpression;
        }
		private protected virtual void AddOrderByDesc(Expression<Func<TEntity, object>> orderByExpressionDesc)
		{
			OrderByDescending = orderByExpressionDesc;
		}
		private protected virtual void AddIncludes()
		{
		}


	}
}
