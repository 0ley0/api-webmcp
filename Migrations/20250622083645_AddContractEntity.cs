using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace mcpserver.Migrations
{
    /// <inheritdoc />
    public partial class AddContractEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "m_contracts",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contract_name = table.Column<string>(type: "text", nullable: false),
                    customer_name = table.Column<string>(type: "text", nullable: false),
                    contract_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_contracts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "m_lotcontract",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    lot_name = table.Column<string>(type: "text", nullable: false),
                    lot_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ContractEntityId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_m_lotcontract", x => x.id);
                    table.ForeignKey(
                        name: "FK_m_lotcontract_m_contracts_ContractEntityId",
                        column: x => x.ContractEntityId,
                        principalTable: "m_contracts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_m_lotcontract_ContractEntityId",
                table: "m_lotcontract",
                column: "ContractEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "m_lotcontract");

            migrationBuilder.DropTable(
                name: "m_contracts");
        }
    }
}
