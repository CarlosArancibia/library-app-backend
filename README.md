# 📚 Backend (.NET 8 API) - Library Manager

Este proyecto es una API REST para la gestión de una biblioteca, desarrollada con .NET 8 siguiendo principios de Clean Architecture.

## 🔧 Tecnologías usadas

- .NET 8
- Entity Framework Core
- JWT (autenticación y autorización)
- AutoMapper
- FluentValidation
- Swagger / OpenAPI
- SQLite (modo desarrollo)

## 🚀 Cómo ejecutar el proyecto

### Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/) (opcional para contenedor)

### 1. Restaurar dependencias

```bash
dotnet restore
```

### 2. Aplicar migraciones y crear la base de datos

```bash
dotnet ef database update
```

### 3. Ejecutar la API

```bash
dotnet run --project src/Api
```

La API estará disponible en:

- http://localhost:5063

### 4. Swagger

Una vez corriendo, puedes acceder a Swagger en:

- http://localhost:5063/swagger

### 5. Configuración del archivo de configuración

Renombrar el archivo: **appsettings.example.json** por **appsettings.json** y configurar según lo prefiera.

### Usuarios de prueba

Para autenticarte, puedes usar el siguiente usuario mockeado:

```json
{
  "email": "admin",
  "password": "password"
},
{
  "email": "user1",
  "password": "123456"
},
{
  "email": "carlos",
  "password": "mipass"
}
```
