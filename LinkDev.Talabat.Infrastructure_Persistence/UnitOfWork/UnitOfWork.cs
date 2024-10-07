using LinkDev.Talabat.Core.Domain.Contracts;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Data;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Infrastructure.Persistence.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreContext _dbContext;
		private readonly Lazy<IGenericRepository<Product, int>> _productRepository;
		private readonly Lazy<IGenericRepository<ProductBrand, int>> _brandRepository;
		private readonly Lazy<IGenericRepository<ProductCategory, int>> _categoryRepository;

		public UnitOfWork(StoreContext dbContext)
		{
			_dbContext = dbContext;
			_productRepository = new Lazy<IGenericRepository<Product, int>>(() => new GenericRepository<Product,int>(_dbContext));
			_brandRepository = new Lazy<IGenericRepository<ProductBrand, int>>(() => new GenericRepository<ProductBrand, int>(_dbContext));
			_categoryRepository = new Lazy<IGenericRepository<ProductCategory, int>>(() => new GenericRepository<ProductCategory, int>(_dbContext));

		}

		public IGenericRepository<Product, int> productRepository => _productRepository.Value;
		public IGenericRepository<ProductCategory, int> categoryRepository => _categoryRepository.Value;
		public IGenericRepository<ProductBrand, int> brandRepository => _brandRepository.Value;
		public Task<int> CompeleteAsync()
		{
			throw new NotImplementedException();
		}
		public ValueTask DisposeAsync()
		{
			throw new NotImplementedException();
		}
	}
}
