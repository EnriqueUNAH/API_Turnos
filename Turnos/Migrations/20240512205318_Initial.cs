using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Turnos.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    ID_USUARIO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USUARIO = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ID_ROL = table.Column<int>(type: "int", nullable: false),
                    ID_AREA = table.Column<int>(type: "int", nullable: false),
                    NUMERO = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EXTENCION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdZona = table.Column<int>(type: "int", nullable: false),
                    CELULAR = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.ID_USUARIO);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
