using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioDesk.App.Migrations
{
    /// <inheritdoc />
    public partial class CorrigirValoresDinamicos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCriacao", "UltimaConsulta" },
                values: new object[] { new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataCriacao", "UltimaConsulta" },
                values: new object[] { new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataCriacao", "UltimaConsulta" },
                values: new object[] { new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCriacao", "UltimaConsulta" },
                values: new object[] { new DateTime(2025, 8, 21, 23, 51, 19, 787, DateTimeKind.Local).AddTicks(9311), new DateTime(2025, 9, 13, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(9) });

            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataCriacao", "UltimaConsulta" },
                values: new object[] { new DateTime(2025, 8, 6, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1025), new DateTime(2025, 9, 6, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1035) });

            migrationBuilder.UpdateData(
                table: "Pacientes",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataCriacao", "UltimaConsulta" },
                values: new object[] { new DateTime(2025, 8, 31, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1053), new DateTime(2025, 9, 17, 23, 51, 19, 788, DateTimeKind.Local).AddTicks(1057) });
        }
    }
}
