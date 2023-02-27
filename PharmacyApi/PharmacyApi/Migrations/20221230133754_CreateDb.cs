using Microsoft.EntityFrameworkCore.Migrations;

namespace PharmacyApi.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    BranchID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(maxLength: 50, nullable: false),
                    BranchPhone = table.Column<string>(maxLength: 11, nullable: false),
                    BranchAdress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.BranchID);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(maxLength: 50, nullable: false),
                    CompanyPhone = table.Column<string>(maxLength: 11, nullable: false),
                    CompanyAdress = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.CompanyID);
                });

            migrationBuilder.CreateTable(
                name: "Staffs",
                columns: table => new
                {
                    StaffID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffName = table.Column<string>(maxLength: 50, nullable: false),
                    StaffAge = table.Column<string>(nullable: false),
                    StaffPhone = table.Column<string>(maxLength: 11, nullable: false),
                    StaffAdress = table.Column<string>(maxLength: 100, nullable: false),
                    StaffSalary = table.Column<string>(nullable: false),
                    BranchID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffID);
                    table.ForeignKey(
                        name: "FK_Staffs_Branches_BranchID",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "BranchID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    MedicineID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicineName = table.Column<string>(maxLength: 50, nullable: false),
                    MedicineDescription = table.Column<string>(maxLength: 100, nullable: false),
                    MedicinePrice = table.Column<string>(nullable: false),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.MedicineID);
                    table.ForeignKey(
                        name: "FK_Medicines_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalTable: "Companies",
                        principalColumn: "CompanyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(maxLength: 50, nullable: false),
                    CustomerPhone = table.Column<string>(maxLength: 11, nullable: false),
                    MedicineID = table.Column<int>(nullable: false),
                    StaffID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                    table.ForeignKey(
                        name: "FK_Customers_Medicines_MedicineID",
                        column: x => x.MedicineID,
                        principalTable: "Medicines",
                        principalColumn: "MedicineID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Customers_Staffs_StaffID",
                        column: x => x.StaffID,
                        principalTable: "Staffs",
                        principalColumn: "StaffID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_MedicineID",
                table: "Customers",
                column: "MedicineID");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_StaffID",
                table: "Customers",
                column: "StaffID");

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_CompanyID",
                table: "Medicines",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Staffs_BranchID",
                table: "Staffs",
                column: "BranchID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Staffs");

            migrationBuilder.DropTable(
                name: "Companies");

            migrationBuilder.DropTable(
                name: "Branches");
        }
    }
}
