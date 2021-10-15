using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieTickets.Repository.Migrations
{
    public partial class amaann : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieInOrder_Order_MovieId",
                table: "MovieInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieInOrder_Movies_OrderId",
                table: "MovieInOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieInOrder",
                table: "MovieInOrder");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "MovieInOrder",
                newName: "MovieInOrders");

            migrationBuilder.RenameIndex(
                name: "IX_Order_UserId",
                table: "Orders",
                newName: "IX_Orders_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieInOrder_OrderId",
                table: "MovieInOrders",
                newName: "IX_MovieInOrders_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieInOrder_MovieId",
                table: "MovieInOrders",
                newName: "IX_MovieInOrders_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieInOrders",
                table: "MovieInOrders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieInOrders_Orders_MovieId",
                table: "MovieInOrders",
                column: "MovieId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieInOrders_Movies_OrderId",
                table: "MovieInOrders",
                column: "OrderId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieInOrders_Orders_MovieId",
                table: "MovieInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieInOrders_Movies_OrderId",
                table: "MovieInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_UserId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieInOrders",
                table: "MovieInOrders");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "MovieInOrders",
                newName: "MovieInOrder");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserId",
                table: "Order",
                newName: "IX_Order_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieInOrders_OrderId",
                table: "MovieInOrder",
                newName: "IX_MovieInOrder_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieInOrders_MovieId",
                table: "MovieInOrder",
                newName: "IX_MovieInOrder_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieInOrder",
                table: "MovieInOrder",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieInOrder_Order_MovieId",
                table: "MovieInOrder",
                column: "MovieId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieInOrder_Movies_OrderId",
                table: "MovieInOrder",
                column: "OrderId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
