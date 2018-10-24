using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Mas.Domain.UserCenter.Migrations
{
    public partial class UserCenterMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sys_User",
                columns: table => new
                {
                    State = table.Column<int>(nullable: false),
                    RowVersion = table.Column<byte[]>(rowVersion: true, nullable: true),
                    Id = table.Column<string>(maxLength: 30, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    CreatorId = table.Column<string>(nullable: true),
                    CreatorName = table.Column<string>(nullable: true),
                    Modified = table.Column<DateTime>(nullable: false),
                    ModifierId = table.Column<string>(nullable: true),
                    ModifierName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    UserName = table.Column<string>(maxLength: 30, nullable: true),
                    LoginName = table.Column<string>(maxLength: 30, nullable: true),
                    IdCard = table.Column<string>(maxLength: 18, nullable: true),
                    WeChatNum = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(maxLength: 16, nullable: true),
                    HeadPhoto = table.Column<string>(nullable: true),
                    Password = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sys_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sys_User");
        }
    }
}
