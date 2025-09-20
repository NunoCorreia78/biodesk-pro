using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BioDesk.App.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarAlergiasAmbientaisDetalhadas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AlergiaAcaros",
                table: "QuestionariosSaude",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlergiaMofo",
                table: "QuestionariosSaude",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlergiaPelosAnimais",
                table: "QuestionariosSaude",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlergiaPoeira",
                table: "QuestionariosSaude",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AlergiaPolen",
                table: "QuestionariosSaude",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "AlergiasAmbientaisOutras",
                table: "QuestionariosSaude",
                type: "TEXT",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlergiaAcaros",
                table: "QuestionariosSaude");

            migrationBuilder.DropColumn(
                name: "AlergiaMofo",
                table: "QuestionariosSaude");

            migrationBuilder.DropColumn(
                name: "AlergiaPelosAnimais",
                table: "QuestionariosSaude");

            migrationBuilder.DropColumn(
                name: "AlergiaPoeira",
                table: "QuestionariosSaude");

            migrationBuilder.DropColumn(
                name: "AlergiaPolen",
                table: "QuestionariosSaude");

            migrationBuilder.DropColumn(
                name: "AlergiasAmbientaisOutras",
                table: "QuestionariosSaude");
        }
    }
}
