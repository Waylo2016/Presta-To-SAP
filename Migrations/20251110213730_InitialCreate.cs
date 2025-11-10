using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrestaToSap.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAddressDelivery = table.Column<int>(type: "int", nullable: false),
                    IdAddressInvoice = table.Column<int>(type: "int", nullable: false),
                    IdCart = table.Column<int>(type: "int", nullable: false),
                    IdCurrency = table.Column<int>(type: "int", nullable: false),
                    IdLang = table.Column<int>(type: "int", nullable: false),
                    IdCustomer = table.Column<int>(type: "int", nullable: false),
                    IdCarrier = table.Column<int>(type: "int", nullable: false),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    Module = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeliveryNumber = table.Column<int>(type: "int", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valid = table.Column<int>(type: "int", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdShopGroup = table.Column<int>(type: "int", nullable: false),
                    IdShop = table.Column<int>(type: "int", nullable: false),
                    SecureKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Payment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Recyclable = table.Column<int>(type: "int", nullable: false),
                    Gift = table.Column<int>(type: "int", nullable: false),
                    GiftMessage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileTheme = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscountsTaxIncl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalDiscountsTaxExcl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaidTaxIncl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaidTaxExcl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPaidReal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalProducts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalProductsWt = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalShipping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalShippingTaxIncl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalShippingTaxExcl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CarrierTaxRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWrapping = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWrappingTaxIncl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalWrappingTaxExcl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RoundMode = table.Column<int>(type: "int", nullable: false),
                    RoundType = table.Column<int>(type: "int", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<int>(type: "int", nullable: false),
                    CRNInvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CRNDeliveryNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "int", nullable: false),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductEan13 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductIsbn = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductUpc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IdCustomization = table.Column<int>(type: "int", nullable: false),
                    UnitPriceTaxIncl = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPriceTaxExcl = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderRows_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRows_OrderId",
                table: "OrderRows",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRows");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
