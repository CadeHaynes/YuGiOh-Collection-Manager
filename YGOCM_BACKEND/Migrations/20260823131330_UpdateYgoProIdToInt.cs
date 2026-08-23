using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YGOCM_BACKEND.Migrations
{
    /// <inheritdoc />
    public partial class UpdateYgoProIdToInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Cards"
                ALTER COLUMN "YgoProId"
                TYPE integer USING ("YgoProId"::integer);
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Cards"
                ALTER COLUMN "YgoProId"
                TYPE text USING ("YgoProId"::text);
                """);
        }
    }
}
