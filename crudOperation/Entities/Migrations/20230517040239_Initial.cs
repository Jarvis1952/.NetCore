using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryID);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CountryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    ReceiveNewsLetters = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonID);
                });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "CountryID", "CountryName" },
                values: new object[,]
                {
                    { new Guid("600e27c4-18e8-4487-9151-fe7020de943e"), "UK" },
                    { new Guid("6b69260c-3516-46a4-b7a2-e6df45e303ac"), "India" },
                    { new Guid("a1c80eee-69ef-49b1-a41c-9359e77dd111"), "Canada" },
                    { new Guid("d919d5b4-832e-44d9-80b0-1dd5a2d5e081"), "USA" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonID", "Address", "CountryID", "DateOfBirth", "Email", "Gender", "PersonName", "ReceiveNewsLetters" },
                values: new object[,]
                {
                    { new Guid("42ff717e-324d-43ac-9ee3-d8785077dceb"), "28022 Blaine Alley", new Guid("a1c80eee-69ef-49b1-a41c-9359e77dd111"), new DateTime(2011, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "walbrighton3@rakuten.co.jp", "Male", "Wilbur", false },
                    { new Guid("4545bf56-d313-4872-a506-3b081d8ab008"), "30703 Chinook Center", new Guid("600e27c4-18e8-4487-9151-fe7020de943e"), new DateTime(2000, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "sousley2@china.com.cn", "Female", "Sybilla", true },
                    { new Guid("6bd65612-c114-44c9-8e20-134513546ed7"), "625 Fairview Road", new Guid("6b69260c-3516-46a4-b7a2-e6df45e303ac"), new DateTime(2013, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "jcotta1@oracle.com", "Male", "Jedediah", false },
                    { new Guid("ddab3d49-f5b6-46cc-858d-3af5bd5a8827"), "6 Weeping Birch Lane", new Guid("d919d5b4-832e-44d9-80b0-1dd5a2d5e081"), new DateTime(2014, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "einggall0@ed.gov", "Female", "Ellary", true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
