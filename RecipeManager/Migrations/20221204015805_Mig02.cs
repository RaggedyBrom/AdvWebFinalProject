using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RecipeManager.Migrations
{
    public partial class Mig02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "RecipeIngredients");

            migrationBuilder.RenameColumn(
                name: "QuantityUnit",
                table: "RecipeIngredients",
                newName: "Amount");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "RecipeIngredients",
                newName: "QuantityUnit");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "RecipeIngredients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
