using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace TechnoByteLambders.MediTrackSensor.Platform.Shared.Infrastructure.Persistence.EFC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    entity_name = table.Column<string>(type: "varchar(400)", maxLength: 400, nullable: false),
                    entity_code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    schedule = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_admins", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    exact_location = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    type_of_medication = table.Column<string>(type: "longtext", nullable: false),
                    temperature = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    humidity = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    light_intensity = table.Column<decimal>(type: "decimal(6,1)", precision: 6, scale: 1, nullable: false),
                    air_quality = table.Column<decimal>(type: "decimal(6,1)", precision: 6, scale: 1, nullable: false),
                    vibration = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    atmospheric_pressure = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    suspended_particles = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: false),
                    door_status = table.Column<string>(type: "longtext", nullable: false),
                    establishment_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_devices", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "establishments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    establishment_name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    establishment_type = table.Column<string>(type: "longtext", nullable: false),
                    address = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    district = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    city_region = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    country = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    latitude = table.Column<decimal>(type: "decimal(10,8)", precision: 10, scale: 8, nullable: false),
                    longitude = table.Column<decimal>(type: "decimal(11,8)", precision: 11, scale: 8, nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    website = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_establishments", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    plan = table.Column<string>(type: "longtext", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    status = table.Column<string>(type: "longtext", nullable: false),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_subscriptions", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    type_of_transport = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    type_of_medication = table.Column<string>(type: "longtext", nullable: false),
                    temperature = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    humidity = table.Column<decimal>(type: "decimal(4,1)", precision: 4, scale: 1, nullable: false),
                    light_intensity = table.Column<decimal>(type: "decimal(6,1)", precision: 6, scale: 1, nullable: false),
                    air_quality = table.Column<decimal>(type: "decimal(6,1)", precision: 6, scale: 1, nullable: false),
                    vibration = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    atmospheric_pressure = table.Column<decimal>(type: "decimal(6,2)", precision: 6, scale: 2, nullable: false),
                    suspended_particles = table.Column<decimal>(type: "decimal(5,1)", precision: 5, scale: 1, nullable: false),
                    door_status = table.Column<string>(type: "longtext", nullable: false),
                    establishment_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_transports", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    dni = table.Column<string>(type: "varchar(8)", maxLength: 8, nullable: false),
                    email = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    phone = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    job_title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    entry_date = table.Column<DateOnly>(type: "date", nullable: false),
                    role = table.Column<string>(type: "longtext", nullable: false),
                    password_hash = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    photo = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "operators",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    alerts_answered = table.Column<int>(type: "int", nullable: false),
                    schedule = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    establishment_id = table.Column<int>(type: "int", nullable: false),
                    users_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_operators", x => x.id);
                    table.ForeignKey(
                        name: "f_k_operators_establishments_establishment_id",
                        column: x => x.establishment_id,
                        principalTable: "establishments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "i_x_operators_establishment_id",
                table: "operators",
                column: "establishment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "devices");

            migrationBuilder.DropTable(
                name: "operators");

            migrationBuilder.DropTable(
                name: "subscriptions");

            migrationBuilder.DropTable(
                name: "transports");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "establishments");
        }
    }
}
