using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mcpserver.Migrations
{
    /// <inheritdoc />
    public partial class Editcontract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_m_lotcontract_ContractEntityId",
                table: "m_lotcontract",
                column: "ContractEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_m_lotcontract_m_contracts_ContractEntityId",
                table: "m_lotcontract",
                column: "ContractEntityId",
                principalTable: "m_contracts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_m_lotcontract_m_contracts_ContractEntityId",
                table: "m_lotcontract");

            migrationBuilder.DropIndex(
                name: "IX_m_lotcontract_ContractEntityId",
                table: "m_lotcontract");
        }
    }
}
