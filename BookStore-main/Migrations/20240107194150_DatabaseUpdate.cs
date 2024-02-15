using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    public partial class DatabaseUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingInfoId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfo_AspNetUsers_UserId",
                table: "ShippingInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfo_PaymentMethods_PaymentMethodId",
                table: "ShippingInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingInfo",
                table: "ShippingInfo");

            migrationBuilder.RenameTable(
                name: "ShippingInfo",
                newName: "ShippingInfos");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingInfo_UserId",
                table: "ShippingInfos",
                newName: "IX_ShippingInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingInfo_PaymentMethodId",
                table: "ShippingInfos",
                newName: "IX_ShippingInfos_PaymentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingInfos",
                table: "ShippingInfos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfos_ShippingInfoId",
                table: "Orders",
                column: "ShippingInfoId",
                principalTable: "ShippingInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfos_AspNetUsers_UserId",
                table: "ShippingInfos",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfos_PaymentMethods_PaymentMethodId",
                table: "ShippingInfos",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_ShippingInfos_ShippingInfoId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfos_AspNetUsers_UserId",
                table: "ShippingInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_ShippingInfos_PaymentMethods_PaymentMethodId",
                table: "ShippingInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingInfos",
                table: "ShippingInfos");

            migrationBuilder.RenameTable(
                name: "ShippingInfos",
                newName: "ShippingInfo");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingInfos_UserId",
                table: "ShippingInfo",
                newName: "IX_ShippingInfo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ShippingInfos_PaymentMethodId",
                table: "ShippingInfo",
                newName: "IX_ShippingInfo_PaymentMethodId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingInfo",
                table: "ShippingInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_ShippingInfo_ShippingInfoId",
                table: "Orders",
                column: "ShippingInfoId",
                principalTable: "ShippingInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfo_AspNetUsers_UserId",
                table: "ShippingInfo",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShippingInfo_PaymentMethods_PaymentMethodId",
                table: "ShippingInfo",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
