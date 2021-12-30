using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tekoding.KoIdentity.Abstraction.Test.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "KoIdentity.Abstraction");

            migrationBuilder.CreateTable(
                name: "DefaultEntity",
                schema: "KoIdentity.Abstraction",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()"),
                    ChangeDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getDate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DefaultEntity", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DefaultEntity",
                schema: "KoIdentity.Abstraction");
        }
    }
}
