using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts;

namespace LinkDev.Talabat.Infrastructure.Persistence.Generic_Repositories
{
	public static class SpecificationsEvaluator<TEntity, TKey>
		where TEntity : BaseEntity<TKey>
		where TKey : IEquatable<TKey>
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> spec)
		{
			var query = inputQuery;
			if (spec.Critria is not null)
				query = query.Where(spec.Critria);


			if(spec.OrderByDescending is not null)
				query = query.OrderByDescending(spec.OrderByDescending);

			else if (spec.OredrBy is not null)
				query = query.OrderBy(spec.OredrBy);

			if(spec.IsPaginationEnabled)
				query=query.Skip(spec.Skip).Take(spec.Take);


			query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

			return query;
		}
	}
}
