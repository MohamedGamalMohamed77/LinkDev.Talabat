using LinkDev.Talabat.Core.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Entities
{
    public abstract class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity,bool>>? Critria { get; set; } = null;
        public List<Expression<Func<TEntity,object>>> Includes { get; set; } = new();

        public BaseSpecifications()
        {

        }
        public BaseSpecifications(TKey id)
        {
            Critria = E => E.Id.Equals(id);
        }

		
	}
}
