using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LendThingsAPI.Migrations
{
    public partial class FixModelsRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_People_PersonId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Things_ThingId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Things_Categories_CategoryId",
                table: "Things");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Things",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThingId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Loans",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_People_PersonId",
                table: "Loans",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Things_ThingId",
                table: "Loans",
                column: "ThingId",
                principalTable: "Things",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Things_Categories_CategoryId",
                table: "Things",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_People_PersonId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Things_ThingId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Things_Categories_CategoryId",
                table: "Things");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Things",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ThingId",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "Loans",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_People_PersonId",
                table: "Loans",
                column: "PersonId",
                principalTable: "People",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Things_ThingId",
                table: "Loans",
                column: "ThingId",
                principalTable: "Things",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Things_Categories_CategoryId",
                table: "Things",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
