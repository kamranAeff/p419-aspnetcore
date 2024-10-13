using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UserTokens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.AddColumn<byte>(
                name: "Type",
                schema: "Membership",
                table: "UserTokens",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpireDate",
                schema: "Membership",
                table: "UserTokens",
                type: "smalldatetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDisable",
                schema: "Membership",
                table: "UserTokens",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name", "Type", "ExpireDate" });

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "5f59a77d-d608-4492-bfbc-5af878f60852");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "72ef31a6-5d60-46c7-a164-038ba563e6f3");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEKzm9Qwka3s9LF4jpgiO81Fb1gbqaFNmSwH8fZFYf1nYQhX8CbIA2qRq+rsetOO/fA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "Type",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "ExpireDate",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.DropColumn(
                name: "IsDisable",
                schema: "Membership",
                table: "UserTokens");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserTokens",
                schema: "Membership",
                table: "UserTokens",
                columns: new[] { "UserId", "LoginProvider", "Name" });

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cec2c85f-893f-438b-b528-a6cb1be7a777");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "227a1a27-2270-42ab-b1da-03fbe24822c3");

            migrationBuilder.UpdateData(
                schema: "Membership",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "PasswordHash",
                value: "AQAAAAIAAYagAAAAEL1FK766fwOzNi4ofvfHGlNOPRrGwAk1sC75ez1q2VOyG5v89JB60+ktojX4cAelXQ==");
        }
    }
}
