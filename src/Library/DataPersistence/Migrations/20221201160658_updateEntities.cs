using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class updateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryName",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "RestaurantCategoryName",
                table: "Restaurants",
                type: "nvarchar(20)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryName",
                table: "Restaurants",
                column: "RestaurantCategoryName",
                principalTable: "RestaurantCategories",
                principalColumn: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryName",
                table: "Restaurants");

            migrationBuilder.AlterColumn<string>(
                name: "RestaurantCategoryName",
                table: "Restaurants",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Restaurants_RestaurantCategories_RestaurantCategoryName",
                table: "Restaurants",
                column: "RestaurantCategoryName",
                principalTable: "RestaurantCategories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
