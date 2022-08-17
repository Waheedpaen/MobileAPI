using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EntitiesClasses.Migrations
{
    public partial class orderDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "Created_At",
                table: "orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderStatus",
                table: "orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ShippedDate",
                table: "orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Updated_At",
                table: "orders",
                type: "datetime2",
                nullable: true);

           
            migrationBuilder.CreateTable(
                name: "OrderDetail",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MobileId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDetail_Mobiles_MobileId",
                        column: x => x.MobileId,
                        principalTable: "Mobiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetail_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_MobileId",
                table: "OrderDetail",
                column: "MobileId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetail",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetail");

            migrationBuilder.DropColumn(
                name: "Created_At",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "OrderStatus",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "ShippedDate",
                table: "orders");

            migrationBuilder.DropColumn(
                name: "Updated_At",
                table: "orders");

           

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
