using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductsPricing.Data.SqlServer.Provider.Migrations
{
    public partial class impactedbyproduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TargetProductNewValue",
                table: "ImpactedProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetProductId",
                table: "ImpactedProducts",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "ImpactedProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "ImpactedProducts");

            migrationBuilder.AlterColumn<Guid>(
                name: "TargetProductId",
                table: "ImpactedProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TargetProductNewValue",
                table: "ImpactedProducts",
                type: "decimal(9,2)",
                precision: 9,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
