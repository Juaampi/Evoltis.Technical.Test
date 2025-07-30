using CSharpFunctionalExtensions;
using Evoltis.Technical.Test.API.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using technical_tests_backend_ssr.Models.Dtos;

namespace Evoltis.Technical.Test.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los productos", Description = "Retorna una lista de productos.")]
        [SwaggerResponse(200, "Lista de productos obtenida correctamente", typeof(IEnumerable<ProductDto>))]
        [SwaggerResponse(404, "No se encontraron productos")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(Summary = "Obtener un producto por ID", Description = "Retorna un producto si existe con el ID especificado.")]
        [SwaggerResponse(200, "Producto encontrado", typeof(ProductDto))]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear un nuevo producto", Description = "Crea un producto en la base de datos.")]
        [SwaggerResponse(201, "Producto creado correctamente", typeof(ProductDto))]
        public async Task<IActionResult> Create([FromBody] ProductDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
        }

        [HttpPut("{id:int}")]
        [SwaggerOperation(Summary = "Actualizar un producto", Description = "Actualiza los datos de un producto existente.")]
        [SwaggerResponse(200, "Producto actualizado correctamente", typeof(ProductDto))]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            if (result.IsFailure)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpDelete("{id:int}")]
        [SwaggerOperation(Summary = "Eliminar un producto", Description = "Elimina un producto existente por su ID.")]
        [SwaggerResponse(204, "Producto eliminado correctamente")]
        [SwaggerResponse(404, "Producto no encontrado")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (result.IsFailure)
                return NotFound(result.Error);

            return NoContent();
        }
    }
}
