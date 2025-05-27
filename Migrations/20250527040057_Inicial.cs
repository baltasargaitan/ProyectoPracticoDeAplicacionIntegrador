using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSismica.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empleados",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    Rol = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sismografos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Identificador = table.Column<string>(type: "TEXT", nullable: true),
                    Estado = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sismografos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposMotivoBaja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposMotivoBaja", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CambiosEstado",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Estado = table.Column<string>(type: "TEXT", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SismografoId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponsableId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CambiosEstado", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_Empleados_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CambiosEstado_Sismografos_SismografoId",
                        column: x => x.SismografoId,
                        principalTable: "Sismografos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Estaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Nombre = table.Column<string>(type: "TEXT", nullable: true),
                    SismografoId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estaciones_Sismografos_SismografoId",
                        column: x => x.SismografoId,
                        principalTable: "Sismografos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrdenesInspeccion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ObservacionCierre = table.Column<string>(type: "TEXT", nullable: true),
                    EstaCerrada = table.Column<bool>(type: "INTEGER", nullable: false),
                    FechaCierre = table.Column<DateTime>(type: "TEXT", nullable: true),
                    EstacionSismologicaId = table.Column<int>(type: "INTEGER", nullable: false),
                    ResponsableId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdenesInspeccion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrdenesInspeccion_Empleados_ResponsableId",
                        column: x => x.ResponsableId,
                        principalTable: "Empleados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdenesInspeccion_Estaciones_EstacionSismologicaId",
                        column: x => x.EstacionSismologicaId,
                        principalTable: "Estaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotivosBaja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Comentario = table.Column<string>(type: "TEXT", nullable: true),
                    TipoMotivoBajaId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrdenInspeccionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivosBaja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotivosBaja_OrdenesInspeccion_OrdenInspeccionId",
                        column: x => x.OrdenInspeccionId,
                        principalTable: "OrdenesInspeccion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotivosBaja_TiposMotivoBaja_TipoMotivoBajaId",
                        column: x => x.TipoMotivoBajaId,
                        principalTable: "TiposMotivoBaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_ResponsableId",
                table: "CambiosEstado",
                column: "ResponsableId");

            migrationBuilder.CreateIndex(
                name: "IX_CambiosEstado_SismografoId",
                table: "CambiosEstado",
                column: "SismografoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estaciones_SismografoId",
                table: "Estaciones",
                column: "SismografoId");

            migrationBuilder.CreateIndex(
                name: "IX_MotivosBaja_OrdenInspeccionId",
                table: "MotivosBaja",
                column: "OrdenInspeccionId");

            migrationBuilder.CreateIndex(
                name: "IX_MotivosBaja_TipoMotivoBajaId",
                table: "MotivosBaja",
                column: "TipoMotivoBajaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesInspeccion_EstacionSismologicaId",
                table: "OrdenesInspeccion",
                column: "EstacionSismologicaId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdenesInspeccion_ResponsableId",
                table: "OrdenesInspeccion",
                column: "ResponsableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CambiosEstado");

            migrationBuilder.DropTable(
                name: "MotivosBaja");

            migrationBuilder.DropTable(
                name: "OrdenesInspeccion");

            migrationBuilder.DropTable(
                name: "TiposMotivoBaja");

            migrationBuilder.DropTable(
                name: "Empleados");

            migrationBuilder.DropTable(
                name: "Estaciones");

            migrationBuilder.DropTable(
                name: "Sismografos");
        }
    }
}
