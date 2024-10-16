using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Common;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models.Products;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Products;
using LinkDev.Talabat.Core.Application.Exceptions;
using LinkDev.Talabat.Core.Domain.Contracts.Products;
using LinkDev.Talabat.Core.Domain.Entities;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Domain.Specefications.Products;
using LinkDev.Talabat.Infrastructure.Persistence.Repositories.Generic_Repositories;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.Talabat.Core.Application.Services.Products
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper mapper) : IProductService
    {
        public async Task<Pagination<ProductToReturnDto>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(specParams.Sort, specParams.BrandId, specParams.CategoryId,
                specParams.PageSize,specParams.PageIndex,specParams.Search);

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
            var data = mapper.Map<IEnumerable<ProductToReturnDto>>(products);

            var countSpec = new ProductWithFilterationForCountSpecifications(specParams.BrandId, specParams.CategoryId,specParams.Search);
            var count = await _unitOfWork.GetRepository<Product,int>().GetCountAsync(countSpec);
            return new Pagination<ProductToReturnDto>(specParams.PageIndex, specParams.PageSize,count) {Data=data };
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(id); 
            var product = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(spec);
            if (product == null)
            
                throw new NotFoundException(nameof(product), id);
			var productToReturn = mapper.Map<ProductToReturnDto>(product);
        
            return productToReturn;
        }
        public async Task<IEnumerable<BrandDto>> GetBrandsAsync()
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var brandsToReturn = mapper.Map<IEnumerable<BrandDto>>(brands);
            return brandsToReturn;
        }

        public async Task<IEnumerable<CategoryDto>> GetCatigoriesAsync()
        {
            var categories = await _unitOfWork.GetRepository<ProductCategory, int>().GetAllAsync();
            var categoriesToReturn = mapper.Map<IEnumerable<CategoryDto>>(categories);
            return categoriesToReturn;
        }

	}
}
