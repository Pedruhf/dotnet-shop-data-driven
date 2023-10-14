using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "[categoria]",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[categoria]", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "[usuario]",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    login = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    senha = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    permissao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[usuario]", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "[produto]",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    id_categoria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_[produto]", x => x.id);
                    table.ForeignKey(
                        name: "FK_[produto]_[categoria]_id_categoria",
                        column: x => x.id_categoria,
                        principalTable: "[categoria]",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_[produto]_id_categoria",
                table: "[produto]",
                column: "id_categoria");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "[produto]");

            migrationBuilder.DropTable(
                name: "[usuario]");

            migrationBuilder.DropTable(
                name: "[categoria]");
        }
    }
}
