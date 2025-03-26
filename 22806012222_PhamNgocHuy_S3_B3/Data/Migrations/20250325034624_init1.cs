using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _22806012222_PhamNgocHuy_S3_B3.Migrations
{
    /// <inheritdoc />
    public partial class init1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_UserId",
                table: "OrderDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_UserId",
                table: "OrderDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_UserId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_UserId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "OrderDetails");
        }
    }
}
