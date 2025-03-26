using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _22806012222_PhamNgocHuy_S3_B3.Migrations
{
    /// <inheritdoc />
    public partial class init3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_UserId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_UserId",
                table: "OrderDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_AspNetUsers_UserId",
                table: "OrderDetails");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "OrderDetails",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_AspNetUsers_UserId",
                table: "OrderDetails",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
