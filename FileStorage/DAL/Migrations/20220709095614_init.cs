using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FileSecureLevel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SecureLevelName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileSecureLevel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FileStreamCol = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    FileCreateDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                    table.ForeignKey(
                        name: "FK_File_FileType_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FileType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FullFileInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    FileSecureLevelId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FullFileInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FullFileInfo_File_FileId",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FullFileInfo_FileSecureLevel_FileSecureLevelId",
                        column: x => x.FileSecureLevelId,
                        principalTable: "FileSecureLevel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FullFileInfo_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_File_FileTypeId",
                table: "File",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FullFileInfo_FileId",
                table: "FullFileInfo",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FullFileInfo_FileSecureLevelId",
                table: "FullFileInfo",
                column: "FileSecureLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_FullFileInfo_UserId",
                table: "FullFileInfo",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FullFileInfo");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "FileSecureLevel");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "FileType");
        }
    }
}
