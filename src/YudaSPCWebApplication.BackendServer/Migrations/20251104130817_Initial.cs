using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YudaSPCWebApplication.BackendServer.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "KnowledgeBaseSepence");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntRoleID = table.Column<int>(type: "int", nullable: false),
                    StrRoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IntLevel = table.Column<int>(type: "int", nullable: false),
                    IntRoleUser = table.Column<int>(type: "int", nullable: true),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntUserID = table.Column<int>(type: "int", nullable: false),
                    StrRoleID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    StrPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrEmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrStaffID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrDepartment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtLastActivityTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntEnable = table.Column<int>(type: "int", nullable: true),
                    StrSelectedAreaID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
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
                name: "MeasDatas",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntCharacteristicID = table.Column<int>(type: "int", nullable: true),
                    VarCharacteristicValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    VarCharacteristicRange = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    DtTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntDataCollection = table.Column<int>(type: "int", nullable: true),
                    DtTimeMeasure = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntLineID = table.Column<int>(type: "int", nullable: true),
                    IntJobID = table.Column<int>(type: "int", nullable: true),
                    IntProductionID = table.Column<int>(type: "int", nullable: true),
                    IntOutputQty = table.Column<int>(type: "int", nullable: true),
                    IntSampleIndex = table.Column<int>(type: "int", nullable: true),
                    IntUserID = table.Column<int>(type: "int", nullable: true),
                    StrFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrOutputNotes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IntSampleQty = table.Column<int>(type: "int", nullable: true),
                    IntMoldID = table.Column<int>(type: "int", nullable: true),
                    IntCavityID = table.Column<int>(type: "int", nullable: true),
                    StrPlanTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeasDatas", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbBlockVialFixture",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntAreaID = table.Column<int>(type: "int", nullable: true),
                    StrCharacteristicName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StrNameArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntMeaTypeID = table.Column<int>(type: "int", nullable: true),
                    StrMeaType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntProcessID = table.Column<int>(type: "int", nullable: true),
                    StrProcessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntCharacteristicType = table.Column<int>(type: "int", nullable: true),
                    StrCharacteristicUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BoolDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IntDefectRateLimit = table.Column<int>(type: "int", nullable: true),
                    IntEventEnable = table.Column<int>(type: "int", nullable: true),
                    IntEmailEventModel = table.Column<int>(type: "int", nullable: true),
                    IntDecimals = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbBlockVialFixture", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbEmailServer",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrEmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrSMTPHost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IntSMTPPort = table.Column<int>(type: "int", nullable: true),
                    BoolEnabledSSL = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEmailServer", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbEventLog",
                columns: table => new
                {
                    IntEventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DtEventTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StrEventCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    StrEvent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StrStation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEventLog", x => x.IntEventID);
                });

            migrationBuilder.CreateTable(
                name: "tbEventRoles",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrRoleID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IntAreaID = table.Column<int>(type: "int", nullable: true),
                    StrNameArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntProcessID = table.Column<int>(type: "int", nullable: true),
                    StrProcessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntCharacteristicID = table.Column<int>(type: "int", nullable: true),
                    StrCharacteristicName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IntDefectRateLimit = table.Column<int>(type: "int", nullable: true),
                    IntEventEnable = table.Column<int>(type: "int", nullable: true),
                    IntMeaTypeID = table.Column<int>(type: "int", nullable: true),
                    IntEmailEventModel = table.Column<int>(type: "int", nullable: true),
                    StrMeaType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrCharacteristicUnit = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEventRoles", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbInspectionPlanType",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrPlanTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbInspectionPlanType", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbJobData",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrJobCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrPOCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrNameArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrNameProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntInspPlanID = table.Column<int>(type: "int", nullable: true),
                    StrInspPlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrModelInternal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntJobQty = table.Column<int>(type: "int", nullable: true),
                    IntOutputQty = table.Column<int>(type: "int", nullable: true),
                    IntProductID = table.Column<int>(type: "int", nullable: true),
                    IntAreaID = table.Column<int>(type: "int", nullable: true),
                    DtCreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BoolDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IntJobDecisionID = table.Column<int>(type: "int", nullable: true),
                    StrDecision = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IntColorCode = table.Column<int>(type: "int", nullable: true),
                    StrSOCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntVialFixture = table.Column<int>(type: "int", nullable: true),
                    StrVialFixture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbJobData", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbJobDecision",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrDecision = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IntColorCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbJobDecision", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbMeasureType",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrMeaType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMeasureType", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbProcess",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrProcessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntAreaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProcess", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbProcessLine",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrProcessLineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrProcessLineCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntProcessID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProcessLine", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbProductionArea",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrNameArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProductionArea", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbProductionData",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IntProductID = table.Column<int>(type: "int", nullable: true),
                    IntJobID = table.Column<int>(type: "int", nullable: true),
                    IntLineID = table.Column<int>(type: "int", nullable: true),
                    IntProcessID = table.Column<int>(type: "int", nullable: true),
                    IntAreaID = table.Column<int>(type: "int", nullable: true),
                    IntJobQty = table.Column<int>(type: "int", nullable: true),
                    IntOutputQty = table.Column<int>(type: "int", nullable: true),
                    IntInspPlanID = table.Column<int>(type: "int", nullable: true),
                    StrNotes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrInspPlanName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrModelInternal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrNameArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrProcessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrProcessLineCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrProcessLineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrNameProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrJobCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrPOCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrSOCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrCustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtEndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtCreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BoolDeleted = table.Column<bool>(type: "bit", nullable: true),
                    IntUserID = table.Column<int>(type: "int", nullable: true),
                    StrFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtProductionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IntProductionQty = table.Column<int>(type: "int", nullable: true),
                    StrLotInform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrMaterialInform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IntCNCLatheMachine = table.Column<int>(type: "int", nullable: true),
                    StrCNCLatheMachine = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntVialFixture = table.Column<int>(type: "int", nullable: true),
                    StrVialFixture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProductionData", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbProductName",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrNameProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrModelInternal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntAreaID = table.Column<int>(type: "int", nullable: true),
                    IntInspPlanID = table.Column<int>(type: "int", nullable: true),
                    BoolDeleted = table.Column<bool>(type: "bit", nullable: false),
                    StrCustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrNotes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IntVialFixture = table.Column<int>(type: "int", nullable: true),
                    StrVialFixture = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProductName", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbShift",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrNameShift = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DtStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    DtEndTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbShift", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbTVDisplay",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrTVName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrProductionID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrCharacteristicID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrPlanTypeID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrMoldID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StrCavityID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTVDisplay", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "tbWebSession",
                columns: table => new
                {
                    IntID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrSessionID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StrIpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DtStartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DtEndTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbWebSession", x => x.IntID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false),
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
                    UserId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
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
                    UserId = table.Column<int>(type: "int", nullable: false),
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
        }

        /// <inheritdoc />
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
                name: "MeasDatas");

            migrationBuilder.DropTable(
                name: "tbBlockVialFixture");

            migrationBuilder.DropTable(
                name: "tbEmailServer");

            migrationBuilder.DropTable(
                name: "tbEventLog");

            migrationBuilder.DropTable(
                name: "tbEventRoles");

            migrationBuilder.DropTable(
                name: "tbInspectionPlanType");

            migrationBuilder.DropTable(
                name: "tbJobData");

            migrationBuilder.DropTable(
                name: "tbJobDecision");

            migrationBuilder.DropTable(
                name: "tbMeasureType");

            migrationBuilder.DropTable(
                name: "tbProcess");

            migrationBuilder.DropTable(
                name: "tbProcessLine");

            migrationBuilder.DropTable(
                name: "tbProductionArea");

            migrationBuilder.DropTable(
                name: "tbProductionData");

            migrationBuilder.DropTable(
                name: "tbProductName");

            migrationBuilder.DropTable(
                name: "tbShift");

            migrationBuilder.DropTable(
                name: "tbTVDisplay");

            migrationBuilder.DropTable(
                name: "tbWebSession");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropSequence(
                name: "KnowledgeBaseSepence");
        }
    }
}
