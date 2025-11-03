using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_RSV.Migrations
{
    /// <inheritdoc />
    public partial class NuevaDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasAviso",
                columns: table => new
                {
                    CategoriaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasAviso", x => x.CategoriaID);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                columns: table => new
                {
                    PersonaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personas", x => x.PersonaID);
                });

            migrationBuilder.CreateTable(
                name: "TiposAmenidad",
                columns: table => new
                {
                    TipoAmenidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HorarioInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HorarioFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposAmenidad", x => x.TipoAmenidadID);
                });

            migrationBuilder.CreateTable(
                name: "TiposReporte",
                columns: table => new
                {
                    TipoReporteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposReporte", x => x.TipoReporteID);
                });

            migrationBuilder.CreateTable(
                name: "TiposServicio",
                columns: table => new
                {
                    TipoServicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposServicio", x => x.TipoServicioID);
                });

            migrationBuilder.CreateTable(
                name: "TiposUsuario",
                columns: table => new
                {
                    TipoUsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposUsuario", x => x.TipoUsuarioID);
                });

            migrationBuilder.CreateTable(
                name: "PersonalMantenimiento",
                columns: table => new
                {
                    PersonalMantenimientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    Puesto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaContratacion = table.Column<DateOnly>(type: "date", nullable: false),
                    Sueldo = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TipoContrato = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Turno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    DiasLaborales = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    Notas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalMantenimiento", x => x.PersonalMantenimientoID);
                    table.ForeignKey(
                        name: "FK_PersonalMantenimiento_Personas_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Personas",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Amenidades",
                columns: table => new
                {
                    AmenidadID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoAmenidadID = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Capacidad = table.Column<int>(type: "int", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenidades", x => x.AmenidadID);
                    table.ForeignKey(
                        name: "FK_Amenidades_TiposAmenidad_TipoAmenidadID",
                        column: x => x.TipoAmenidadID,
                        principalTable: "TiposAmenidad",
                        principalColumn: "TipoAmenidadID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosCatalogo",
                columns: table => new
                {
                    ServicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TipoServicioID = table.Column<int>(type: "int", nullable: false),
                    NombreEncargado = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumeroServiciosCompletados = table.Column<int>(type: "int", nullable: false),
                    Disponible = table.Column<bool>(type: "bit", nullable: false),
                    NotasInternas = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosCatalogo", x => x.ServicioID);
                    table.ForeignKey(
                        name: "FK_ServiciosCatalogo_TiposServicio_TipoServicioID",
                        column: x => x.TipoServicioID,
                        principalTable: "TiposServicio",
                        principalColumn: "TipoServicioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonaID = table.Column<int>(type: "int", nullable: false),
                    FirebaseUID = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    TipoUsuarioID = table.Column<int>(type: "int", nullable: false),
                    NumeroCasa = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: true),
                    Calle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UltimoAcceso = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Activo = table.Column<bool>(type: "bit", nullable: false),
                    MotivoDesactivar = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioID);
                    table.ForeignKey(
                        name: "FK_Usuarios_Personas_PersonaID",
                        column: x => x.PersonaID,
                        principalTable: "Personas",
                        principalColumn: "PersonaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_TiposUsuario_TipoUsuarioID",
                        column: x => x.TipoUsuarioID,
                        principalTable: "TiposUsuario",
                        principalColumn: "TipoUsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertasPanico",
                columns: table => new
                {
                    AlertaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertasPanico", x => x.AlertaID);
                    table.ForeignKey(
                        name: "FK_AlertasPanico_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Avisos",
                columns: table => new
                {
                    AvisoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    CategoriaID = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaEvento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avisos", x => x.AvisoID);
                    table.ForeignKey(
                        name: "FK_Avisos_CategoriasAviso_CategoriaID",
                        column: x => x.CategoriaID,
                        principalTable: "CategoriasAviso",
                        principalColumn: "CategoriaID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Avisos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargosMantenimiento",
                columns: table => new
                {
                    CargoMantenimientoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: true),
                    PersonalMantenimientoID = table.Column<int>(type: "int", nullable: true),
                    Concepto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SaldoPendiente = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargosMantenimiento", x => x.CargoMantenimientoID);
                    table.ForeignKey(
                        name: "FK_CargosMantenimiento_PersonalMantenimiento_PersonalMantenimientoID",
                        column: x => x.PersonalMantenimientoID,
                        principalTable: "PersonalMantenimiento",
                        principalColumn: "PersonalMantenimientoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargosMantenimiento_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CuentaUsuario",
                columns: table => new
                {
                    CuentaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    NumeroTarjeta = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UltimosDigitos = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    FechaVencimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    SaldoMantenimiento = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SaldoServicios = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SaldoTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    UltimaActualizacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuentaUsuario", x => x.CuentaID);
                    table.ForeignKey(
                        name: "FK_CuentaUsuario_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invitados",
                columns: table => new
                {
                    InvitadoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    NombreInvitado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoPaternoInvitado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ApellidoMaternoInvitado = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CodigoQR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaEntrada = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaSalida = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invitados", x => x.InvitadoID);
                    table.ForeignKey(
                        name: "FK_Invitados_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarcadoresMapa",
                columns: table => new
                {
                    MarcadorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: true),
                    Latitud = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    Indicador = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Comentario = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarcadoresMapa", x => x.MarcadorID);
                    table.ForeignKey(
                        name: "FK_MarcadoresMapa_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "QRPersonales",
                columns: table => new
                {
                    QRID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    CodigoQR = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FechaGeneracion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaVencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QRPersonales", x => x.QRID);
                    table.ForeignKey(
                        name: "FK_QRPersonales_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reportes",
                columns: table => new
                {
                    ReporteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: true),
                    TipoReporteID = table.Column<int>(type: "int", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Latitud = table.Column<decimal>(type: "decimal(10,8)", nullable: false),
                    Longitud = table.Column<decimal>(type: "decimal(11,8)", nullable: false),
                    DireccionTexto = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EsAnonimo = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Visto = table.Column<bool>(type: "bit", nullable: false),
                    Imagen = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reportes", x => x.ReporteID);
                    table.ForeignKey(
                        name: "FK_Reportes_TiposReporte_TipoReporteID",
                        column: x => x.TipoReporteID,
                        principalTable: "TiposReporte",
                        principalColumn: "TipoReporteID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reportes_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    ReservaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    AmenidadID = table.Column<int>(type: "int", nullable: false),
                    FechaReserva = table.Column<DateOnly>(type: "date", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.ReservaID);
                    table.ForeignKey(
                        name: "FK_Reservas_Amenidades_AmenidadID",
                        column: x => x.AmenidadID,
                        principalTable: "Amenidades",
                        principalColumn: "AmenidadID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservas_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SolicitudesServicio",
                columns: table => new
                {
                    SolicitudID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    TipoServicioID = table.Column<int>(type: "int", nullable: false),
                    PersonaAsignado = table.Column<int>(type: "int", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Urgencia = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaPreferida = table.Column<DateOnly>(type: "date", nullable: true),
                    HoraPreferida = table.Column<TimeSpan>(type: "time", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCompletado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NotasAdmin = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitudesServicio", x => x.SolicitudID);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_ServiciosCatalogo_PersonaAsignado",
                        column: x => x.PersonaAsignado,
                        principalTable: "ServiciosCatalogo",
                        principalColumn: "ServicioID");
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_TiposServicio_TipoServicioID",
                        column: x => x.TipoServicioID,
                        principalTable: "TiposServicio",
                        principalColumn: "TipoServicioID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SolicitudesServicio_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagos",
                columns: table => new
                {
                    PagoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    CargoMantenimientoID = table.Column<int>(type: "int", nullable: true),
                    FolioUnico = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    TipoPago = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FechaPago = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MetodoPago = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UltimosDigitosTarjeta = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagos", x => x.PagoID);
                    table.ForeignKey(
                        name: "FK_Pagos_CargosMantenimiento_CargoMantenimientoID",
                        column: x => x.CargoMantenimientoID,
                        principalTable: "CargosMantenimiento",
                        principalColumn: "CargoMantenimientoID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagos_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CargosServicios",
                columns: table => new
                {
                    CargoServicioID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioID = table.Column<int>(type: "int", nullable: false),
                    SolicitudID = table.Column<int>(type: "int", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MontoPagado = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    SaldoPendiente = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CargosServicios", x => x.CargoServicioID);
                    table.ForeignKey(
                        name: "FK_CargosServicios_SolicitudesServicio_SolicitudID",
                        column: x => x.SolicitudID,
                        principalTable: "SolicitudesServicio",
                        principalColumn: "SolicitudID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CargosServicios_Usuarios_UsuarioID",
                        column: x => x.UsuarioID,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DetallePago",
                columns: table => new
                {
                    DetalleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PagoID = table.Column<int>(type: "int", nullable: false),
                    TipoCargo = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CargoID = table.Column<int>(type: "int", nullable: false),
                    MontoAplicado = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    FechaAplicacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetallePago", x => x.DetalleID);
                    table.ForeignKey(
                        name: "FK_DetallePago_Pagos_PagoID",
                        column: x => x.PagoID,
                        principalTable: "Pagos",
                        principalColumn: "PagoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertasPanico_UsuarioID",
                table: "AlertasPanico",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Amenidades_TipoAmenidadID",
                table: "Amenidades",
                column: "TipoAmenidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Avisos_CategoriaID",
                table: "Avisos",
                column: "CategoriaID");

            migrationBuilder.CreateIndex(
                name: "IX_Avisos_UsuarioID",
                table: "Avisos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_CargosMantenimiento_PersonalMantenimientoID",
                table: "CargosMantenimiento",
                column: "PersonalMantenimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_CargosMantenimiento_UsuarioID",
                table: "CargosMantenimiento",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_CargosServicios_SolicitudID",
                table: "CargosServicios",
                column: "SolicitudID");

            migrationBuilder.CreateIndex(
                name: "IX_CargosServicios_UsuarioID",
                table: "CargosServicios",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasAviso_Nombre",
                table: "CategoriasAviso",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CuentaUsuario_UsuarioID",
                table: "CuentaUsuario",
                column: "UsuarioID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DetallePago_PagoID",
                table: "DetallePago",
                column: "PagoID");

            migrationBuilder.CreateIndex(
                name: "IX_Invitados_CodigoQR",
                table: "Invitados",
                column: "CodigoQR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invitados_UsuarioID",
                table: "Invitados",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_MarcadoresMapa_UsuarioID",
                table: "MarcadoresMapa",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_CargoMantenimientoID",
                table: "Pagos",
                column: "CargoMantenimientoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_FolioUnico",
                table: "Pagos",
                column: "FolioUnico",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pagos_UsuarioID",
                table: "Pagos",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalMantenimiento_PersonaID",
                table: "PersonalMantenimiento",
                column: "PersonaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QRPersonales_CodigoQR",
                table: "QRPersonales",
                column: "CodigoQR",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QRPersonales_UsuarioID",
                table: "QRPersonales",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_TipoReporteID",
                table: "Reportes",
                column: "TipoReporteID");

            migrationBuilder.CreateIndex(
                name: "IX_Reportes_UsuarioID",
                table: "Reportes",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_AmenidadID",
                table: "Reservas",
                column: "AmenidadID");

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_UsuarioID",
                table: "Reservas",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosCatalogo_TipoServicioID",
                table: "ServiciosCatalogo",
                column: "TipoServicioID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_PersonaAsignado",
                table: "SolicitudesServicio",
                column: "PersonaAsignado");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_TipoServicioID",
                table: "SolicitudesServicio",
                column: "TipoServicioID");

            migrationBuilder.CreateIndex(
                name: "IX_SolicitudesServicio_UsuarioID",
                table: "SolicitudesServicio",
                column: "UsuarioID");

            migrationBuilder.CreateIndex(
                name: "IX_TiposReporte_Nombre",
                table: "TiposReporte",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposServicio_Nombre",
                table: "TiposServicio",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TiposUsuario_Nombre",
                table: "TiposUsuario",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FirebaseUID",
                table: "Usuarios",
                column: "FirebaseUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PersonaID",
                table: "Usuarios",
                column: "PersonaID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioID",
                table: "Usuarios",
                column: "TipoUsuarioID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertasPanico");

            migrationBuilder.DropTable(
                name: "Avisos");

            migrationBuilder.DropTable(
                name: "CargosServicios");

            migrationBuilder.DropTable(
                name: "CuentaUsuario");

            migrationBuilder.DropTable(
                name: "DetallePago");

            migrationBuilder.DropTable(
                name: "Invitados");

            migrationBuilder.DropTable(
                name: "MarcadoresMapa");

            migrationBuilder.DropTable(
                name: "QRPersonales");

            migrationBuilder.DropTable(
                name: "Reportes");

            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "CategoriasAviso");

            migrationBuilder.DropTable(
                name: "SolicitudesServicio");

            migrationBuilder.DropTable(
                name: "Pagos");

            migrationBuilder.DropTable(
                name: "TiposReporte");

            migrationBuilder.DropTable(
                name: "Amenidades");

            migrationBuilder.DropTable(
                name: "ServiciosCatalogo");

            migrationBuilder.DropTable(
                name: "CargosMantenimiento");

            migrationBuilder.DropTable(
                name: "TiposAmenidad");

            migrationBuilder.DropTable(
                name: "TiposServicio");

            migrationBuilder.DropTable(
                name: "PersonalMantenimiento");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Personas");

            migrationBuilder.DropTable(
                name: "TiposUsuario");
        }
    }
}
