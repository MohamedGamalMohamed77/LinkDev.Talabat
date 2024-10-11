using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Domain.Contracts
{
	public interface ISpecifications<TEntity,TKey>
		where TEntity:BaseEntity<TKey> 
		where TKey:IEquatable<TKey>
	{
		public Expression<Func<TEntity,bool>>? Critria { get; set; }
		public  List<Expression<Func<TEntity,object>>> Includes { get; set; }
		public Expression<Func<TEntity, object>>? OredrBy { get; set; }
		public Expression<Func<TEntity, object>>? OrderByDescending { get; set; }


	}
}
