using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BluesoftBank.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cuentas",
                columns: table => new
                {
                    NumeroCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Saldo = table.Column<decimal>(type: "REAL", nullable: false),
                    CiudadOrigen = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "BLOB", rowVersion: true, nullable: true, defaultValueSql: "randomblob(8)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuentas", x => x.NumeroCuenta);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Valor = table.Column<decimal>(type: "REAL", nullable: false),
                    TipoTransaccion = table.Column<int>(type: "INTEGER", nullable: false),
                    CiudadOperacion = table.Column<string>(type: "TEXT", nullable: false),
                    NumeroCuenta = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacciones_Cuentas_NumeroCuenta",
                        column: x => x.NumeroCuenta,
                        principalTable: "Cuentas",
                        principalColumn: "NumeroCuenta",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_NumeroCuenta",
                table: "Transacciones",
                column: "NumeroCuenta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Cuentas");
        }
    }
}
