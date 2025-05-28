using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSismica.Migrations
{
    public partial class AddSismografoIdToEstaciones : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ambito = table.Column<string>(type: "TEXT", nullable: true),
                    nombreEstado = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotivosTipo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    tipoMotivo = table.Column<string>(type: "TEXT", nullable: true),
                    Descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosTipo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: true),
                    descripcion = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Estaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    codigoEstacion = table.Column<string>(type: "TEXT", nullable: true),
                    nombre = table.Column<string>(type: "TEXT", nullable: true),
                    fechaSolicitudCertificacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    documentoCertificacionAdquirida = table.Column<string>(type: "TEXT", nullable: true),
                    nroCertificacionAdquisicion = table.Column<string>(type: "TEXT", nullable: true),
                    latitud = table.Column<double>(type: "REAL", nullable: false),
                    longitud = table.Column<double>(type: "REAL", nullable: false),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    SismografoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estaciones_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotivosFueraServicio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    comentario = table.Column<string>(type: "TEXT", nullable: true),
                    MotivoTipoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosFueraServicio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotivosFueraServicio_MotivosTipo_MotivoTipoId",
                        column: x => x.MotivoTipoId,
                        principalTable: "MotivosTipo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombre = table.Column<string>(type: "TEXT", nullable: true),
                    apellido = table.Column<string>(type: "TEXT", nullable: true),
                    mail = table.Column<string>(type: "TEXT", nullable: true),
                    telefono = table.Column<string>(type: "TEXT", nullable: true),
                    RolId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Empleados_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sismografos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdentificacionSismografo = table.Column<string>(type: "TEXT", nullable: true),
                    nroSerie = table.Column<string>(type: "TEXT", nullable: true),
                    fechaAdquisicion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EstacionSismologicaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sismografos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sismografos_Estaciones_EstacionSismologicaId",
                        column: x => x.EstacionSismologicaId,
                        principalTable: "Estaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sismografos_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesInspeccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nroOrden = table.Column<string>(type: "TEXT", nullable: true),
                    fechaHoraInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaHoraFinalizacion = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaHoraCierre = table.Column<DateTime>(type: "TEXT", nullable: true),
                    ObservacionCierre = table.Column<string>(type: "TEXT", nullable: true),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    EstacionSismologicaId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesInspeccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesInspeccion_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrdenesInspeccion_Estaciones_EstacionSismologicaId",
                        column: x => x.EstacionSismologicaId,
                        principalTable: "Estaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesInspeccion_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    nombreUsuario = table.Column<string>(type: "TEXT", nullable: true),
                    contraseña = table.Column<string>(type: "TEXT", nullable: true),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CambiosEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fechaHoraInicio = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaHoraFin = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    MotivoFueraServicioId = table.Column<int>(type: "INTEGER", nullable: true),
                    EmpleadoId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrdenDeInspeccionId = table.Column<int>(type: "INTEGER", nullable: false),
                    SismografoId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CambiosEstado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_Empleados_EmpleadoId",
                        column: x => x.EmpleadoId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_MotivosFueraServicio_MotivoFueraServicioId",
                        column: x => x.MotivoFueraServicioId,
                        principalTable: "MotivosFueraServicio",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_OrdenesInspeccion_OrdenDeInspeccionId",
                        column: x => x.OrdenDeInspeccionId,
                        principalTable: "OrdenesInspeccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_Sismografos_SismografoId",
                        column: x => x.SismografoId,
                        principalTable: "Sismografos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sesiones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    fechaHoraDesde = table.Column<DateTime>(type: "TEXT", nullable: false),
                    fechaHoraHasta = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UsuarioId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sesiones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sesiones_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_EmpleadoId",
                table: "CambiosEstado",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_EstadoId",
                table: "CambiosEstado",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_MotivoFueraServicioId",
                table: "CambiosEstado",
                column: "MotivoFueraServicioId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_OrdenDeInspeccionId",
                table: "CambiosEstado",
                column: "OrdenDeInspeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_SismografoId",
                table: "CambiosEstado",
                column: "SismografoId");

            migrationBuilder.CreateIndex(
                name: "IX_Empleados_RolId",
                table: "Empleados",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Estaciones_EstadoId",
                table: "Estaciones",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_MotivosFueraServicio_MotivoTipoId",
                table: "MotivosFueraServicio",
                column: "MotivoTipoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesInspeccion_EmpleadoId",
                table: "OrdenesInspeccion",
                column: "EmpleadoId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesInspeccion_EstacionSismologicaId",
                table: "OrdenesInspeccion",
                column: "EstacionSismologicaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesInspeccion_EstadoId",
                table: "OrdenesInspeccion",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Sesiones_UsuarioId",
                table: "Sesiones",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Sismografos_EstacionSismologicaId",
                table: "Sismografos",
                column: "EstacionSismologicaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sismografos_EstadoId",
                table: "Sismografos",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpleadoId",
                table: "Usuarios",
                column: "EmpleadoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CambiosEstado");

            migrationBuilder.DropTable(
                name: "Sesiones");

            migrationBuilder.DropTable(
                name: "MotivosFueraServicio");

            migrationBuilder.DropTable(
                name: "OrdenesInspeccion");

            migrationBuilder.DropTable(
                name: "Sismografos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "MotivosTipo");

            migrationBuilder.DropTable(
                name: "Estaciones");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
