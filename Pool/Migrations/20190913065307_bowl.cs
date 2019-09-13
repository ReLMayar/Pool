using Microsoft.EntityFrameworkCore.Migrations;

namespace Pool.Migrations
{
    public partial class bowl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "QuantityOfOccupation",
                table: "Subscriptions",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "Age",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.CreateTable(
                name: "Bowls",
                columns: table => new
                {
                    BowlId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    WorkLoad = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bowls", x => x.BowlId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bowls");

            migrationBuilder.AlterColumn<int>(
                name: "QuantityOfOccupation",
                table: "Subscriptions",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Clients",
                nullable: false,
                oldClrType: typeof(byte));
        }
    }
}
