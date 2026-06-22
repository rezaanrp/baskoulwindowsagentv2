using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveRealEstateModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BuilderProfileCompletedProjects");

            migrationBuilder.DropTable(
                name: "BuilderProfileEstimatedNeeds");

            migrationBuilder.DropTable(
                name: "BuilderProfileFollowUpResults");

            migrationBuilder.DropTable(
                name: "BuilderProfileOngoingAndUpcomingProjects");

            migrationBuilder.DropTable(
                name: "DesignExecutionEmployerFollowUps");

            migrationBuilder.DropTable(
                name: "DesignExecutionEmployerVisits");

            migrationBuilder.DropTable(
                name: "RebuildAbilities");

            migrationBuilder.DropTable(
                name: "BuilderProfiles");

            migrationBuilder.DropTable(
                name: "DesignExecutionEmployers");

            migrationBuilder.DeleteData(
                table: "ObjectForms",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ObjectForms",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BuilderProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnnualActivityVolumeBaseTableId = table.Column<int>(type: "int", nullable: true),
                    BusinessPriorityBaseTableId = table.Column<int>(type: "int", nullable: true),
                    BuyingAndSellingBaseTableId = table.Column<int>(type: "int", nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CooperationDuration = table.Column<int>(type: "int", nullable: true),
                    CooperationField = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CooperationResults = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Corporate = table.Column<bool>(type: "bit", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    DesignBaseTableId = table.Column<int>(type: "int", nullable: true),
                    EducationBaseTableId = table.Column<int>(type: "int", nullable: true),
                    ExecutionBaseTableId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    MainOccupation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ManagementContract = table.Column<bool>(type: "bit", nullable: true),
                    MaritalStatusBaseTableId = table.Column<int>(type: "int", nullable: true),
                    MaterialSupplyBaseTableId = table.Column<int>(type: "int", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NumberOfDaughters = table.Column<int>(type: "int", nullable: true),
                    NumberOfSons = table.Column<int>(type: "int", nullable: true),
                    Partnership = table.Column<bool>(type: "bit", nullable: true),
                    PermitObtainingBaseTableId = table.Column<int>(type: "int", nullable: true),
                    Personal = table.Column<bool>(type: "bit", nullable: true),
                    PreviousCooperation = table.Column<bool>(type: "bit", nullable: true),
                    Reconstruction = table.Column<bool>(type: "bit", nullable: true),
                    Renovation = table.Column<bool>(type: "bit", nullable: true),
                    WorkAddress = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    YearsOfExperience = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BuilderProfiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DesignExecutionEmployers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOfAdmissionBaseTableId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    AdmissionsSpecialistUserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Datedmission = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IntroducingPerson = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProjectAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RequestAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestBarbecueBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestBathroomCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBathroomDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestBathroomFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBathroomLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBathroomWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestBedroomFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomHasIroningBoard = table.Column<bool>(type: "bit", nullable: true),
                    RequestBedroomHasLibrary = table.Column<bool>(type: "bit", nullable: true),
                    RequestBedroomHasWardrobe = table.Column<bool>(type: "bit", nullable: true),
                    RequestBedroomLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBedroomWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBetweenCabinetsMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestBuildingSpecificationsDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestCabinetHeight = table.Column<double>(type: "float", nullable: true),
                    RequestCabinetType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestChildNumber = table.Column<int>(type: "int", nullable: true),
                    RequestCoolingSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestCountertopMaterial = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RequestElectricalSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestEntranceCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestEntranceFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceHasPlantSpace = table.Column<bool>(type: "bit", nullable: true),
                    RequestEntranceLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestEntranceWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestFacilitiesDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestFatherName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestFavoriteColor = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestFireAlarmSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestHandleType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestHasBuiltInGasCooktop = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasCamera = table.Column<bool>(type: "bit", nullable: true),
                    RequestHasGasCooktopSpace = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasMicrowaveSpace = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasOvenSpace = table.Column<bool>(type: "bit", nullable: false),
                    RequestHasPlantSpaceOnTerrace = table.Column<bool>(type: "bit", nullable: true),
                    RequestHasShowcaseDesk = table.Column<bool>(type: "bit", nullable: true),
                    RequestHasShowcaseDressingTable = table.Column<bool>(type: "bit", nullable: true),
                    RequestHasTrashBinSpace = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestHeatingSystemBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestKitchenCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestKitchenFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestKitchenWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLandlinePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RequestLivingRoomCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestLivingRoomFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomHasShowcase = table.Column<bool>(type: "bit", nullable: true),
                    RequestLivingRoomHasTVWall = table.Column<bool>(type: "bit", nullable: true),
                    RequestLivingRoomLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestLivingRoomWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestMasterDoorMaterialBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestMobilePhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RequestMunicipalArea = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RequestPropertyAddress = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestPropertyArea = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyLength = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyPositionBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyUsageBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestPropertyUsageOthers = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestPropertyWidth = table.Column<int>(type: "int", nullable: true),
                    RequestRenovationReasonsBaseTableId = table.Column<int>(type: "int", nullable: true),
                    RequestRenovationReasonsOthers = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestRequiredBathrooms = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredBedrooms = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredKitchenSize = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredLivingRoomSize = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredStorageSpace = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredTerraceSpace = table.Column<int>(type: "int", nullable: true),
                    RequestRequiredToilets = table.Column<int>(type: "int", nullable: true),
                    RequestSecondarySpacesRequiredCloakroom = table.Column<bool>(type: "bit", nullable: false),
                    RequestSecondarySpacesRequiredLaundry = table.Column<bool>(type: "bit", nullable: false),
                    RequestSecondarySpacesRequiredOthers = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestSecondarySpacesRequiredSecondKitchen = table.Column<bool>(type: "bit", nullable: false),
                    RequestStorageCabinet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestSuperCabinet = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceCeilingMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceDescription = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    RequestTerraceFloorMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceLighting = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestTerraceWallMaterial = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RequestYearOfManufacture = table.Column<int>(type: "int", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    TypeOfProjectAreaDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectEmployerBaseTableId = table.Column<int>(type: "int", nullable: true),
                    TypeOfProjectFacadeDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectInteriorDesign = table.Column<bool>(type: "bit", nullable: false),
                    TypeOfProjectPlanDesign = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitAdmissionsSpecialistUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VisitEmployerComments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VisitExistingSituation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    VisitImageUrl1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl4 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitImageUrl5 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitWasItMeasured = table.Column<bool>(type: "bit", nullable: false),
                    VisitWasItPhotoTaken = table.Column<bool>(type: "bit", nullable: false),
                    VisitWasItVideoTaken = table.Column<bool>(type: "bit", nullable: false)
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
                name: "RebuildAbilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Family = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Modifiedby = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    RebuildabilityBaseTableId = table.Column<int>(type: "int", nullable: true),
                    Representative = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RebuildAbilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BuilderProfileCompletedProjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConstructionQualityBaseTableId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalArea = table.Column<double>(type: "float", nullable: false)
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
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EstimatedResolutionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FollowUpMethod = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NeedDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    ResponsiblePerson = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NeedDescription = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    ProjectNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Result = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
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
                    BuilderProfileId = table.Column<int>(type: "int", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    CurrentStage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NumberOfFloors = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalArea = table.Column<int>(type: "int", nullable: true)
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
                name: "DesignExecutionEmployerFollowUps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DesignExecutionEmployerId = table.Column<int>(type: "int", nullable: false),
                    Actions = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FollowUpMethodBaseTableId = table.Column<int>(type: "int", nullable: false),
                    FollowUpOutcomeBaseTableId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    NextFollowUpMethod = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
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
                    DesignExecutionEmployerId = table.Column<int>(type: "int", nullable: false),
                    AdmissionsSpecialistUserId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CodMarkaz = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    EmployerComments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ExistingSituation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WasItMeasured = table.Column<bool>(type: "bit", nullable: false),
                    WasItPhotoTaken = table.Column<bool>(type: "bit", nullable: false),
                    WasItVideoTaken = table.Column<bool>(type: "bit", nullable: false)
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
                table: "ObjectForms",
                columns: new[] { "Id", "CodMarkaz", "CreateIp", "Departement", "GroupName", "GroupNameFarsi", "ModifyIp", "Name", "NameFarsi", "UserName" },
                values: new object[,]
                {
                    { 1, null, null, "realestate", "Applicant", "متقاضی", null, "Applicant", "مشاهده متقاضی", "user1" },
                    { 2, null, null, "design", "Form", "فرم", null, "FileRebuildability", "پروفایل بازسازی", "user2" }
                });

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
        }
    }
}

