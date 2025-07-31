# Evoltis Technical Test - Backend SSR

Este proyecto es una API RESTful desarrollada en ASP.NET Core 6.0 como parte de un desafío técnico. La solución implementa un CRUD para productos, Swagger para documentación interactiva, autenticación por `X-API-KEY` y acceso a base de datos MySQL usando Entity Framework Core con Fluent API.

---

## Tecnologías utilizadas

- ASP.NET Core 6.0
- Entity Framework Core
- MySQL / MariaDB
- Swashbuckle (Swagger)
- AutoMapper
- xUnit (para tests)
- Fluent API
- Inyección de dependencias (DI)
- C#

---

## Requisitos

- .NET 6 SDK
- MySQL o MariaDB
- Visual Studio 2022 o superior (recomendado)
- Postman o herramienta similar para pruebas manuales

---

## Configuración inicial

1. Cloná el proyecto:
```bash
   git clone https://github.com/Juaampi/Evoltis.Technical.Test.git
   cd technical-tests-backend-ssr
```

2. Configurá tu cadena de conexión en appsettings.json:

   
```bash
  
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;port=3306;database=productdb;user=dotnet_user;password=tu_contraseña;"
}
```

3. Restaura paquetes de nuget con 
```bash
dotnet restore
```

4. Creá y aplicá migraciones

```bash

dotnet ef migrations add InitialCreate
dotnet ef database update
```


## Swagger UI
Swagger se encuentra disponible en:

```bash
http://localhost:port/swagger

```

## x-Api-Key
Para acceder a los endpoints protegidos, debés incluir una cabecera x-api-key con tu clave.

```bash
f4a5e439-47c8-4566-ba52-fa550a9cd748

```

Autor
Juan Pablo Garcia
Evoltis - Technical Test

