[1mdiff --git a/Backend-RSV.csproj b/Backend-RSV.csproj[m
[1mindex 704535a..f089702 100644[m
[1m--- a/Backend-RSV.csproj[m
[1m+++ b/Backend-RSV.csproj[m
[36m@@ -20,9 +20,13 @@[m
     <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.10" />[m
     <PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.10" />[m
     <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.10">[m
[31m-      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>[m
       <PrivateAssets>all</PrivateAssets>[m
[32m+[m[32m      <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>[m
     </PackageReference>[m
[32m+[m
[32m+[m[32m    <!-- QuestPDF debe ir como referencia separada -->[m
[32m+[m[32m    <PackageReference Include="QuestPDF" Version="2025.7.4" />[m
[32m+[m
     <PackageReference Include="QRCoder" Version="1.7.0" />[m
     <PackageReference Include="Swashbuckle.AspNetCore" Version="9.0.6" />[m
   </ItemGroup>[m
[1mdiff --git a/Controllers/Estadisticas/EstadisticasController.cs b/Controllers/Estadisticas/EstadisticasController.cs[m
[1mindex f8da3b7..8182c31 100644[m
[1m--- a/Controllers/Estadisticas/EstadisticasController.cs[m
[1m+++ b/Controllers/Estadisticas/EstadisticasController.cs[m
[36m@@ -6,6 +6,7 @@[m [musing QuestPDF.Helpers;[m
 using QuestPDF.Infrastructure;[m
 using Microsoft.AspNetCore.Mvc;[m
 [m
[32m+[m
 namespace Backend_RSV.Controllers.Estadisticas[m
 {[m
     [ApiController][m
[1mdiff --git a/Program.cs b/Program.cs[m
[1mindex 3307195..659f7f3 100644[m
[1m--- a/Program.cs[m
[1m+++ b/Program.cs[m
[36m@@ -1,70 +1,108 @@[m
[31m-using MiApi.Data;[m
[32m+[m[32musing System.IO;[m
 using Microsoft.EntityFrameworkCore;[m
[32m+[m[32musing MiApi.Data;[m
[32m+[m
[32m+[m[32m// =====  Landin =====[m
 using Backend_RSV.Data.Alertas;[m
[31m-using Backend_RSV.Services;[m
[31m-using FirebaseAdmin;[m
[31m-using Google.Apis.Auth.OAuth2;[m
[31m-using System.IO;[m
 using Backend_RSV.Data.Reportes;[m
 using Backend_RSV.Data.Servicios;[m
 using Backend_RSV.Data.Invitados;[m
[32m+[m[32musing Backend_RSV.Services;                  // FirebaseNotificationService, IFirebaseDataService/FirebaseDataService, QrService, FirebaseInitializer[m
[32m+[m
[32m+[m[32m// ===== Data de Óscar =====[m
[32m+[m[32musing Backend_RSV.Data.Usuarios;[m
[32m+[m[32musing Backend_RSV.Data.Avisos;[m
[32m+[m[32musing Backend_RSV.Data.Estadisticas;[m
[32m+[m[32musing Backend_RSV.Data.Mapa;[m
[32m+[m[32musing Backend_RSV.Controllers.Pagos;[m
[32m+[m
[32m+[m
[32m+[m
 [m
 var builder = WebApplication.CreateBuilder(args);[m
 [m
[32m+[m[32m// -------------------------[m
[32m+[m[32m// Configuración (appsettings + env)[m
[32m+[m[32m// -------------------------[m
[32m+[m[32mbuilder.Configuration[m
[32m+[m[32m    .SetBasePath(Directory.GetCurrentDirectory())[m
[32m+[m[32m    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)[m
[32m+[m[32m    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)[m
[32m+[m[32m    .AddEnvironmentVariables();[m
[32m+[m
[32m+[m[32m// -------------------------[m
[32m+[m[32m// Servicios base[m
[32m+[m[32m// -------------------------[m
 builder.Services.AddControllers()[m
     .AddJsonOptions(options =>[m
     {[m
[31m-        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;[m
[32m+[m[32m        options.JsonSerializerOptions.ReferenceHandler =[m
[32m+[m[32m            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;[m
     });[m
 [m
 builder.Services.AddOpenApi();[m
 builder.Services.AddSwaggerGen();[m
 [m
[31m-// Database Context[m
[32m+[m[32m// DB Context[m
 builder.Services.AddDbContext<AppDbContext>(options =>[m
     options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL")));[m
 [m
[31m-// CONFIGURAR FIREBASE[m
[31m-var firebasePath = Path.Combine(Directory.GetCurrentDirectory(),"firebase-adminsdk.json");[m
[31m-if (File.Exists(firebasePath))[m
[32m+[m[32m// CORS (estándar del repo de Óscar)[m
[32m+[m[32mbuilder.Services.AddCors(options =>[m
 {[m
[31m-    FirebaseApp.Create(new AppOptions()[m
[32m+[m[32m    options.AddPolicy("NuevaPolitica", app =>[m
     {[m
[31m-        Credential = GoogleCredential.FromFile(firebasePath),[m
[32m+[m[32m        app.AllowAnyOrigin()[m
[32m+[m[32m           .AllowAnyHeader()[m
[32m+[m[32m           .AllowAnyMethod();[m
     });[m
[31m-    Console.WriteLine("✅ Firebase configurado correctamente");[m
[31m-}[m
[31m-else[m
[31m-{[m
[31m-    Console.WriteLine($"⚠️  Archivo firebase-adminsdk.json no encontrado en: {firebasePath}");[m
[31m-    Console.WriteLine("⚠️  Las notificaciones funcionarán en modo simulación");[m
[31m-}[m
[32m+[m[32m});[m
 [m
[32m+[m[32m// -------------------------[m
[32m+[m[32m// DI: Registrar TODAS las Data/Servicios (sin duplicar)[m
[32m+[m[32m// -------------------------[m
[32m+[m[32m// Óscar[m
[32m+[m[32mbuilder.Services.AddScoped<UsuariosData>();[m
[32m+[m[32mbuilder.Services.AddScoped<AvisosData>();[m
[32m+[m[32mbuilder.Services.AddScoped<MapaData>();[m
[32m+[m[32mbuilder.Services.AddScoped<EstadisticasData>();[m
[32m+[m[32mbuilder.Services.AddScoped<PagosData>();[m
 [m
[32m+[m[32m// / Landin[m
 builder.Services.AddScoped<AlertaPanicoData>();[m
[31m-builder.Services.AddScoped<FirebaseNotificationService>();[m
 builder.Services.AddScoped<ReporteData>();[m
 builder.Services.AddScoped<ServiciosData>();[m
[31m-builder.Services.AddScoped<IFirebaseDataService, FirebaseDataService>();[m
 builder.Services.AddScoped<InvitadosData>();[m
[32m+[m
[32m+[m
[32m+[m[32mbuilder.Services.AddScoped<FirebaseNotificationService>();[m
[32m+[m[32mbuilder.Services.AddScoped<IFirebaseDataService, FirebaseDataService>();[m
 builder.Services.AddScoped<QrService>();[m
[32m+[m
[32m+[m[32m// -------------------------[m
[32m+[m[32m// Firebase (tu inicializador; mantiene fallback a env/appsettings/archivo)[m
[32m+[m[32m// -------------------------[m
[32m+[m[32mFirebaseInitializer.Initialize(); // usa tu clase existente[m
[32m+[m
 var app = builder.Build();[m
 [m
[31m-// Configure the HTTP request pipeline.[m
[32m+[m[32m// -------------------------[m
[32m+[m[32m// Pipeline[m
[32m+[m[32m// -------------------------[m
 if (app.Environment.IsDevelopment())[m
 {[m
[31m-    using (var scope = app.Services.CreateScope())[m
[31m-    {[m
[31m-        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();[m
[31m-        db.Database.Migrate();[m
[31m-    }[m
[32m+[m[32m    // Migraciones automáticas en desarrollo[m
[32m+[m[32m    using var scope = app.Services.CreateScope();[m
[32m+[m[32m    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();[m
[32m+[m[32m    db.Database.Migrate();[m
[32m+[m
     app.MapOpenApi();[m
     app.UseSwagger();[m
     app.UseSwaggerUI();[m
 }[m
 [m
 app.UseHttpsRedirection();[m
[32m+[m[32mapp.UseCors("NuevaPolitica");[m
 app.UseAuthorization();[m
 app.MapControllers();[m
[31m-[m
[31m-app.Run();[m
\ No newline at end of file[m
[32m+[m[32mapp.Run();[m
[1mdiff --git a/Services/FirebaseInitializer.cs b/Services/FirebaseInitializer.cs[m
[1mindex 170feb0..ef9e7ae 100644[m
[1m--- a/Services/FirebaseInitializer.cs[m
[1m+++ b/Services/FirebaseInitializer.cs[m
[36m@@ -1,22 +1,58 @@[m
 using FirebaseAdmin;[m
 using Google.Apis.Auth.OAuth2;[m
[32m+[m[32musing Microsoft.Extensions.Configuration;[m
[32m+[m[32musing System;[m
[32m+[m[32musing System.IO;[m
 [m
[31m-namespace Backend_RSV.Config[m
[32m+[m[32mnamespace Backend_RSV.Services   // <-- OJO: Services (no Config)[m
 {[m
     public static class FirebaseInitializer[m
     {[m
[31m-        private static bool _initialized = false;[m
[31m-        public static void Initialize()[m
[32m+[m[32m        private static bool _initialized;[m
[32m+[m
[32m+[m[32m        // Overload con IConfiguration (recomendado)[m
[32m+[m[32m        public static void Initialize(IConfiguration config)[m
         {[m
[31m-            if (!_initialized)[m
[32m+[m[32m            if (_initialized || FirebaseApp.DefaultInstance != null) return;[m
[32m+[m
[32m+[m[32m            // Lee ruta desde appsettings o usa env var GOOGLE_APPLICATION_CREDENTIALS[m
[32m+[m[32m            var cfgPath = config["Firebase:CredentialsPath"];[m
[32m+[m[32m            var envPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");[m
[32m+[m
[32m+[m[32m            string? finalPath = null;[m
[32m+[m[32m            if (!string.IsNullOrWhiteSpace(envPath) && File.Exists(envPath))[m
             {[m
[31m-                FirebaseApp.Create(new AppOptions()[m
[32m+[m[32m                finalPath = envPath;[m
[32m+[m[32m            }[m
[32m+[m[32m            else if (!string.IsNullOrWhiteSpace(cfgPath))[m
[32m+[m[32m            {[m
[32m+[m[32m                var abs = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), cfgPath));[m
[32m+[m[32m                if (File.Exists(abs))[m
                 {[m
[31m-                    Credential = GoogleCredential.FromFile("firebase-key.json") // ruta a tu archivo JSON de credenciales[m
[31m-                });[m
[32m+[m[32m                    finalPath = abs;[m
[32m+[m[32m                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", abs);[m
[32m+[m[32m                }[m
[32m+[m[32m            }[m
 [m
[32m+[m[32m            try[m
[32m+[m[32m            {[m
[32m+[m[32m                var cred = GoogleCredential.GetApplicationDefault(); // no deprecado[m
[32m+[m[32m                FirebaseApp.Create(new AppOptions { Credential = cred });[m
[32m+[m[32m                Console.WriteLine(finalPath != null[m
[32m+[m[32m                    ? $"✅ Firebase inicializado con credenciales: {finalPath}"[m
[32m+[m[32m                    : "✅ Firebase inicializado con credenciales por defecto del entorno");[m
                 _initialized = true;[m
             }[m
[32m+[m[32m            catch (Exception)[m
[32m+[m[32m            {[m
[32m+[m[32m                Console.WriteLine("⚠️ No se encontró credencial de Firebase. Modo simulación.");[m
[32m+[m[32m            }[m
[32m+[m[32m        }[m
[32m+[m
[32m+[m[32m        // Overload sin parámetros (opcional) por si lo llamas sin config[m
[32m+[m[32m        public static void Initialize()[m
[32m+[m[32m        {[m
[32m+[m[32m            Initialize(new ConfigurationBuilder().Build());[m
         }[m
     }[m
 }[m
[1mdiff --git a/appsettings.json b/appsettings.json[m
[1mindex 2b213c6..8da4767 100644[m
[1m--- a/appsettings.json[m
[1m+++ b/appsettings.json[m
[36m@@ -3,6 +3,6 @@[m
     "CadenaSQL": "Server=.\\SQLEXPRESS;Database=Red_Seguridad_Vecinal;Trusted_Connection=True;TrustServerCertificate=True"[m
   },[m
   "Firebase": {[m
[31m-    "ServiceAccountPath": "firebase-credentials\\red-seguridad-vecinal-firebase-adminsdk-fbsvc-6685565ddc.json"[m
[32m+[m[32m    "CredentialsPath": "firebase-credentials\\red-seguridad-vecinal-firebase-adminsdk-fbsvc-6685565ddc.json"[m
   }[m
 }[m
