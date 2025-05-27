using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RedSismica.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CambiosEstado_Empleados_ResponsableId",
                table: "CambiosEstado");

            migrationBuilder.DropForeignKey(
                name: "FK_CambiosEstado_Sismografos_SismografoId",
                table: "CambiosEstado");

            migrationBuilder.DropForeignKey(
                name: "FK_Estaciones_Sismografos_SismografoId",
                table: "Estaciones");

            migrationBuilder.DropForeignKey(
                name: "FK_MotivosBaja_OrdenesInspeccion_OrdenInspeccionId",
                table: "MotivosBaja");

            migrationBuilder.DropForeignKey(
                name: "FK_MotivosBaja_TiposMotivoBaja_TipoMotivoBajaId",
                table: "MotivosBaja");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesInspeccion_Empleados_ResponsableId",
                table: "OrdenesInspeccion");

            migrationBuilder.DropIndex(
                name: "IX_Estaciones_SismografoId",
                table: "Estaciones");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotivosBaja",
                table: "MotivosBaja");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CambiosEstado",
                table: "CambiosEstado");

            migrationBuilder.DropColumn(
                name: "SismografoId",
                table: "Estaciones");

            migrationBuilder.RenameTable(
                name: "MotivosBaja",
                newName: "MotivosBajaSismografo");

            migrationBuilder.RenameTable(
                name: "CambiosEstado",
                newName: "CambiosEstadoSismografo");

            migrationBuilder.RenameColumn(
                name: "ResponsableId",
                table: "OrdenesInspeccion",
                newName: "EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenesInspeccion_ResponsableId",
                table: "OrdenesInspeccion",
                newName: "IX_OrdenesInspeccion_EmpleadoId");

            migrationBuilder.RenameIndex(
                name: "IX_MotivosBaja_TipoMotivoBajaId",
                table: "MotivosBajaSismografo",
                newName: "IX_MotivosBajaSismografo_TipoMotivoBajaId");

            migrationBuilder.RenameIndex(
                name: "IX_MotivosBaja_OrdenInspeccionId",
                table: "MotivosBajaSismografo",
                newName: "IX_MotivosBajaSismografo_OrdenInspeccionId");

            migrationBuilder.RenameColumn(
                name: "ResponsableId",
                table: "CambiosEstadoSismografo",
                newName: "EmpleadoId");

            migrationBuilder.RenameColumn(
                name: "FechaHora",
                table: "CambiosEstadoSismografo",
                newName: "FechaHoraCambio");

            migrationBuilder.RenameIndex(
                name: "IX_CambiosEstado_SismografoId",
                table: "CambiosEstadoSismografo",
                newName: "IX_CambiosEstadoSismografo_SismografoId");

            migrationBuilder.RenameIndex(
                name: "IX_CambiosEstado_ResponsableId",
                table: "CambiosEstadoSismografo",
                newName: "IX_CambiosEstadoSismografo_EmpleadoId");

            migrationBuilder.AddColumn<string>(
                name: "Descripcion",
                table: "TiposMotivoBaja",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EstacionSismologicaId",
                table: "Sismografos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Ubicacion",
                table: "Estaciones",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotivosBajaSismografo",
                table: "MotivosBajaSismografo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CambiosEstadoSismografo",
                table: "CambiosEstadoSismografo",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Sismografos_EstacionSismologicaId",
                table: "Sismografos",
                column: "EstacionSismologicaId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CambiosEstadoSismografo_Empleados_EmpleadoId",
                table: "CambiosEstadoSismografo",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CambiosEstadoSismografo_Sismografos_SismografoId",
                table: "CambiosEstadoSismografo",
                column: "SismografoId",
                principalTable: "Sismografos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotivosBajaSismografo_OrdenesInspeccion_OrdenInspeccionId",
                table: "MotivosBajaSismografo",
                column: "OrdenInspeccionId",
                principalTable: "OrdenesInspeccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotivosBajaSismografo_TiposMotivoBaja_TipoMotivoBajaId",
                table: "MotivosBajaSismografo",
                column: "TipoMotivoBajaId",
                principalTable: "TiposMotivoBaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesInspeccion_Empleados_EmpleadoId",
                table: "OrdenesInspeccion",
                column: "EmpleadoId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sismografos_Estaciones_EstacionSismologicaId",
                table: "Sismografos",
                column: "EstacionSismologicaId",
                principalTable: "Estaciones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CambiosEstadoSismografo_Empleados_EmpleadoId",
                table: "CambiosEstadoSismografo");

            migrationBuilder.DropForeignKey(
                name: "FK_CambiosEstadoSismografo_Sismografos_SismografoId",
                table: "CambiosEstadoSismografo");

            migrationBuilder.DropForeignKey(
                name: "FK_MotivosBajaSismografo_OrdenesInspeccion_OrdenInspeccionId",
                table: "MotivosBajaSismografo");

            migrationBuilder.DropForeignKey(
                name: "FK_MotivosBajaSismografo_TiposMotivoBaja_TipoMotivoBajaId",
                table: "MotivosBajaSismografo");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdenesInspeccion_Empleados_EmpleadoId",
                table: "OrdenesInspeccion");

            migrationBuilder.DropForeignKey(
                name: "FK_Sismografos_Estaciones_EstacionSismologicaId",
                table: "Sismografos");

            migrationBuilder.DropIndex(
                name: "IX_Sismografos_EstacionSismologicaId",
                table: "Sismografos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MotivosBajaSismografo",
                table: "MotivosBajaSismografo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CambiosEstadoSismografo",
                table: "CambiosEstadoSismografo");

            migrationBuilder.DropColumn(
                name: "Descripcion",
                table: "TiposMotivoBaja");

            migrationBuilder.DropColumn(
                name: "EstacionSismologicaId",
                table: "Sismografos");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "Estaciones");

            migrationBuilder.RenameTable(
                name: "MotivosBajaSismografo",
                newName: "MotivosBaja");

            migrationBuilder.RenameTable(
                name: "CambiosEstadoSismografo",
                newName: "CambiosEstado");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "OrdenesInspeccion",
                newName: "ResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdenesInspeccion_EmpleadoId",
                table: "OrdenesInspeccion",
                newName: "IX_OrdenesInspeccion_ResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_MotivosBajaSismografo_TipoMotivoBajaId",
                table: "MotivosBaja",
                newName: "IX_MotivosBaja_TipoMotivoBajaId");

            migrationBuilder.RenameIndex(
                name: "IX_MotivosBajaSismografo_OrdenInspeccionId",
                table: "MotivosBaja",
                newName: "IX_MotivosBaja_OrdenInspeccionId");

            migrationBuilder.RenameColumn(
                name: "FechaHoraCambio",
                table: "CambiosEstado",
                newName: "FechaHora");

            migrationBuilder.RenameColumn(
                name: "EmpleadoId",
                table: "CambiosEstado",
                newName: "ResponsableId");

            migrationBuilder.RenameIndex(
                name: "IX_CambiosEstadoSismografo_SismografoId",
                table: "CambiosEstado",
                newName: "IX_CambiosEstado_SismografoId");

            migrationBuilder.RenameIndex(
                name: "IX_CambiosEstadoSismografo_EmpleadoId",
                table: "CambiosEstado",
                newName: "IX_CambiosEstado_ResponsableId");

            migrationBuilder.AddColumn<int>(
                name: "SismografoId",
                table: "Estaciones",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MotivosBaja",
                table: "MotivosBaja",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CambiosEstado",
                table: "CambiosEstado",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Estaciones_SismografoId",
                table: "Estaciones",
                column: "SismografoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CambiosEstado_Empleados_ResponsableId",
                table: "CambiosEstado",
                column: "ResponsableId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CambiosEstado_Sismografos_SismografoId",
                table: "CambiosEstado",
                column: "SismografoId",
                principalTable: "Sismografos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estaciones_Sismografos_SismografoId",
                table: "Estaciones",
                column: "SismografoId",
                principalTable: "Sismografos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotivosBaja_OrdenesInspeccion_OrdenInspeccionId",
                table: "MotivosBaja",
                column: "OrdenInspeccionId",
                principalTable: "OrdenesInspeccion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MotivosBaja_TiposMotivoBaja_TipoMotivoBajaId",
                table: "MotivosBaja",
                column: "TipoMotivoBajaId",
                principalTable: "TiposMotivoBaja",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdenesInspeccion_Empleados_ResponsableId",
                table: "OrdenesInspeccion",
                column: "ResponsableId",
                principalTable: "Empleados",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
