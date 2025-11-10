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
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdAddressDelivery = table.Column<int>(type: "INTEGER", nullable: false),
                    IdAddressInvoice = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCart = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCurrency = table.Column<int>(type: "INTEGER", nullable: false),
                    IdLang = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCustomer = table.Column<int>(type: "INTEGER", nullable: false),
                    IdCarrier = table.Column<int>(type: "INTEGER", nullable: false),
                    CurrentState = table.Column<int>(type: "INTEGER", nullable: false),
                    Module = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "TEXT", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DeliveryNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    DeliveryDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valid = table.Column<int>(type: "INTEGER", nullable: false),
                    DateAdd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateUpd = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ShippingNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    IdShopGroup = table.Column<int>(type: "INTEGER", nullable: false),
                    IdShop = table.Column<int>(type: "INTEGER", nullable: false),
                    SecureKey = table.Column<string>(type: "TEXT", nullable: false),
                    Payment = table.Column<string>(type: "TEXT", nullable: false),
                    Recyclable = table.Column<int>(type: "INTEGER", nullable: false),
                    Gift = table.Column<int>(type: "INTEGER", nullable: false),
                    GiftMessage = table.Column<string>(type: "TEXT", nullable: false),
                    MobileTheme = table.Column<string>(type: "TEXT", nullable: false),
                    TotalDiscounts = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalDiscountsTaxIncl = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalDiscountsTaxExcl = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPaid = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPaidTaxIncl = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPaidTaxExcl = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalPaidReal = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProducts = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalProductsWt = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalShipping = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalShippingTaxIncl = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalShippingTaxExcl = table.Column<decimal>(type: "TEXT", nullable: false),
                    CarrierTaxRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalWrapping = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalWrappingTaxIncl = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalWrappingTaxExcl = table.Column<decimal>(type: "TEXT", nullable: false),
                    RoundMode = table.Column<int>(type: "INTEGER", nullable: false),
                    RoundType = table.Column<int>(type: "INTEGER", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "TEXT", nullable: false),
                    Reference = table.Column<int>(type: "INTEGER", nullable: false),
                    CRNInvoiceNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CRNDeliveryNumber = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderRows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    OrderId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductAttributeId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductQuantity = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductName = table.Column<string>(type: "TEXT", nullable: false),
                    ProductReference = table.Column<string>(type: "TEXT", nullable: false),
                    ProductEan13 = table.Column<string>(type: "TEXT", nullable: false),
                    ProductIsbn = table.Column<string>(type: "TEXT", nullable: false),
                    ProductUpc = table.Column<string>(type: "TEXT", nullable: false),
                    ProductPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    IdCustomization = table.Column<int>(type: "INTEGER", nullable: false),
                    UnitPriceTaxIncl = table.Column<decimal>(type: "TEXT", nullable: false),
                    UnitPriceTaxExcl = table.Column<decimal>(type: "TEXT", nullable: false)
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
