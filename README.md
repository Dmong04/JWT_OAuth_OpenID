# JWT PLECSYS — OAuth 2.0 con OpenIddict

Sistema de autenticación y autorización basado en **OAuth 2.0** usando **OpenIddict** como servidor de tokens, construido sobre **ASP.NET Core** con arquitectura limpia (Domain, Application, Infrastructure, API).

---

## 📋 Tabla de contenidos

- [Prerrequisitos](#prerrequisitos)
- [Tecnologías y dependencias](#tecnologías-y-dependencias)
- [Configuración del proyecto](#configuración-del-proyecto)
- [Migraciones y base de datos](#migraciones-y-base-de-datos)
- [Uso de endpoints](#uso-de-endpoints)
- [Estructura del proyecto](#estructura-del-proyecto)
- [Notas importantes](#notas-importantes)

---

## Prerrequisitos

Antes de ejecutar el proyecto, asegúrate de tener instalado lo siguiente:

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/es-mx/sql-server/sql-server-downloads) (local o en la nube)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o [VS Code](https://code.visualstudio.com/)
- [Postman](https://www.postman.com/) o [Insomnia](https://insomnia.rest/) para pruebas

---

## Tecnologías y dependencias

Instala los siguientes paquetes NuGet en los proyectos correspondientes:

| Paquete | Proyecto | Descripción |
|---|---|---|
| `OpenIddict.AspNetCore` | API | Middleware de autenticación OAuth 2.0 |
| `OpenIddict.EntityFrameworkCore` | INFRAESTRUCTURE | Persistencia de tokens y aplicaciones |
| `Microsoft.EntityFrameworkCore.SqlServer` | INFRAESTRUCTURE | Proveedor de base de datos |
| `Microsoft.EntityFrameworkCore.Tools` | INFRAESTRUCTURE | Herramientas de migración |
| `FastEndpoints` | API | Definición de endpoints |

<img width="950" height="455" alt="image" src="https://github.com/user-attachments/assets/e54fd22b-2516-4ffb-8d48-d6ea07dc6736" />

---

## Configuración del proyecto

### 1. Cadena de conexión

En el archivo `appsettings.json` de la capa **API**, configura tu conexión a SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=TU_BASE_DE_DATOS;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

### 2. Clave de encriptación

El proyecto usa una **SymmetricSecurityKey** para encriptar los tokens. La clave está definida en `Program.cs`. Si la cambias, asegúrate de que esté en formato Base64 y tenga al menos 256 bits:

```csharp
options.AddEncryptionKey(new SymmetricSecurityKey(
    Convert.FromBase64String("TU_CLAVE_EN_BASE64")));
```

> ⚠️ **Nunca expongas esta clave en repositorios públicos.** Usa variables de entorno o Azure Key Vault en producción.

<img width="1240" height="549" alt="image" src="https://github.com/user-attachments/assets/fde2dfab-df0d-4340-b1d0-bba4aba13fbb" />

---

## Migraciones y base de datos

### Paso 1 — Crear la migración

Ejecuta el siguiente comando desde la consola del Administrador de paquetes en Visual Studio (o terminal en la raíz de la solución):

```bash
dotnet ef migrations add AddOpenIddict --project INFRAESTRUCTURE --startup-project API
```

Esto generará tres archivos en la carpeta `Migrations/` de tu proyecto de infraestructura.

<img width="367" height="88" alt="image" src="https://github.com/user-attachments/assets/e08bb48f-e782-4d20-ac30-6372c0cde1e3" />

### Paso 2 — Aplicar la migración

```bash
dotnet ef database update --project INFRAESTRUCTURE --startup-project API
```

### Paso 3 — Resolver conflictos con tablas existentes

Si ya tienes tablas propias en la base de datos y la migración intenta recrearlas, **comenta o elimina** el código `CreateTable` correspondiente dentro del archivo de migración generado, conservando únicamente la creación de las tablas de OpenIddict:

- `OpenIddictApplications`
- `OpenIddictAuthorizations`
- `OpenIddictScopes`
- `OpenIddictTokens`

<img width="1109" height="661" alt="image" src="https://github.com/user-attachments/assets/19aeb7cc-37a6-4d7d-bca0-82a6781fe341" />
<img width="1091" height="628" alt="image" src="https://github.com/user-attachments/assets/9a954cc1-9c94-4364-a898-a7577eadc696" />
<img width="1275" height="770" alt="image" src="https://github.com/user-attachments/assets/d2eb982c-22b2-4fed-b7db-1ea5ed8f11f0" />

### Paso 4 — Poblar la base de datos

Antes de hacer pruebas, inserta al menos un usuario en tu tabla de usuarios. El sistema no incluye un seeder automático por fines prácticos de la demo.

---

## Uso de endpoints

### 🔐 Obtener un token de acceso

**POST** `https://localhost:7131/connect/token`

Content-Type: `application/x-www-form-urlencoded`

| Campo | Valor |
|---|---|
| `grant_type` | `password` |
| `username` | `tu_usuario` |
| `password` | `tu_contraseña` |

**Respuesta exitosa (200 OK):**

```json
{
  "access_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6...",
  "token_type": "Bearer",
  "expires_in": 1800,
  "refresh_token": "eyJhbGciOiJSUzI1NiIsInR5cCI6..."
}
```

### 🔄 Renovar el token (Refresh)

**POST** `https://localhost:7131/connect/token`

| Campo | Valor |
|---|---|
| `grant_type` | `refresh_token` |
| `refresh_token` | `tu_refresh_token` |

### 🔒 Usar el token en requests protegidos

Agrega el header `Authorization` en tus peticiones:

```
Authorization: Bearer TU_ACCESS_TOKEN
```

**Tiempos de vida configurados:**
- Access token: **30 minutos**
- Refresh token: **7 días**

<img width="1386" height="876" alt="image" src="https://github.com/user-attachments/assets/1c349b9d-cfbb-4d85-b36a-95bb8ffad922" />

---

## Estructura del proyecto

```
JWT_PLECSYS/
├── API/                        # Capa de presentación (endpoints, Program.cs)
├── APPLICATION/                # Casos de uso, handlers, servicios
│   ├── Services/
│   └── Use_cases/Handlers/
├── DOMAIN/                     # Entidades e interfaces
│   └── Interfaces/
└── INFRAESTRUCTURE/            # Contexto EF Core, repositorios
    ├── Context/
    └── Repositories/
```

---

## Notas importantes

- Este proyecto está configurado para **desarrollo local**. Los certificados usados (`AddDevelopmentEncryptionCertificate` y `AddDevelopmentSigningCertificate`) no son aptos para producción.
- En producción, reemplaza los certificados de desarrollo por certificados reales y mueve las claves sensibles a variables de entorno o un gestor de secretos.
- El flujo `AcceptAnonymousClients()` está habilitado para simplificar la demo; en producción considera registrar clientes explícitamente.
