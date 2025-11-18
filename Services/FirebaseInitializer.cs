using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Backend_RSV.Services   // <-- OJO: Services (no Config)
{
    public static class FirebaseInitializer
    {
        private static bool _initialized;

        // Overload con IConfiguration (recomendado)
        public static void Initialize(IConfiguration config)
        {
            if (_initialized || FirebaseApp.DefaultInstance != null) return;

            // Lee ruta desde appsettings o usa env var GOOGLE_APPLICATION_CREDENTIALS
            var cfgPath = config["Firebase:CredentialsPath"];
            var envPath = Environment.GetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS");

            string? finalPath = null;
            if (!string.IsNullOrWhiteSpace(envPath) && File.Exists(envPath))
            {
                finalPath = envPath;
            }
            else if (!string.IsNullOrWhiteSpace(cfgPath))
            {
                var abs = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), cfgPath));
                if (File.Exists(abs))
                {
                    finalPath = abs;
                    Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", abs);
                }
            }

            try
            {
                var cred = GoogleCredential.GetApplicationDefault(); // no deprecado
                FirebaseApp.Create(new AppOptions { Credential = cred });
                Console.WriteLine(finalPath != null
                    ? $"✅ Firebase inicializado con credenciales: {finalPath}"
                    : "✅ Firebase inicializado con credenciales por defecto del entorno");
                _initialized = true;
            }
            catch (Exception)
            {
                Console.WriteLine("⚠️ No se encontró credencial de Firebase. Modo simulación.");
            }
        }

        // Overload sin parámetros (opcional) por si lo llamas sin config
        public static void Initialize()
        {
            Initialize(new ConfigurationBuilder().Build());
        }
    }
}
