using Microsoft.EntityFrameworkCore;
using technical_tests_backend_ssr.Domain;
using technical_tests_backend_ssr.Models;

namespace technical_tests_backend_ssr.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly EvoltisTestDbContext _context;

        public ProductRepository(EvoltisTestDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync() =>
            await _context.Products.ToListAsync();

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.FindAsync(id);

        public async Task<Product?> GetByNameAsync(string name) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Name == name);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
