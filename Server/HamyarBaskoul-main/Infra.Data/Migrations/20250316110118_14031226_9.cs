using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infra.Data.Migrations
{
    public partial class _14031226_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    PersonnelCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Departments = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BargeBaskouls",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateBarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeBarge = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeBaskol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateBaskol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNegahbani = table.Column<long>(type: "bigint", nullable: true),
                    DateNegahbani = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeNegahbani = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDTakhlie = table.Column<long>(type: "bigint", nullable: true),
                    DateTakhlie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeTakhlie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDBargiri = table.Column<long>(type: "bigint", nullable: true),
                    DateBargiri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeBargiri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDNemoneGiri = table.Column<long>(type: "bigint", nullable: true),
                    IDRanande = table.Column<long>(type: "bigint", nullable: true),
                    IDVasile = table.Column<long>(type: "bigint", nullable: true),
                    IDTypeMarkazHamz = table.Column<long>(type: "bigint", nullable: true),
                    IDMarkaz = table.Column<long>(type: "bigint", nullable: true),
                    IDShakhs = table.Column<long>(type: "bigint", nullable: true),
                    IDTafsili = table.Column<long>(type: "bigint", nullable: true),
                    IDTafsili2 = table.Column<long>(type: "bigint", nullable: true),
                    IDAnbar = table.Column<long>(type: "bigint", nullable: true),
                    IDKala = table.Column<long>(type: "bigint", nullable: true),
                    ShomareMashin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KerayeHaml = table.Column<float>(type: "real", nullable: true),
                    IDTypeJabejaiee = table.Column<long>(type: "bigint", nullable: true),
                    IDDarkhastAmaliat = table.Column<long>(type: "bigint", nullable: true),
                    TypeGheymatGozari = table.Column<int>(type: "int", nullable: true),
                    IDGheymatGozari = table.Column<long>(type: "bigint", nullable: true),
                    NerkhKala = table.Column<float>(type: "real", nullable: true),
                    VaznPor = table.Column<float>(type: "real", nullable: true),
                    VanKhali = table.Column<float>(type: "real", nullable: true),
                    VaznBasteBandi = table.Column<float>(type: "real", nullable: true),
                    TimeVaznPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeVaznKhali = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TypeBargeAnbar = table.Column<int>(type: "int", nullable: true),
                    Tozihat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountChap = table.Column<int>(type: "int", nullable: true),
                    DateChap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeChap = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MeghdarDarkhastAmaliat = table.Column<float>(type: "real", nullable: true),
                    KarbarTaeedKonandeDA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnvanRanandeh = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlgSabt = table.Column<bool>(type: "bit", nullable: true),
                    FlgEbtal = table.Column<bool>(type: "bit", nullable: true),
                    MablaghBaskol = table.Column<float>(type: "real", nullable: true),
                    Karbar_Ins = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Karbar_Up = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IDBaskul = table.Column<long>(type: "bigint", nullable: true),
                    IDWebBarge = table.Column<int>(type: "int", nullable: true),
                    TypeBarge = table.Column<int>(type: "int", nullable: true),
                    Karbar_Sabt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Sabt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Karbar_Ebtal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date_Ebtal = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateTimeBarge = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IDTypeBaskul = table.Column<long>(type: "bigint", nullable: true),
                    GhabzBaskolID = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BargeBaskouls", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BaseTables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ValueFarsi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "baskouls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScaleCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baskouls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuilderProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EducationBaseTableId = table.Column<int>(type: "int", nullable: true),
                    MaritalStatusBaseTableId = table.Column<int>(type: "int", nullable: true),
                    NumberOfDaughters = table.Column<int>(type: "int", nullable: true),
                    NumberOfSons = table.Column<int>(type: "int", nullable: true),
                    MainOccupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    WorkAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Renovation = table.Column<bool>(type: "bit", nullable: true),
                    Reconstruction = table.Column<bool>(type: "bit", nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true),
                    Personal = table.Column<bool>(type: "bit", nullable: true),
                    Partnership = table.Column<bool>(type: "bit", nullable: true),
                    Corporate = table.Column<bool>(type: "bit", nullable: true),
                    ManagementContract = table.Column<bool>(type: "bit", nullable: true),
                    AnnualActivityVolumeBaseTableId = table.Column<int>(type: "int", nullable: true),
                    BusinessPriorityBaseTableId = table.Column<int>(type: "int", nullable: true),
                    BuyingAndSellingBaseTableId = table.Column<int>(type: "int", nullable: true),
                    DesignBaseTableId = table.Column<int>(type: "int", nullable: true),
                    ExecutionBaseTableId = table.Column<int>(type: "int", nullable: true),
                    MaterialSupplyBaseTableId = table.Column<int>(type: "int", nullable: true),
                    PermitObtainingBaseTableId = table.Column<int>(type: "int", nullable: true),
                    PreviousCooperation = table.Column<bool>(type: "bit", nullable: true),
                    CooperationDuration = table.Column<int>(type: "int", nullable: true),
                    CooperationField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CooperationResults = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilderProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataProtectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FriendlyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Xml = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataProtectionKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mabanis",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IDLinq = table.Column<long>(type: "bigint", nullable: true),
                    Onvan = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tozihat = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KarbarIns = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KarbarUp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateIns = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateUp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mabanis", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ObjectForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NameFarsi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GroupNameFarsi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Departement = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ObjectTransactionTypeUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    SupplierId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectTransactionTypeUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RebuildAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RebuildabilityBaseTableId = table.Column<int>(type: "int", nullable: true),
                    Representative = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Modifiedby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RebuildAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignExecutionEmployers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProjectAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Tel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    Datedmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdmissionsSpecialistUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TypeOfAdmissionBaseTableId = table.Column<int>(type: "int", nullable: true),
                    IntroducingPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    TypeOfProjectFacadeDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectInteriorDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectPlanDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectAreaDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectEmployerBaseTableId = table.Column<int>(type: "int", nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitExistingSituation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VisitAdmissionsSpecialistUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisitWasItMeasured = table.Column<bool>(type: "bit", nullable: false),
                    VisitWasItPhotoTaken = table.Column<bool>(type: "bit", nullable: false),
                    VisitWasItVideoTaken = table.Column<bool>(type: "bit", nullable: false),
                    VisitEmployerComments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VisitImageUrl1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestFatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestChildNumber = table.Column<int>(type: "int", nullable: true),
                    RequestMunicipalArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestFavoriteColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLandlinePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RequestMobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RequestAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestRenovationReasonsBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestRenovationReasonsOthers = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestPropertyAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestPropertyUsageBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyUsageOthers = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestPropertyPositionBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyLength = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyWidth = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyArea = table.Column<int>(type: "int", nullable: true),
                    RequestYearOfManufacture = table.Column<int>(type: "int", nullable: true),
                    RequestBuildingSpecificationsDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestRequiredBedrooms = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredBathrooms = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredToilets = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredStorageSpace = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredTerraceSpace = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredLivingRoomSize = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredKitchenSize = table.Column<int>(type: "int", nullable: true),
                    RequestSecondarySpacesRequiredLaundry = table.Column<bool>(type: "bit", nullable: false),
                    RequestSecondarySpacesRequiredCloakroom = table.Column<bool>(type: "bit", nullable: false),
                    RequestSecondarySpacesRequiredSecondKitchen = table.Column<bool>(type: "bit", nullable: false),
                    RequestSecondarySpacesRequiredOthers = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestElectricalSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestHeatingSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestCoolingSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestFireAlarmSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestHasCamera = table.Column<bool>(type: "bit", nullable: true),
                    RequestFacilitiesDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestEntranceCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceHasPlantSpace = table.Column<bool>(type: "bit", nullable: true),
                    RequestEntranceDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestLivingRoomCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomHasTVWall = table.Column<bool>(type: "bit", nullable: true),
                    RequestLivingRoomHasShowcase = table.Column<bool>(type: "bit", nullable: true),
                    RequestLivingRoomDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestBedroomCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomHasWardrobe = table.Column<bool>(type: "bit", nullable: true),
                    RequestBedroomHasIroningBoard = table.Column<bool>(type: "bit", nullable: true),
                    RequestBedroomHasLibrary = table.Column<bool>(type: "bit", nullable: true),
                    RequestHasShowcaseDesk = table.Column<bool>(type: "bit", nullable: true),
                    RequestHasShowcaseDressingTable = table.Column<bool>(type: "bit", nullable: true),
                    RequestBedroomDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestBathroomCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBathroomFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBathroomWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBathroomLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestMasterDoorMaterialBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestBathroomDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestKitchenCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBetweenCabinetsMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestCabinetType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestHandleType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestCabinetHeight = table.Column<double>(type: "float", nullable: true),
                    RequestCountertopMaterial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestHasOvenSpace = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasMicrowaveSpace = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasGasCooktopSpace = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasBuiltInGasCooktop = table.Column<bool>(type: "bit", nullable: false),
                    RequestSuperCabinet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestStorageCabinet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestHasTrashBinSpace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestTerraceCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBarbecueBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestHasPlantSpaceOnTerrace = table.Column<bool>(type: "bit", nullable: true),
                    RequestTerraceDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignExecutionEmployers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignExecutionEmployers_BaseTables_TypeOfAdmissionBaseTableId",
                        column: x => x.TypeOfAdmissionBaseTableId,
                        principalTable: "BaseTables",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuilderProfileCompletedProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: false),
                    TotalArea = table.Column<double>(type: "float", nullable: false),
                    ConstructionQualityBaseTableId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilderProfileCompletedProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuilderProfileCompletedProjects_BuilderProfiles_BuilderProfileId",
                        column: x => x.BuilderProfileId,
                        principalTable: "BuilderProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuilderProfileEstimatedNeeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NeedDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EstimatedResolutionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FollowUpMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilderProfileEstimatedNeeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuilderProfileEstimatedNeeds_BuilderProfiles_BuilderProfileId",
                        column: x => x.BuilderProfileId,
                        principalTable: "BuilderProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuilderProfileFollowUpResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NeedDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Result = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilderProfileFollowUpResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuilderProfileFollowUpResults_BuilderProfiles_BuilderProfileId",
                        column: x => x.BuilderProfileId,
                        principalTable: "BuilderProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BuilderProfileOngoingAndUpcomingProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: true),
                    TotalArea = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CurrentStage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilderProfileOngoingAndUpcomingProjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BuilderProfileOngoingAndUpcomingProjects_BuilderProfiles_BuilderProfileId",
                        column: x => x.BuilderProfileId,
                        principalTable: "BuilderProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ObjectFormUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ObjectFormId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Supplier_ = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectFormUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ObjectFormUsers_ObjectForms_ObjectFormId",
                        column: x => x.ObjectFormId,
                        principalTable: "ObjectForms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignExecutionEmployerFollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    FollowUpMethodBaseTableId = table.Column<int>(type: "int", nullable: false),
                    Actions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NextFollowUpMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FollowUpOutcomeBaseTableId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DesignExecutionEmployerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignExecutionEmployerFollowUps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignExecutionEmployerFollowUps_DesignExecutionEmployers_DesignExecutionEmployerId",
                        column: x => x.DesignExecutionEmployerId,
                        principalTable: "DesignExecutionEmployers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DesignExecutionEmployerVisits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExistingSituation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AdmissionsSpecialistUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WasItMeasured = table.Column<bool>(type: "bit", nullable: false),
                    WasItPhotoTaken = table.Column<bool>(type: "bit", nullable: false),
                    WasItVideoTaken = table.Column<bool>(type: "bit", nullable: false),
                    EmployerComments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DesignExecutionEmployerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DesignExecutionEmployerVisits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DesignExecutionEmployerVisits_DesignExecutionEmployers_DesignExecutionEmployerId",
                        column: x => x.DesignExecutionEmployerId,
                        principalTable: "DesignExecutionEmployers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "55e62da2-2c86-4438-afdd-4025c16802d4", "User", "USER" },
                    { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "dde4cd0a-c55c-4c1b-874d-8d2e33c0c7eb", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Departments", "Email", "EmailConfirmed", "Family", "LockoutEnabled", "LockoutEnd", "Mobile", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PersonnelCode", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "8e445865-a24d-4543-a6c6-9443d048cdb9", 0, "7de05ab0-564d-49de-986a-bbf977e70579", null, "admin@localhost.com", true, "Manager", false, null, "0", "Manager", "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAEAACcQAAAAEBVmwtKAZCwB5vQrSQOzAGjo6EQItNmhaLdwnEPd4Q4N7aoTBiYmDdUqTSLS3wHMtg==", null, null, false, "af18f0b4-8f6d-4e16-b956-750144f3e4d0", false, "admin@localhost.com" },
                    { "9e224968-33e4-4652-b7b7-8574d048cdb9", 0, "457de0cb-4367-4939-b5eb-542191a662b1", null, "user@localhost.com", true, "User", false, null, "0", "User", "USER@LOCALHOST.COM", "USER@LOCALHOST.COM", "AQAAAAEAACcQAAAAENu/R9ZIq/ANzxX5EQy0MN86ie8onZipiGbGNTkHOi6mwycIuAEW7fK1AEf2atQJwA==", null, null, false, "8b5c7ddc-4304-4f49-a415-41430ac47561", false, "user@localhost.com" }
                });

            migrationBuilder.InsertData(
                table: "BaseTables",
                columns: new[] { "Id", "Description", "GroupName", "IsDeleted", "Value", "ValueFarsi" },
                values: new object[,]
                {
                    { 1, null, "TransactionType", false, null, "فروش" },
                    { 2, null, "RequestPropertyUsage", false, null, "سایر" }
                });

            migrationBuilder.InsertData(
                table: "ObjectForms",
                columns: new[] { "Id", "Departement", "GroupName", "GroupNameFarsi", "Name", "NameFarsi", "UserName" },
                values: new object[,]
                {
                    { 1, "realestate", "Applicant", "متقاضی", "Applicant", "مشاهده متقاضی", "user1" },
                    { 2, "design", "Form", "فرم", "FileRebuildability", "پروفایل بازسازی", "user2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cbc43a8e-f7bb-4445-baaf-1add431ffbbf", "8e445865-a24d-4543-a6c6-9443d048cdb9" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "cac43a6e-f7bb-4448-baaf-1add431ccbbf", "9e224968-33e4-4652-b7b7-8574d048cdb9" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BuilderProfileCompletedProjects_BuilderProfileId",
                table: "BuilderProfileCompletedProjects",
                column: "BuilderProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BuilderProfileEstimatedNeeds_BuilderProfileId",
                table: "BuilderProfileEstimatedNeeds",
                column: "BuilderProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BuilderProfileFollowUpResults_BuilderProfileId",
                table: "BuilderProfileFollowUpResults",
                column: "BuilderProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_BuilderProfileOngoingAndUpcomingProjects_BuilderProfileId",
                table: "BuilderProfileOngoingAndUpcomingProjects",
                column: "BuilderProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignExecutionEmployerFollowUps_DesignExecutionEmployerId",
                table: "DesignExecutionEmployerFollowUps",
                column: "DesignExecutionEmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignExecutionEmployers_TypeOfAdmissionBaseTableId",
                table: "DesignExecutionEmployers",
                column: "TypeOfAdmissionBaseTableId");

            migrationBuilder.CreateIndex(
                name: "IX_DesignExecutionEmployerVisits_DesignExecutionEmployerId",
                table: "DesignExecutionEmployerVisits",
                column: "DesignExecutionEmployerId");

            migrationBuilder.CreateIndex(
                name: "IX_ObjectFormUsers_ObjectFormId",
                table: "ObjectFormUsers",
                column: "ObjectFormId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BargeBaskouls");

            migrationBuilder.DropTable(
                name: "baskouls");

            migrationBuilder.DropTable(
                name: "BuilderProfileCompletedProjects");

            migrationBuilder.DropTable(
                name: "BuilderProfileEstimatedNeeds");

            migrationBuilder.DropTable(
                name: "BuilderProfileFollowUpResults");

            migrationBuilder.DropTable(
                name: "BuilderProfileOngoingAndUpcomingProjects");

            migrationBuilder.DropTable(
                name: "DataProtectionKeys");

            migrationBuilder.DropTable(
                name: "DesignExecutionEmployerFollowUps");

            migrationBuilder.DropTable(
                name: "DesignExecutionEmployerVisits");

            migrationBuilder.DropTable(
                name: "Mabanis");

            migrationBuilder.DropTable(
                name: "ObjectFormUsers");

            migrationBuilder.DropTable(
                name: "ObjectTransactionTypeUsers");

            migrationBuilder.DropTable(
                name: "RebuildAbilities");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "BuilderProfiles");

            migrationBuilder.DropTable(
                name: "DesignExecutionEmployers");

            migrationBuilder.DropTable(
                name: "ObjectForms");

            migrationBuilder.DropTable(
                name: "BaseTables");
        }
    }
}

