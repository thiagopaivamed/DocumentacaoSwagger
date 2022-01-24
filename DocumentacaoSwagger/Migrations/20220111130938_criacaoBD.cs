using Microsoft.EntityFrameworkCore.Migrations;

namespace DocumentacaoSwagger.Migrations
{
    public partial class criacaoBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Avioes",
                columns: table => new
                {
                    AviaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeProdutor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeAviao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtdPassageiros = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Avioes", x => x.AviaoId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Avioes");
        }
    }
}
