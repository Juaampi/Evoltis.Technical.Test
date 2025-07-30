using AutoMapper;
using Moq;
using technical_tests_backend_ssr.Models;
using technical_tests_backend_ssr.Models.Dtos;
using technical_tests_backend_ssr.Repositories;
using Xunit;
using Evoltis.Technical.Test.API.Services;

namespace technical_tests_backend_ssr.UnitTests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Product, ProductDto>().ReverseMap();
            });
            _mapper = config.CreateMapper();

            _service = new ProductService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsFailure_WhenNoProducts()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Product>());

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("No products found", result.Error);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsSuccess_WhenProductsExist()
        {
            // Arrange
            var products = new List<Product>
        {
            new Product { Id = 1, Name = "Prod1" },
            new Product { Id = 2, Name = "Prod2" }
        };
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsFailure_WhenProductNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product not found", result.Error);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsSuccess_WhenProductFound()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Prod1" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(1, result.Value.Id);
            Assert.Equal("Prod1", result.Value.Name);
        }

        [Fact]
        public async Task CreateAsync_ReturnsSuccess()
        {
            // Arrange
            var dto = new ProductDto { Name = "New Product" };
            _repositoryMock.Setup(r => r.CreateAsync(It.IsAny<Product>())).Returns(Task.FromResult<Product>);

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("New Product", result.Value.Name);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsSuccess_WhenProductUpdated()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "Old Name" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.UpdateAsync(product)).Returns(Task.CompletedTask);

            var dto = new ProductDto { Name = "Updated Name" };

            // Act
            var result = await _service.UpdateAsync(1, dto);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Updated Name", result.Value.Name);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsFailure_WhenProductNotFound()
        {
            // Arrange
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result.IsFailure);
            Assert.Equal("Product not found", result.Error);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsSuccess_WhenProductDeleted()
        {
            // Arrange
            var product = new Product { Id = 1, Name = "ToDelete" };
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(product);
            _repositoryMock.Setup(r => r.DeleteAsync(product)).Returns(Task.CompletedTask);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }
    }
}
