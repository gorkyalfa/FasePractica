using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace FasePractica.WebApp.Migrations
{
    public partial class CrearSchemaT1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "t1");

            migrationBuilder.CreateTable(
                name: "Semestres",
                schema: "t1",
                columns: table => new
                {
                    SemestreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Semestres", x => x.SemestreId);
                });

            migrationBuilder.CreateTable(
                name: "Conversaciones",
                schema: "t1",
                columns: table => new
                {
                    ConversacionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealizadoEl = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Conversaciones", x => x.ConversacionId);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                schema: "t1", 
                columns: table => new
                {
                    DocumentoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Codigo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirmadoEl = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFinal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AlmacenadoEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Documentos", x => x.DocumentoId);
                });

            migrationBuilder.CreateTable(
                name: "Personas",
                schema: "t1", 
                columns: table => new
                {
                    PersonaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombres = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    CorreoInstitucional = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CorreoPersonal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    TipoPersona = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TituloProfesional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CargoEmpresa = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmpresaId = table.Column<int>(type: "int", nullable: true),
                    EmpresaId1 = table.Column<int>(type: "int", nullable: true),
                    CodigoIgnug = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Tutor_TituloProfesional = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Tutor_CodigoIgnug = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Personas", x => x.PersonaId);
                });

            migrationBuilder.CreateTable(
                name: "Empresas",
                schema: "t1", 
                columns: table => new
                {
                    EmpresaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Alias = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TipoEmpresa = table.Column<int>(type: "int", nullable: false),
                    TipoPersona = table.Column<int>(type: "int", nullable: false),
                    Ruc = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SectorProductivo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Latitud = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Longitud = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    EstudiantesConvenio = table.Column<int>(type: "int", nullable: false),
                    EstudiantesActuales = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Empresas", x => x.EmpresaId);
                    table.ForeignKey(
                        name: "FK_t1_Empresas_Personas_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Proyectos",
                schema: "t1",
                columns: table => new
                {
                    ProyectoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SemestreId = table.Column<int>(type: "int", nullable: false),
                    EmpresaId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tecnologia = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    TutorId = table.Column<int>(type: "int", nullable: false),
                    ContactoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Proyectos", x => x.ProyectoId);
                    table.ForeignKey(
                        name: "FK_t1_Proyectos_Empresas_EmpresaId",
                        column: x => x.EmpresaId,
                        principalTable: "Empresas",
                        principalColumn: "EmpresaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t1_Proyectos_Personas_ContactoId",
                        column: x => x.ContactoId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId");
                    table.ForeignKey(
                        name: "FK_t1_Proyectos_Personas_TutorId",
                        column: x => x.TutorId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId");
                    table.ForeignKey(
                        name: "FK_t1_Proyectos_Semestres_SemestreId",
                        column: x => x.SemestreId,
                        principalTable: "Semestres",
                        principalColumn: "SemestreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                schema: "t1",
                columns: table => new
                {
                    NotaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EstudianteId = table.Column<int>(type: "int", nullable: false),
                    ProyectoId = table.Column<int>(type: "int", nullable: false),
                    Calificacion = table.Column<float>(type: "real", nullable: false),
                    Aprueba = table.Column<bool>(type: "bit", nullable: false),
                    ProyectoId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t1_Notas", x => x.NotaId);
                    table.ForeignKey(
                        name: "FK_t1_Notas_Personas_EstudianteId",
                        column: x => x.EstudianteId,
                        principalTable: "Personas",
                        principalColumn: "PersonaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t1_Notas_Proyectos_ProyectoId",
                        column: x => x.ProyectoId,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoId");
                    table.ForeignKey(
                        name: "FK_t1_Notas_Proyectos_ProyectoId1",
                        column: x => x.ProyectoId1,
                        principalTable: "Proyectos",
                        principalColumn: "ProyectoId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversaciones_EmpresaId",
                table: "Conversaciones",
                schema: "t1",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_EmpresaId",
                table: "Documentos",
                schema: "t1",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Empresas_TutorId",
                table: "Empresas",
                schema: "t1",
                column: "TutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_EstudianteId",
                table: "Notas",
                schema: "t1",
                column: "EstudianteId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_ProyectoId",
                table: "Notas",
                schema: "t1",
                column: "ProyectoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_ProyectoId1",
                table: "Notas",
                schema: "t1",
                column: "ProyectoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EmpresaId",
                table: "Personas",
                schema: "t1",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Personas_EmpresaId1",
                table: "Personas",
                schema: "t1",
                column: "EmpresaId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_ContactoId",
                table: "Proyectos",
                schema: "t1",
                column: "ContactoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_EmpresaId",
                table: "Proyectos",
                schema: "t1",
                column: "EmpresaId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_SemestreId",
                table: "Proyectos",
                schema: "t1",
                column: "SemestreId");

            migrationBuilder.CreateIndex(
                name: "IX_Proyectos_TutorId",
                table: "Proyectos",
                schema: "t1",
                column: "TutorId");

            migrationBuilder.AddForeignKey(
                name: "FK_t1_Conversaciones_Empresas_EmpresaId",
                table: "Conversaciones",
                schema: "t1",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t1_Documentos_Empresas_EmpresaId",
                table: "Documentos",
                schema: "t1",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_t1_Personas_Empresas_EmpresaId",
                table: "Personas",
                schema: "t1",
                column: "EmpresaId",
                principalTable: "Empresas",
                principalColumn: "EmpresaId");

            migrationBuilder.AddForeignKey(
                name: "FK_t1_Personas_Empresas_EmpresaId1",
                table: "Personas",
                schema: "t1",
                column: "EmpresaId1",
                principalTable: "Empresas",
                principalColumn: "EmpresaId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_t1_Personas_Empresas_EmpresaId",
                schema: "t1",
                table: "Personas");

            migrationBuilder.DropForeignKey(
                name: "FK_t1_Personas_Empresas_EmpresaId1",
                schema: "t1",
                table: "Personas");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Conversaciones");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Documentos");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Notas");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Proyectos");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Semestres");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Empresas");

            migrationBuilder.DropTable(
                schema: "t1",
                name: "Personas");
        }
    }
}
