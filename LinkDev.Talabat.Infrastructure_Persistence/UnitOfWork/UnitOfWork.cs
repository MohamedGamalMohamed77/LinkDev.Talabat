using LinkDev.Talabat.Core.Domain.Common;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private readonly ConcurrentDictionary<string, object> _repositories;

		//private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
		//private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandRepository;
		//private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;

		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_repositories = new();
			//_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product,int>(_dbContext));
			//_brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
			//_categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));

		}
		public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
					where TEntity : BaseEntity<TKey>
					where TKey : IEquatable<TKey>
		{
			return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name
				,new GenericRepository<TEntity, TKey>(_dbContext));


		}

		//public IGenericRepository<Product, int> productRepository => _productRepository.Value;
		//public IGenericRepository<ProductCategory, int> categoryRepository => _categoryRepository.Value;
		//public IGenericRepository<ProductBrand, int> brandRepository => _brandRepository.Value;


		public async Task<int> CompeleteAsync() => await _dbContext.SaveChangesAsync();
		public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();


	}
}
