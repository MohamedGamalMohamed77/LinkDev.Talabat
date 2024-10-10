using AutoMapper;
using LinkDev.Talabat.Core.Aplication.Abstraction.Models;
using LinkDev.Talabat.Core.Aplication.Abstraction.Services.Products;
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
        public async Task<IEnumerable<ProductToReturnDto>> GetProductsAsync()
        {
            var spec = new ProductWithBrandAndCategorySpecifications();

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllWithSpecAsync(spec);
            var productsToReturn = mapper.Map<IEnumerable<ProductToReturnDto>>(products);
            return productsToReturn;
        }
        public async Task<ProductToReturnDto> GetProductAsync(int id)
        {
            var spec = new ProductWithBrandAndCategorySpecifications(); 
            var product = await _unitOfWork.GetRepository<Product, int>().GetWithSpecAsync(spec);
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
