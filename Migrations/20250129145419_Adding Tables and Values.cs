using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Restaurant_API.Migrations
{
    /// <inheritdoc />
    public partial class AddingTablesandValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Desserts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreparationTimeInMin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desserts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    VolumeInMl = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PreparationTimeInMin = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TableNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsOccupied = table.Column<bool>(type: "bit", nullable: false),
                    IsReserved = table.Column<bool>(type: "bit", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ReservationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DessertId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MealId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DrinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seats_Desserts_DessertId",
                        column: x => x.DessertId,
                        principalTable: "Desserts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Drinks_DrinkId",
                        column: x => x.DrinkId,
                        principalTable: "Drinks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seats_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Desserts",
                columns: new[] { "Id", "Category", "Description", "IsAvailable", "Name", "PreparationTimeInMin", "Price" },
                values: new object[,]
                {
                    { new Guid("1d7be41b-5f4d-4398-9e2c-93d113016866"), "Cake", "New York style", true, "Cheesecake", 5, 7.99m },
                    { new Guid("414033df-b813-49d1-b1e6-e4722c17aa74"), "Ice Cream", "Vanilla with berries", true, "Ice Cream", 3, 5.99m },
                    { new Guid("71e1f68e-adf1-451b-ad74-3974891ba91a"), "Pie", "Warm apple pie", true, "Apple Pie", 15, 6.99m },
                    { new Guid("c4511eba-e269-4566-b56e-2ee3662a6dd2"), "Cake", "Rich chocolate layer cake", true, "Chocolate Cake", 10, 8.99m },
                    { new Guid("d1e2f3a4-b5c6-7890-1234-56789abcdef3"), "Italian", "Classic Italian dessert", true, "Tiramisu", 5, 7.99m }
                });

            migrationBuilder.InsertData(
                table: "Drinks",
                columns: new[] { "Id", "Description", "IsAvailable", "Name", "Price", "VolumeInMl" },
                values: new object[,]
                {
                    { new Guid("a2b3c4d5-e6f7-7890-1234-56789abcdef6"), "Local IPA", true, "Craft Beer", 5.99m, 500 },
                    { new Guid("b2c3d4e5-f6a7-7890-1234-56789abcdef7"), "Freshly squeezed", true, "Fresh Orange Juice", 3.99m, 250 },
                    { new Guid("c2d3e4f5-a6b7-7890-1234-56789abcdef8"), "Mineral water", true, "Sparkling Water", 2.99m, 330 },
                    { new Guid("d2e3f4a5-b6c7-7890-1234-56789abcdef9"), "Single shot", true, "Espresso", 2.99m, 50 },
                    { new Guid("f1a2b3c4-d5e6-7890-1234-56789abcdef5"), "Red wine", true, "House Wine", 7.99m, 175 }
                });

            migrationBuilder.InsertData(
                table: "Meals",
                columns: new[] { "Id", "Category", "Description", "Name", "PreparationTimeInMin", "Price" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-7890-1234-56789abcdef0"), "Main Course", "Fresh salmon with herbs", "Grilled Salmon", 25, 25.99m },
                    { new Guid("b1c2d3e4-f5a6-7890-1234-56789abcdef1"), "Main Course", "Premium cut beef steak", "Beef Steak", 20, 29.99m },
                    { new Guid("c1d2e3f4-a5b6-7890-1234-56789abcdef2"), "Pasta", "Creamy pasta with chicken", "Chicken Alfredo", 15, 18.99m },
                    { new Guid("d1e2f3a4-b5c6-7890-1234-56789abcdef3"), "Pizza", "Fresh vegetables on crispy base", "Vegetarian Pizza", 20, 16.99m },
                    { new Guid("e1f2a3b4-c5d6-7890-1234-56789abcdef4"), "Salad", "Classic caesar with chicken", "Caesar Salad", 10, 12.99m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Seats_DessertId",
                table: "Seats",
                column: "DessertId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_DrinkId",
                table: "Seats",
                column: "DrinkId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_MealId",
                table: "Seats",
                column: "MealId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Desserts");

            migrationBuilder.DropTable(
                name: "Drinks");

            migrationBuilder.DropTable(
                name: "Meals");
        }
    }
}
