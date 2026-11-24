using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Comments.Migrations;

/// <inheritdoc />
public partial class _20261123112010_initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UserComments",
            columns: table => new
            {
                UserCommentId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                NameSurname = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                ImageUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                CommentDetail = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                Rating = table.Column<int>(type: "int", nullable: false),
                CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                Status = table.Column<bool>(type: "bit", nullable: false),
                ProductId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserComments", x => x.UserCommentId);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserComments");
    }
}
