using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TareasMVC.Migrations
{
    /// <inheritdoc />
    public partial class adminRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"IF NOT EXISTS 
                                    (SELECT Id FROM AspNetRoles WHERE Id = 'd4c450b5-d463-45d6-8fe6-e48fbc72ae7e')
                                    BEGIN
	                                    INSERT AspNetRoles (Id, [Name], [NormalizedName])
	                                    VALUES ('d4c450b5-d463-45d6-8fe6-e48fbc72ae7e', 'admin', 'ADMIN')
                                    END");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE AspNetRoles WHERE Id = 'd4c450b5-d463-45d6-8fe6-e48fbc72ae7e'");
        }
    }
}
