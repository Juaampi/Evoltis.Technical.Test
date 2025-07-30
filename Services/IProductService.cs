using CSharpFunctionalExtensions;
using technical_tests_backend_ssr.Models.Dtos;

namespace Evoltis.Technical.Test.API.Services
{
    public interface IProductService
    {
        Task<Result<IEnumerable<ProductDto>>> GetAllAsync();
        Task<Result<ProductDto>> GetByIdAsync(int id);
        Task<Result<ProductDto>> CreateAsync(ProductDto dto);
        Task<Result<ProductDto>> UpdateAsync(int id, ProductDto dto);
        Task<Result<bool>> DeleteAsync(int id);
    }
}
