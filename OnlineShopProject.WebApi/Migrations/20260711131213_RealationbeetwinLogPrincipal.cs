using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShopProject.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class RealationbeetwinLogPrincipal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreaterId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiederId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreaterId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiederId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreaterId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiederId",
                table: "OrderItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreaterId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "LastPermium",
                table: "AspNetUsers",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiederId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CreaterId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ModifiederId",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreaterId",
                table: "Products",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_DeleterId",
                table: "Products",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ModifiederId",
                table: "Products",
                column: "ModifiederId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CreaterId",
                table: "Orders",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeleterId",
                table: "Orders",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ModifiederId",
                table: "Orders",
                column: "ModifiederId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_CreaterId",
                table: "OrderItems",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_DeleterId",
                table: "OrderItems",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ModifiederId",
                table: "OrderItems",
                column: "ModifiederId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreaterId",
                table: "AspNetUsers",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DeleterId",
                table: "AspNetUsers",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ModifiederId",
                table: "AspNetUsers",
                column: "ModifiederId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_CreaterId",
                table: "AspNetRoles",
                column: "CreaterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_DeleterId",
                table: "AspNetRoles",
                column: "DeleterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ModifiederId",
                table: "AspNetRoles",
                column: "ModifiederId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_CreaterId",
                table: "AspNetRoles",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_DeleterId",
                table: "AspNetRoles",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_ModifiederId",
                table: "AspNetRoles",
                column: "ModifiederId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_CreaterId",
                table: "AspNetUsers",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DeleterId",
                table: "AspNetUsers",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ModifiederId",
                table: "AspNetUsers",
                column: "ModifiederId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AspNetUsers_CreaterId",
                table: "OrderItems",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AspNetUsers_DeleterId",
                table: "OrderItems",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_AspNetUsers_ModifiederId",
                table: "OrderItems",
                column: "ModifiederId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_CreaterId",
                table: "Orders",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_DeleterId",
                table: "Orders",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_AspNetUsers_ModifiederId",
                table: "Orders",
                column: "ModifiederId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_CreaterId",
                table: "Products",
                column: "CreaterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_DeleterId",
                table: "Products",
                column: "DeleterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_ModifiederId",
                table: "Products",
                column: "ModifiederId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_CreaterId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_DeleterId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_AspNetUsers_ModifiederId",
                table: "AspNetRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_CreaterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_DeleterId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ModifiederId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AspNetUsers_CreaterId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AspNetUsers_DeleterId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_AspNetUsers_ModifiederId",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_CreaterId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_DeleterId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_AspNetUsers_ModifiederId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_CreaterId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_DeleterId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_ModifiederId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_CreaterId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_DeleterId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ModifiederId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CreaterId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeleterId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ModifiederId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_CreaterId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_DeleterId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_OrderItems_ModifiederId",
                table: "OrderItems");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CreaterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DeleterId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ModifiederId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_CreaterId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_DeleterId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ModifiederId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModifiederId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ModifiederId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ModifiederId",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastPermium",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ModifiederId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CreaterId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ModifiederId",
                table: "AspNetRoles");
        }
    }
}
