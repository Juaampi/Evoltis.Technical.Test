using AutoMapper;
using CSharpFunctionalExtensions;
using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Models.Dtos;
using technical_tests_backend_ssr.Repositories;

namespace Evoltis.Technical.Test.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<IEnumerable<ProductDto>>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            if (products is null || !products.Any())
                return Result.Failure<IEnumerable<ProductDto>>("No products found");

            var mapped = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Result.Success(mapped);
        }

        public async Task<Result<ProductDto>> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result.Failure<ProductDto>("Product not found");

            return Result.Success(_mapper.Map<ProductDto>(product));
        }

        public async Task<Result<ProductDto>> CreateAsync(ProductDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            await _repository.CreateAsync(product);
            return Result.Success(_mapper.Map<ProductDto>(product));
        }

        public async Task<Result<ProductDto>> UpdateAsync(int id, ProductDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return Result.Failure<ProductDto>("Product not found");

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);

            return Result.Success(_mapper.Map<ProductDto>(existing));
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null)
                return Result.Failure<bool>("Product not found");

            await _repository.DeleteAsync(product);
            return Result.Success(true);
        }
    }
}
