using APPLICATION.services;
using APPLICATION.Use_cases.Handlers;
using DOMAIN.Interfaces;
using FastEndpoints;
using INFRAESTRUCTURE.Context;
using INFRAESTRUCTURE.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Server;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); // Necesario para que Open ID registre sus controladores

// Database context connection

builder.Services.AddDbContext<AppDBContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints();

// Configure the HTTP request pipeline.

builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<ITokenRepository, TokenRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<CreateUserHandler>();

// Add services to the container.

builder.Services.AddOpenIddict()
    .AddValidation(options =>
    {
        options.UseLocalServer(); // Validar tokens emitidos por la propia API
        options.UseAspNetCore(); // Integra la validación con el middleware de ASP.NET Core

        // Opcional: establece tu issuer explícito
        options.SetIssuer("https://localhost:7131/"); // Firma explícita de quien los emite

        // Si quieres usar una clave simétrica personalizada (no necesaria si usas certificados, especial para desarrollo)
        options.AddEncryptionKey(new SymmetricSecurityKey(
            Convert.FromBase64String("U2rPe7XldpAnfA1Lg4FbyjNsbjAK6A7EKFqR0mQOq9M=")));

        options.UseSystemNetHttp(); // En caso de hacer llamadas HTTP contra un servidor remoto, no aplica en este caso
    })
    .AddCore(options => // Configuración del almacenamiento de datos
    {
        options.UseEntityFrameworkCore() // Le dice a OpenIddict que guarde aplicaciones, autorizaciones, tokens, etc.
               .UseDbContext<AppDBContext>() // Conexión con las entidades propias y ahora las de OpenIddict
               .ReplaceDefaultEntities<Guid>(); // Reemplazo de claves para sustituir int o string
    })
    .AddServer(options => // Configuración del servidor de autorización y emisión de tokens
    {
        options.SetTokenEndpointUris("/connect/token"); // Endpoint propio de OpenIddict para la emisión de tokens

        // Flujos que permites
        options.AllowPasswordFlow();          // login con user/pass
        options.AllowRefreshTokenFlow();      // habilita refresh tokens
        options.AcceptAnonymousClients();     // Autenticación de clientes anónimos
        options.AllowClientCredentialsFlow(); // opcional: si necesitas auth máquina-a-máquina
        options.AddDevelopmentEncryptionCertificate() // 
               .AddDevelopmentSigningCertificate();
        options.UseAspNetCore();
        options.AddEventHandler<OpenIddictServerEvents.HandleTokenRequestContext>(builder =>
        {
            builder.UseScopedHandler<PasswordGrantHandler>();
        });
        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(30)); // access token válido por 30 min
        options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));    // refresh token válido por 7 días
    });


var app = builder.Build();

// Middleware
app.UseHttpsRedirection();
app.UseRouting();              // asegura el routing base
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseFastEndpoints();

app.Run();
