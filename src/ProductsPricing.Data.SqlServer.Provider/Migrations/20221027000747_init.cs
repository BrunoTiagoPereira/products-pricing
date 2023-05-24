using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsPricing.Data.SqlServer.Provider.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ncms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code_Value = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ncms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email_Value = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Password_Hash = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Roles = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Imports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FinishedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status_Value = table.Column<int>(type: "int", nullable: false),
                    Status_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Logs = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Imports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Imports_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    Value = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    AdditionalValue = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false, defaultValue: 0m),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImpactedProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RootProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TargetProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status_Value = table.Column<int>(type: "int", nullable: false),
                    Status_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TargetProductNewValue = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImpactedProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImpactedProducts_Imports_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Imports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImpactedProducts_Products_RootProductId",
                        column: x => x.RootProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImpactedProducts_Products_TargetProductId",
                        column: x => x.TargetProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ImportItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DecodedImportItem_NewValue = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: true),
                    UnitOfMeasure_Value = table.Column<int>(type: "int", nullable: true),
                    UnitOfMeasure_Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FileLineReference = table.Column<int>(type: "int", nullable: true),
                    SelectedProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status_Value = table.Column<int>(type: "int", nullable: true),
                    Status_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ProcessedImportItem_ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    NewValue = table.Column<decimal>(type: "decimal(9,2)", precision: 9, scale: 2, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportItems_Imports_ImportId",
                        column: x => x.ImportId,
                        principalTable: "Imports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImportItems_Products_ProcessedImportItem_ProductId",
                        column: x => x.ProcessedImportItem_ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ImportItems_Products_SelectedProductId",
                        column: x => x.SelectedProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RootProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DependencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ingredients_Products_DependencyId",
                        column: x => x.DependencyId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ingredients_Products_RootProductId",
                        column: x => x.RootProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NcmProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NcmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NcmProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NcmProducts_Ncms_NcmId",
                        column: x => x.NcmId,
                        principalTable: "Ncms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NcmProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnitOfMeasureConversions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitOfMeasure_Value = table.Column<int>(type: "int", nullable: false),
                    UnitOfMeasure_Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProductsCount = table.Column<int>(type: "int", nullable: false),
                    GramsByUnit = table.Column<decimal>(type: "decimal(9,4)", precision: 9, scale: 4, nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasureConversions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasureConversions_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PendingProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PendingImportItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PendingProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PendingProducts_ImportItems_PendingImportItemId",
                        column: x => x.PendingImportItemId,
                        principalTable: "ImportItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PendingProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImpactedProducts_ImportId",
                table: "ImpactedProducts",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImpactedProducts_RootProductId",
                table: "ImpactedProducts",
                column: "RootProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImpactedProducts_TargetProductId",
                table: "ImpactedProducts",
                column: "TargetProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItems_ImportId",
                table: "ImportItems",
                column: "ImportId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItems_ProcessedImportItem_ProductId",
                table: "ImportItems",
                column: "ProcessedImportItem_ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItems_ProductId",
                table: "ImportItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItems_SelectedProductId",
                table: "ImportItems",
                column: "SelectedProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Imports_UserId",
                table: "Imports",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_DependencyId",
                table: "Ingredients",
                column: "DependencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_RootProductId",
                table: "Ingredients",
                column: "RootProductId");

            migrationBuilder.CreateIndex(
                name: "IX_NcmProducts_NcmId",
                table: "NcmProducts",
                column: "NcmId");

            migrationBuilder.CreateIndex(
                name: "IX_NcmProducts_ProductId",
                table: "NcmProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingProducts_PendingImportItemId",
                table: "PendingProducts",
                column: "PendingImportItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PendingProducts_ProductId",
                table: "PendingProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasureConversions_ProductId",
                table: "UnitOfMeasureConversions",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImpactedProducts");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "NcmProducts");

            migrationBuilder.DropTable(
                name: "PendingProducts");

            migrationBuilder.DropTable(
                name: "UnitOfMeasureConversions");

            migrationBuilder.DropTable(
                name: "Ncms");

            migrationBuilder.DropTable(
                name: "ImportItems");

            migrationBuilder.DropTable(
                name: "Imports");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
