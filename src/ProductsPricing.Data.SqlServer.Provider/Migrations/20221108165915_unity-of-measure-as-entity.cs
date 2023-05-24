using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsPricing.Data.SqlServer.Provider.Migrations
{
    public partial class unityofmeasureasentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitOfMeasure_Name",
                table: "UnitOfMeasureConversions");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure_Value",
                table: "UnitOfMeasureConversions");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure_Name",
                table: "ImportItems");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasure_Value",
                table: "ImportItems");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasureId",
                table: "UnitOfMeasureConversions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UnitOfMeasureId",
                table: "ImportItems",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UnitOfMeasures",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitOfMeasures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnitOfMeasures_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasureConversions_UnitOfMeasureId",
                table: "UnitOfMeasureConversions",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportItems_UnitOfMeasureId",
                table: "ImportItems",
                column: "UnitOfMeasureId");

            migrationBuilder.CreateIndex(
                name: "IX_UnitOfMeasures_UserId",
                table: "UnitOfMeasures",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ImportItems_UnitOfMeasures_UnitOfMeasureId",
                table: "ImportItems",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UnitOfMeasureConversions_UnitOfMeasures_UnitOfMeasureId",
                table: "UnitOfMeasureConversions",
                column: "UnitOfMeasureId",
                principalTable: "UnitOfMeasures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImportItems_UnitOfMeasures_UnitOfMeasureId",
                table: "ImportItems");

            migrationBuilder.DropForeignKey(
                name: "FK_UnitOfMeasureConversions_UnitOfMeasures_UnitOfMeasureId",
                table: "UnitOfMeasureConversions");

            migrationBuilder.DropTable(
                name: "UnitOfMeasures");

            migrationBuilder.DropIndex(
                name: "IX_UnitOfMeasureConversions_UnitOfMeasureId",
                table: "UnitOfMeasureConversions");

            migrationBuilder.DropIndex(
                name: "IX_ImportItems_UnitOfMeasureId",
                table: "ImportItems");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "UnitOfMeasureConversions");

            migrationBuilder.DropColumn(
                name: "UnitOfMeasureId",
                table: "ImportItems");

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasure_Name",
                table: "UnitOfMeasureConversions",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasure_Value",
                table: "UnitOfMeasureConversions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UnitOfMeasure_Name",
                table: "ImportItems",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitOfMeasure_Value",
                table: "ImportItems",
                type: "int",
                nullable: true);
        }
    }
}
