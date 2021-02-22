using Microsoft.EntityFrameworkCore.Migrations;

namespace FasePractica.WebApp.Data.Migrations
{
    public partial class CreateTenantSchema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Institutos",
                columns: table => new
                {
                    InstitutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenantName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Institutos", x => x.InstitutoId);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombresApellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.UsuarioId);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPorInstitutos",
                columns: table => new
                {
                    UsuarioPorInstitutoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    InstitutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPorInstitutos", x => x.UsuarioPorInstitutoId);
                    table.ForeignKey(
                        name: "FK_UsuariosPorInstitutos_Institutos_InstitutoId",
                        column: x => x.InstitutoId,
                        principalTable: "Institutos",
                        principalColumn: "InstitutoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosPorInstitutos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPorInstitutos_InstitutoId",
                table: "UsuariosPorInstitutos",
                column: "InstitutoId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPorInstitutos_UsuarioId",
                table: "UsuariosPorInstitutos",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsuariosPorInstitutos");

            migrationBuilder.DropTable(
                name: "Institutos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
