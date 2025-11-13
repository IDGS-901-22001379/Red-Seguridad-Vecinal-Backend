using Microsoft.EntityFrameworkCore;
using MiApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend_RSV.Data.Reportes
{
    public class ReporteData
    {
        private readonly AppDbContext _context;

        public ReporteData(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ReporteDTO>> GetReportesAsync()
        {
            return await _context.Reportes
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Persona)
                .Include(r => r.TipoReporte)
                .OrderByDescending(r => r.FechaCreacion)
                .Select(r => new ReporteDTO
                {
                    ReporteID = r.ReporteID,
                    UsuarioID = r.UsuarioID,
                    TipoReporteID = r.TipoReporteID,
                    Titulo = r.Titulo,
                    Descripcion = r.Descripcion,
                    Latitud = r.Latitud,
                    Longitud = r.Longitud,
                    DireccionTexto = r.DireccionTexto,
                    EsAnonimo = r.EsAnonimo,
                    FechaCreacion = r.FechaCreacion,
                    Visto = r.Visto,
                    Imagen = r.Imagen,
                    NombreUsuario = r.EsAnonimo ? "Anónimo" : 
                                   (r.Usuario != null ? 
                                       r.Usuario.Persona.Nombre + " " + r.Usuario.Persona.ApellidoPaterno 
                                   : "Usuario"),
                    TipoReporte = r.TipoReporte.Nombre
                })
                .ToListAsync();
        }

        public async Task<ReporteDetailDTO?> GetReporteByIdAsync(int id)
        {
            return await _context.Reportes
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Persona)
                .Include(r => r.TipoReporte)
                .Where(r => r.ReporteID == id)
                .Select(r => new ReporteDetailDTO
                {
                    ReporteID = r.ReporteID,
                    UsuarioID = r.UsuarioID,
                    TipoReporteID = r.TipoReporteID,
                    Titulo = r.Titulo,
                    Descripcion = r.Descripcion,
                    Latitud = r.Latitud,
                    Longitud = r.Longitud,
                    DireccionTexto = r.DireccionTexto,
                    EsAnonimo = r.EsAnonimo,
                    FechaCreacion = r.FechaCreacion,
                    Visto = r.Visto,
                    Imagen = r.Imagen,
                    NombreCompleto = r.EsAnonimo ? "Anónimo" : 
                                    (r.Usuario != null ? 
                                        r.Usuario.Persona.Nombre + " " + 
                                        r.Usuario.Persona.ApellidoPaterno + " " + 
                                        (r.Usuario.Persona.ApellidoMaterno ?? "") 
                                    : "Usuario"),
                    Email = r.EsAnonimo ? "" : (r.Usuario != null && r.Usuario.Persona != null ? r.Usuario.Persona.Email : ""),
                    Telefono = r.EsAnonimo ? "" : (r.Usuario != null && r.Usuario.Persona != null ? r.Usuario.Persona.Telefono : ""),
                    TipoReporte = r.TipoReporte.Nombre,
                    NumeroCasa = r.EsAnonimo ? "" : (r.Usuario != null ? r.Usuario.NumeroCasa ?? "" : ""),
                    Calle = r.EsAnonimo ? "" : (r.Usuario != null ? r.Usuario.Calle ?? "" : "")
                })
                .FirstOrDefaultAsync();
        }

        public async Task<List<ReporteDTO>> GetReportesByUsuarioAsync(int usuarioId)
        {
            return await _context.Reportes
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Persona)
                .Include(r => r.TipoReporte)
                .Where(r => r.UsuarioID == usuarioId)
                .OrderByDescending(r => r.FechaCreacion)
                .Select(r => new ReporteDTO
                {
                    ReporteID = r.ReporteID,
                    UsuarioID = r.UsuarioID,
                    TipoReporteID = r.TipoReporteID,
                    Titulo = r.Titulo,
                    Descripcion = r.Descripcion,
                    Latitud = r.Latitud,
                    Longitud = r.Longitud,
                    DireccionTexto = r.DireccionTexto,
                    EsAnonimo = r.EsAnonimo,
                    FechaCreacion = r.FechaCreacion,
                    Visto = r.Visto,
                    Imagen = r.Imagen,
                    NombreUsuario = r.Usuario != null ? r.Usuario.Persona.Nombre + " " + r.Usuario.Persona.ApellidoPaterno : "Usuario",
                    TipoReporte = r.TipoReporte.Nombre
                })
                .ToListAsync();
        }

        public async Task<Reporte> CreateReporteAsync(ReporteRequest request)
        {
            var reporte = new Reporte
            {
                UsuarioID = request.EsAnonimo ? null : request.UsuarioID,
                TipoReporteID = request.TipoReporteID,
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Latitud = request.Latitud,
                Longitud = request.Longitud,
                DireccionTexto = request.DireccionTexto,
                EsAnonimo = request.EsAnonimo,
                FechaCreacion = DateTime.Now,
                Visto = false,
                Imagen = request.ImagenBase64
            };

            _context.Reportes.Add(reporte);
            await _context.SaveChangesAsync();

            await _context.Entry(reporte)
                .Reference(r => r.Usuario)
                .Query()
                .Include(u => u.Persona)
                .LoadAsync();

            await _context.Entry(reporte)
                .Reference(r => r.TipoReporte)
                .LoadAsync();

            return reporte;
        }

        public async Task<Reporte?> UpdateReporteEstadoAsync(int id, bool visto)
        {
            var reporte = await _context.Reportes
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Persona)
                .Include(r => r.TipoReporte)
                .FirstOrDefaultAsync(r => r.ReporteID == id);

            if (reporte is null)
                return null;

            reporte.Visto = visto;
            await _context.SaveChangesAsync();

            return reporte;
        }

        public async Task<Reporte?> CambiarAnonimatoAsync(int id, bool esAnonimo)
        {
            var reporte = await _context.Reportes
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Persona)
                .Include(r => r.TipoReporte)
                .FirstOrDefaultAsync(r => r.ReporteID == id);

            if (reporte is null)
                return null;

            reporte.EsAnonimo = esAnonimo;
            if (esAnonimo)
            {
                reporte.UsuarioID = null;
            }

            await _context.SaveChangesAsync();
            return reporte;
        }

        public async Task<List<TipoReporte>> GetTiposReporteAsync()
        {
            return await _context.TiposReporte
                .Where(t => t.Activo)
                .ToListAsync();
        }
    }

    // DTOs y Request classes
    public class ReporteDTO
    {
        public int ReporteID { get; set; }
        public int? UsuarioID { get; set; }
        public int TipoReporteID { get; set; }
        public string? Titulo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? DireccionTexto { get; set; }
        public bool EsAnonimo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Visto { get; set; }
        public string? Imagen { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public string TipoReporte { get; set; } = string.Empty;
    }

    public class ReporteDetailDTO
    {
        public int ReporteID { get; set; }
        public int? UsuarioID { get; set; }
        public int TipoReporteID { get; set; }
        public string? Titulo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? DireccionTexto { get; set; }
        public bool EsAnonimo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Visto { get; set; }
        public string? Imagen { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string TipoReporte { get; set; } = string.Empty;
        public string NumeroCasa { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
    }

    public class ReporteRequest
    {
        public int? UsuarioID { get; set; }
        public int TipoReporteID { get; set; }
        public string? Titulo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public string? DireccionTexto { get; set; }
        public bool EsAnonimo { get; set; }
        public string? ImagenBase64 { get; set; }
    }

    public class CambiarAnonimatoRequest
    {
        public bool EsAnonimo { get; set; }
    }

    public class UpdateEstadoRequest
    {
        public bool Visto { get; set; }
    }
}