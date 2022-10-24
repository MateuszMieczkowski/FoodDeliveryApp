using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class changedKeyToNameOnRestaurantCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryId",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantCategoryId",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantCategories",
                table: "RestaurantCategories");

            migrationBuilder.DropColumn(
                name: "RestaurantCategoryId",
                table: "Restaurants");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RestaurantCategories");

            migrationBuilder.AddColumn<string>(
                name: "RestaurantCategoryName",
                table: "Restaurants",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantCategories",
                table: "RestaurantCategories",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantCategoryName",
                table: "Restaurants",
                column: "RestaurantCategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryName",
                table: "Restaurants",
                column: "RestaurantCategoryName",
                principalTable: "RestaurantCategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryName",
                table: "Restaurants");

            migrationBuilder.DropIndex(
                name: "IX_Restaurants_RestaurantCategoryName",
                table: "Restaurants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantCategories",
                table: "RestaurantCategories");

            migrationBuilder.DropColumn(
                name: "RestaurantCategoryName",
                table: "Restaurants");

            migrationBuilder.AddColumn<int>(
                name: "RestaurantCategoryId",
                table: "Restaurants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RestaurantCategories",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantCategories",
                table: "RestaurantCategories",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_RestaurantCategoryId",
                table: "Restaurants",
                column: "RestaurantCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryId",
                table: "Restaurants",
                column: "RestaurantCategoryId",
                principalTable: "RestaurantCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
