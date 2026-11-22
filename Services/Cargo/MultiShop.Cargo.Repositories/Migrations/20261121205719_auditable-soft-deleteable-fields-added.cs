using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Cargo.Repositories.Migrations;

/// <inheritdoc />
public partial class _20261121205719_auditablesoftdeleteablefieldsadded : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoSenderCustomers",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "CargoSenderCustomers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "CargoSenderCustomers",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "CargoSenderCustomers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoReceiverCustomers",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "CargoReceiverCustomers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "CargoReceiverCustomers",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "CargoReceiverCustomers",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoOperations",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "CargoOperations",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "CargoOperations",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "CargoOperations",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoDetails",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "CargoDetails",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "CargoDetails",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "CargoDetails",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoCompanies",
            type: "datetime2",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime2");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "CargoCompanies",
            type: "nvarchar(max)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "DeletedAt",
            table: "CargoCompanies",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "CargoCompanies",
            type: "nvarchar(max)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "CargoSenderCustomers");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "CargoSenderCustomers");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "CargoSenderCustomers");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "CargoReceiverCustomers");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "CargoReceiverCustomers");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "CargoReceiverCustomers");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "CargoOperations");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "CargoOperations");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "CargoOperations");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "CargoDetails");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "CargoDetails");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "CargoDetails");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "CargoCompanies");

        migrationBuilder.DropColumn(
            name: "DeletedAt",
            table: "CargoCompanies");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "CargoCompanies");

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoSenderCustomers",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoReceiverCustomers",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoOperations",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoDetails",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "UpdatedAt",
            table: "CargoCompanies",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime2",
            oldNullable: true);
    }
}
