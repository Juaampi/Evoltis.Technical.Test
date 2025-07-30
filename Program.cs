using Evoltis.Technical.Test.API.Services;
using Microsoft.EntityFrameworkCore;
using technical_tests_backend_ssr.Domain;
using technical_tests_backend_ssr.Infraestruture.Middlewares;
using technical_tests_backend_ssr.MappingServices;
using technical_tests_backend_ssr.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Evoltis.Technical.Test.API", Version = "v1" });
    c.EnableAnnotations(); 

    c.AddSecurityDefinition("ApiKey", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "API Key necesaria en el header: x-api-key",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Name = "x-api-key",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                },
                Scheme = "ApiKeyScheme",
                Name = "x-api-key",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            },
            new List<string>()
        }
    });


}); 

builder.Services.AddAutoMapper(typeof(ProductMappingProfile));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();


//configurar Entity Framework Core con MySQL
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EvoltisTestDbContext>(opt => 
    opt.UseMySql(connectionString,
        new MySqlServerVersion(ServerVersion.AutoDetect(connectionString))
    )
);

//configurar automapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{ 
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Evoltis.Technical.Test.API v1");
        c.RoutePrefix = string.Empty; // Swagger en raíz
    });
}

app.UseMiddleware<ApiKeyMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
