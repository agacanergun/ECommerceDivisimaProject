using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Divisima.DAL.Migrations
{
    /// <inheritdoc />
    public partial class migtest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pıcture",
                table: "Slide",
                newName: "Picture");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Picture",
                table: "Slide",
                newName: "Pıcture");
        }
    }
}
