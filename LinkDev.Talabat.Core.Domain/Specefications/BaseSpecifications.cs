using LinkDev.Talabat.Core.Domain.Contracts;
using System.Linq.Expressions;

namespace LinkDev.Talabat.Core.Domain.Specefications
{
	public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public Expression<Func<TEntity, bool>>? Critria { get; set; } = null;
		public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new();
		public Expression<Func<TEntity, object>>? OredrBy { get; set; } = null;
		public Expression<Func<TEntity, object>>? OrderByDescending { get; set; } = null;
		public int Skip { get ; set ; }
		public int Take { get ; set ; }
		public bool IsPaginationEnabled { get; set; }

		protected BaseSpecifications()
		{

		}

		protected BaseSpecifications(Expression<Func<TEntity, bool>> critriaExpression)
		{
			Critria = critriaExpression;
		}
		protected BaseSpecifications(TKey id)
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

		private protected void ApplyPagination(int skip, int take)
		{
			IsPaginationEnabled = true;
			Skip = skip;
			Take = take;
		}

	}
}
