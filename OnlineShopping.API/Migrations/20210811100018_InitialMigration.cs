using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShopping.API.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BPTypes",
                columns: table => new
                {
                    TypeCode = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BPTypes", x => x.TypeCode);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemCode);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BusinessPartners",
                columns: table => new
                {
                    BPCode = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    BPName = table.Column<string>(type: "nvarchar(254)", maxLength: 254, nullable: false),
                    BPTypeId = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessPartners", x => x.BPCode);
                    table.ForeignKey(
                        name: "FK_BusinessPartners_BPTypes_BPTypeId",
                        column: x => x.BPTypeId,
                        principalTable: "BPTypes",
                        principalColumn: "TypeCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessPartnerId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_BusinessPartners_BusinessPartnerId",
                        column: x => x.BusinessPartnerId,
                        principalTable: "BusinessPartners",
                        principalColumn: "BPCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_LastUpdatedByID",
                        column: x => x.LastUpdatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusinessPartnerId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleOrders_BusinessPartners_BusinessPartnerId",
                        column: x => x.BusinessPartnerId,
                        principalTable: "BusinessPartners",
                        principalColumn: "BPCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrders_Users_LastUpdatedByID",
                        column: x => x.LastUpdatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrdersLines",
                columns: table => new
                {
                    LineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseOrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrdersLines", x => x.LineID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrdersLines_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrdersLines_PurchaseOrders_PurchaseOrderId",
                        column: x => x.PurchaseOrderId,
                        principalTable: "PurchaseOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PurchaseOrdersLines_Users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrdersLines_Users_LastUpdatedByID",
                        column: x => x.LastUpdatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrdersLines",
                columns: table => new
                {
                    LineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaleOrderId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(38,18)", precision: 38, scale: 18, nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedByID = table.Column<int>(type: "int", nullable: true),
                    LastUpdatedByID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrdersLines", x => x.LineID);
                    table.ForeignKey(
                        name: "FK_SaleOrdersLines_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrdersLines_SaleOrders_SaleOrderId",
                        column: x => x.SaleOrderId,
                        principalTable: "SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleOrdersLines_Users_CreatedByID",
                        column: x => x.CreatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrdersLines_Users_LastUpdatedByID",
                        column: x => x.LastUpdatedByID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrdersLinesComments",
                columns: table => new
                {
                    CommentLineID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocIDID = table.Column<int>(type: "int", nullable: true),
                    SaleOrdersLineId = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrdersLinesComments", x => x.CommentLineID);
                    table.ForeignKey(
                        name: "FK_SaleOrdersLinesComments_SaleOrders_DocIDID",
                        column: x => x.DocIDID,
                        principalTable: "SaleOrders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleOrdersLinesComments_SaleOrdersLines_SaleOrdersLineId",
                        column: x => x.SaleOrdersLineId,
                        principalTable: "SaleOrdersLines",
                        principalColumn: "LineID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "BPTypes",
                columns: new[] { "TypeCode", "TypeName" },
                values: new object[,]
                {
                    { "C", "Customer" },
                    { "V", "Vendor" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemCode", "Active", "ItemName" },
                values: new object[,]
                {
                    { "Itm1", true, "Item 1" },
                    { "Itm2", true, "Item 2" },
                    { "Itm3", false, "Item 3" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Active", "FullName", "Password", "UserName" },
                values: new object[,]
                {
                    { 1, true, "U1", "P1", "U1" },
                    { 2, false, "U2", "P2", "U2" }
                });

            migrationBuilder.InsertData(
                table: "BusinessPartners",
                columns: new[] { "BPCode", "Active", "BPName", "BPTypeId" },
                values: new object[,]
                {
                    { "C0001", true, "Customer 1", "C" },
                    { "C0002", false, "Customer 2", "C" },
                    { "V0001", true, "Vendor 1", "V" },
                    { "V0002", false, "Vendor 2", "V" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessPartners_BPTypeId",
                table: "BusinessPartners",
                column: "BPTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_BusinessPartnerId",
                table: "PurchaseOrders",
                column: "BusinessPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_CreatedByID",
                table: "PurchaseOrders",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_LastUpdatedByID",
                table: "PurchaseOrders",
                column: "LastUpdatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrdersLines_CreatedByID",
                table: "PurchaseOrdersLines",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrdersLines_ItemId",
                table: "PurchaseOrdersLines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrdersLines_LastUpdatedByID",
                table: "PurchaseOrdersLines",
                column: "LastUpdatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrdersLines_PurchaseOrderId",
                table: "PurchaseOrdersLines",
                column: "PurchaseOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_BusinessPartnerId",
                table: "SaleOrders",
                column: "BusinessPartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_CreatedByID",
                table: "SaleOrders",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_LastUpdatedByID",
                table: "SaleOrders",
                column: "LastUpdatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrdersLines_CreatedByID",
                table: "SaleOrdersLines",
                column: "CreatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrdersLines_ItemId",
                table: "SaleOrdersLines",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrdersLines_LastUpdatedByID",
                table: "SaleOrdersLines",
                column: "LastUpdatedByID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrdersLines_SaleOrderId",
                table: "SaleOrdersLines",
                column: "SaleOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrdersLinesComments_DocIDID",
                table: "SaleOrdersLinesComments",
                column: "DocIDID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrdersLinesComments_SaleOrdersLineId",
                table: "SaleOrdersLinesComments",
                column: "SaleOrdersLineId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PurchaseOrdersLines");

            migrationBuilder.DropTable(
                name: "SaleOrdersLinesComments");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "SaleOrdersLines");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "SaleOrders");

            migrationBuilder.DropTable(
                name: "BusinessPartners");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "BPTypes");
        }
    }
}
