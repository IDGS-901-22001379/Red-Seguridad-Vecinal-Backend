using System.IO;
using Microsoft.EntityFrameworkCore;
using MiApi.Data;

// =====  Landin =====
using Backend_RSV.Data.Alertas;
using Backend_RSV.Data.Reportes;
using Backend_RSV.Data.Servicios;
using Backend_RSV.Data.Invitados;
using Backend_RSV.Services;                  // FirebaseNotificationService, IFirebaseDataService/FirebaseDataService, QrService, FirebaseInitializer

// ===== Data de Óscar =====
using Backend_RSV.Data.Usuarios;
using Backend_RSV.Data.Avisos;
using Backend_RSV.Data.Estadisticas;
using Backend_RSV.Data.Mapa;
using Backend_RSV.Controllers.Pagos;




var builder = WebApplication.CreateBuilder(args);

// -------------------------
// Configuración (appsettings + env)
// -------------------------
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

// -------------------------
// Servicios base
// -------------------------
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

// CORS (estándar del repo de Óscar)
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

// -------------------------
// DI: Registrar TODAS las Data/Servicios (sin duplicar)
// -------------------------
// Óscar
builder.Services.AddScoped<UsuariosData>();
builder.Services.AddScoped<AvisosData>();
builder.Services.AddScoped<MapaData>();
builder.Services.AddScoped<EstadisticasData>();
builder.Services.AddScoped<PagosData>();

// / Landin
builder.Services.AddScoped<AlertaPanicoData>();
builder.Services.AddScoped<ReporteData>();
builder.Services.AddScoped<ServiciosData>();
builder.Services.AddScoped<InvitadosData>();


builder.Services.AddScoped<FirebaseNotificationService>();
builder.Services.AddScoped<IFirebaseDataService, FirebaseDataService>();
builder.Services.AddScoped<QrService>();

// -------------------------
// Firebase (tu inicializador; mantiene fallback a env/appsettings/archivo)
// -------------------------
FirebaseInitializer.Initialize(); // usa tu clase existente

var app = builder.Build();

// -------------------------
// Pipeline
// -------------------------
if (app.Environment.IsDevelopment())
{
    // Migraciones automáticas en desarrollo
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("NuevaPolitica");
app.UseAuthorization();
app.MapControllers();
app.Run();
