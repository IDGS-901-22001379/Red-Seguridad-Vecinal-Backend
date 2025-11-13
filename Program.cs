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

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

// Database Context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));

<<<<<<< HEAD
// CONFIGURAR FIREBASE
var firebasePath = Path.Combine(Directory.GetCurrentDirectory(), "Firebase-Credentials", "firebase-adminsdk.json");
if (File.Exists(firebasePath))
{
    FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(firebasePath),
    });
    Console.WriteLine("✅ Firebase configurado correctamente");
}
else
{
    Console.WriteLine($"⚠️  Archivo firebase-adminsdk.json no encontrado en: {firebasePath}");
    Console.WriteLine("⚠️  Las notificaciones funcionarán en modo simulación");
}


builder.Services.AddScoped<AlertaPanicoData>();
builder.Services.AddScoped<FirebaseNotificationService>();
builder.Services.AddScoped<ReporteData>();
=======
builder.Services.AddScoped<UsuariosData>();
builder.Services.AddScoped<AvisosData>();
builder.Services.AddScoped<PagosData>();

Backend_RSV.Config.FirebaseInitializer.Initialize();

builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevaPolitica", app =>
    {
        app.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
    .AddEnvironmentVariables();
>>>>>>> origin/oscar

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
<<<<<<< HEAD
=======

app.UseCors("NuevaPolitica");

>>>>>>> origin/oscar
app.UseAuthorization();
app.MapControllers();

app.Run();