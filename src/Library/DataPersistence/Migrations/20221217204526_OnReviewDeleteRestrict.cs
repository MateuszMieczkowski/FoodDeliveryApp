using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Migrations
{
    public partial class OnReviewDeleteRestrict : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_RestaurantReviews_Restaurants_RestaurantId",
                table: "RestaurantReviews",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
