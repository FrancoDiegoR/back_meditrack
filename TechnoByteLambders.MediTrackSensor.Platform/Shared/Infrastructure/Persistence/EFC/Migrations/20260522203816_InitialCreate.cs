using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace CatchUpPlatform.API.Shared.Infrastructure.Persistence.EFC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            // 1. TABLA: users
            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    dni = table.Column<string>(type: "varchar(20)", nullable: false),
                    email = table.Column<string>(type: "varchar(255)", nullable: false),
                    phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    job_title = table.Column<string>(type: "varchar(150)", nullable: true),
                    entry_date = table.Column<string>(type: "varchar(50)", nullable: true), // Guardado como string o Date según el JSON
                    role = table.Column<string>(type: "varchar(50)", nullable: false),
                    password = table.Column<string>(type: "varchar(255)", nullable: false),
                    photo = table.Column<string>(type: "varchar(500)", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // 2. TABLA: admins
            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    entity_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    entity_code = table.Column<string>(type: "varchar(100)", nullable: false),
                    schedule = table.Column<string>(type: "varchar(100)", nullable: true),
                    users_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_admins", x => x.id);
                    table.ForeignKey(
                        name: "f_k_admins_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // 3. TABLA: subscriptions
            migrationBuilder.CreateTable(
                name: "subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    plan = table.Column<string>(type: "varchar(50)", nullable: false),
                    start_date = table.Column<string>(type: "varchar(50)", nullable: true),
                    end_date = table.Column<string>(type: "varchar(50)", nullable: true),
                    status = table.Column<string>(type: "varchar(50)", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "f_k_subscriptions_admins_admin_id",
                        column: x => x.admin_id,
                        principalTable: "admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // 4. TABLA: establishments
            migrationBuilder.CreateTable(
                name: "establishments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    establishment_name = table.Column<string>(type: "varchar(255)", nullable: false),
                    establishment_type = table.Column<string>(type: "varchar(100)", nullable: false),
                    address = table.Column<string>(type: "varchar(255)", nullable: true),
                    district = table.Column<string>(type: "varchar(100)", nullable: true),
                    city_region = table.Column<string>(type: "varchar(100)", nullable: true),
                    country = table.Column<string>(type: "varchar(100)", nullable: true),
                    phone = table.Column<string>(type: "varchar(50)", nullable: true),
                    email = table.Column<string>(type: "varchar(255)", nullable: true),
                    website = table.Column<string>(type: "varchar(255)", nullable: true),
                    admin_id = table.Column<int>(type: "int", nullable: false),
                    latitude = table.Column<double>(type: "double", nullable: false),
                    longitude = table.Column<double>(type: "double", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_establishments", x => x.id);
                    table.ForeignKey(
                        name: "f_k_establishments_admins_admin_id",
                        column: x => x.admin_id,
                        principalTable: "admins",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // 5. TABLA: operators
            migrationBuilder.CreateTable(
                name: "operators",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    alerts_answered = table.Column<int>(type: "int", nullable: false),
                    schedule = table.Column<string>(type: "varchar(100)", nullable: true),
                    establishment_id = table.Column<int>(type: "int", nullable: false),
                    users_id = table.Column<int>(type: "int", nullable: false)
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
                    table.ForeignKey(
                        name: "f_k_operators_users_users_id",
                        column: x => x.users_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // 6. TABLA: transports
            migrationBuilder.CreateTable(
                name: "transports",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    type_of_transport = table.Column<string>(type: "varchar(255)", nullable: false),
                    type_of_medication = table.Column<string>(type: "varchar(255)", nullable: false),
                    temperature = table.Column<double>(type: "double", nullable: false),
                    humidity = table.Column<double>(type: "double", nullable: false),
                    light_intensity = table.Column<double>(type: "double", nullable: false),
                    air_quality = table.Column<double>(type: "double", nullable: false),
                    vibration = table.Column<double>(type: "double", nullable: false),
                    door_status = table.Column<string>(type: "varchar(50)", nullable: false),
                    atmospheric_pressure = table.Column<double>(type: "double", nullable: false),
                    suspended_particles = table.Column<double>(type: "double", nullable: false),
                    establishment_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_transports", x => x.id);
                    table.ForeignKey(
                        name: "f_k_transports_establishments_establishment_id",
                        column: x => x.establishment_id,
                        principalTable: "establishments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // 7. TABLA: devices
            migrationBuilder.CreateTable(
                name: "devices",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    exact_location = table.Column<string>(type: "varchar(255)", nullable: false),
                    type_of_medication = table.Column<string>(type: "varchar(255)", nullable: false),
                    temperature = table.Column<double>(type: "double", nullable: false),
                    humidity = table.Column<double>(type: "double", nullable: false),
                    light_intensity = table.Column<double>(type: "double", nullable: false),
                    air_quality = table.Column<double>(type: "double", nullable: false),
                    vibration = table.Column<double>(type: "double", nullable: false),
                    door_status = table.Column<string>(type: "varchar(50)", nullable: false),
                    atmospheric_pressure = table.Column<double>(type: "double", nullable: false),
                    suspended_particles = table.Column<double>(type: "double", nullable: false),
                    establishment_id = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_devices", x => x.id);
                    table.ForeignKey(
                        name: "f_k_devices_establishments_establishment_id",
                        column: x => x.establishment_id,
                        principalTable: "establishments",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            // Índices para búsquedas de claves foráneas rápidas (Manteniento el estilo i_x_...)
            migrationBuilder.CreateIndex(name: "i_x_admins_users_id", table: "admins", column: "users_id");
            migrationBuilder.CreateIndex(name: "i_x_subscriptions_admin_id", table: "subscriptions", column: "admin_id");
            migrationBuilder.CreateIndex(name: "i_x_establishments_admin_id", table: "establishments", column: "admin_id");
            migrationBuilder.CreateIndex(name: "i_x_operators_establishment_id", table: "operators", column: "establishment_id");
            migrationBuilder.CreateIndex(name: "i_x_operators_users_id", table: "operators", column: "users_id");
            migrationBuilder.CreateIndex(name: "i_x_transports_establishment_id", table: "transports", column: "establishment_id");
            migrationBuilder.CreateIndex(name: "i_x_devices_establishment_id", table: "devices", column: "establishment_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "devices");
            migrationBuilder.DropTable(name: "transports");
            migrationBuilder.DropTable(name: "operators");
            migrationBuilder.DropTable(name: "establishments");
            migrationBuilder.DropTable(name: "subscriptions");
            migrationBuilder.DropTable(name: "admins");
            migrationBuilder.DropTable(name: "users");
        }
    }
}