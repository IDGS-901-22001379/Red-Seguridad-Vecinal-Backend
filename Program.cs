using Backend_RSV.Controllers.Pagos;
using Backend_RSV.Data.Avisos;
using Backend_RSV.Data.Usuarios;
using MiApi.Data;
using Microsoft.EntityFrameworkCore;
using Backend_RSV.Data.Alertas;
using Backend_RSV.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Backend_RSV.Data.Reportes;
using Backend_RSV.Data.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Controllers + JSON (evita ciclos)
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// OpenAPI / Swagger
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// DbContext (SQL Server)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

// --- Firebase (SIN subir llaves al repo) ---
try
{
    // Si ya existe, no volver a crear
    var _ = FirebaseApp.DefaultInstance;
}
catch (InvalidOperationException)
{
    var credPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");
    if (!string.IsNullOrWhiteSpace(credPath) && File.Exists(credPath))
    {
        FirebaseApp.Create(new AppOptions
        {
            Credential = GoogleCredential.FromFile(credPath)
        });
        Console.WriteLine("✅ Firebase configurado desde GOOGLE_APPLICATION_CREDENTIALS.");
    }
    else
    {
        Console.WriteLine("⚠️  No se encontró GOOGLE_APPLICATION_CREDENTIALS o el archivo no existe. " +
                          "Las funciones que dependen de Firebase se ejecutarán sin credenciales.");
    }
}

// Servicios / Data
builder.Services.AddScoped<AlertaPanicoData>();
builder.Services.AddScoped<FirebaseNotificationService>();
builder.Services.AddScoped<ReporteData>();
builder.Services.AddScoped<UsuariosData>();
builder.Services.AddScoped<AvisosData>();
builder.Services.AddScoped<PagosData>();
builder.Services.AddScoped<ServiciosData>();

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

// Config extra (opcional: ya carga appsettings.json por defecto)
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();

var app = builder.Build();

// Pipeline
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
