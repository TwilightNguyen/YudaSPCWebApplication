using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YudaSPCWebApplication.BackendServer.Data.Migrations
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
                name: "tbCharacteristic",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strCharacteristicName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    intMeaTypeID = table.Column<int>(type: "int", nullable: true),
                    intProcessID = table.Column<int>(type: "int", nullable: true),
                    intCharacteristicType = table.Column<int>(type: "int", nullable: true),
                    strCharacteristicUnit = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    boolDeleted = table.Column<bool>(type: "bit", nullable: true),
                    intDefectRateLimit = table.Column<int>(type: "int", nullable: true),
                    intEventEnable = table.Column<int>(type: "int", nullable: true),
                    intEmailEventModel = table.Column<int>(type: "int", nullable: true),
                    intDecimals = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbCharacteristic", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbEmailServer",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strEmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strDisplayName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strSMTPHost = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    intSMTPPort = table.Column<int>(type: "int", nullable: true),
                    boolEnabledSSL = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEmailServer", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbEventLog",
                columns: table => new
                {
                    intEventID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dtEventTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    strEventCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    strEvent = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    strStation = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEventLog", x => x.intEventID);
                });

            migrationBuilder.CreateTable(
                name: "tbEventRoles",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCharacteristicID = table.Column<int>(type: "int", nullable: true),
                    strRoleID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbEventRoles", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbInspectionPlanType",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strPlanTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbInspectionPlanType", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbJobData",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intAreaID = table.Column<int>(type: "int", nullable: true),
                    intProductID = table.Column<int>(type: "int", nullable: true),
                    strJobCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strPOCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strSOCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    intJobQty = table.Column<int>(type: "int", nullable: true),
                    intOutputQty = table.Column<int>(type: "int", nullable: true),
                    dtCreateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    intJobDecisionID = table.Column<int>(type: "int", nullable: true),
                    intUserID = table.Column<int>(type: "int", nullable: true),
                    boolDeleted = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbJobData", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbJobDecision",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strDecision = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    intColorCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbJobDecision", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbMeasData3_01",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intCharacteristicID = table.Column<int>(type: "int", nullable: true),
                    varCharacteristicValue = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    varCharacteristicRange = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: true),
                    dtTimeStamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    intDataCollection = table.Column<int>(type: "int", nullable: true),
                    dtTimeMeasure = table.Column<DateTime>(type: "datetime", nullable: true),
                    intJobID = table.Column<int>(type: "int", nullable: true),
                    intProductionID = table.Column<int>(type: "int", nullable: true),
                    intLineID = table.Column<int>(type: "int", nullable: true),
                    intUserID = table.Column<int>(type: "int", nullable: true),
                    intOutputQty = table.Column<int>(type: "int", nullable: true),
                    intSampleIndex = table.Column<int>(type: "int", nullable: true),
                    intOKNG = table.Column<int>(type: "int", nullable: true),
                    intEmailSent = table.Column<int>(type: "int", nullable: true),
                    intPlanTypeID = table.Column<int>(type: "int", nullable: true),
                    strOutputNotes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    intSampleQty = table.Column<int>(type: "int", nullable: true),
                    intMoldID = table.Column<int>(type: "int", nullable: true),
                    intCavityID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMeasData3_01", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbMeasureType",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strMeaType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbMeasureType", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbProcess",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strProcessName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    intAreaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProcess", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbProcessLine",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strProcessLineName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strProcessLineCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    intAreaID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProcessLine", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbProductionArea",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strNameArea = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProductionArea", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbProductionData",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    intLineID = table.Column<int>(type: "int", nullable: true),
                    dtStartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    dtEndTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    intJobID = table.Column<int>(type: "int", nullable: true),
                    boolDeleted = table.Column<bool>(type: "bit", nullable: true),
                    intUserID = table.Column<int>(type: "int", nullable: true),
                    strNotes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dtProductionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    intProductionQty = table.Column<int>(type: "int", nullable: true),
                    strLotInform = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strMaterialInform = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    intCNCLatheMachine = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProductionData", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbProductName",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strNameProduct = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    intAreaID = table.Column<int>(type: "int", nullable: true),
                    intInspPlanID = table.Column<int>(type: "int", nullable: true),
                    boolDeleted = table.Column<bool>(type: "bit", nullable: false),
                    strModelInternal = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strCustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strNotes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    intVialFixture = table.Column<int>(type: "int", nullable: true),
                    strDescription = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    intMoldQty = table.Column<int>(type: "int", nullable: true),
                    intCavityQty = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbProductName", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbRole",
                columns: table => new
                {
                    intRoleID = table.Column<int>(type: "int", nullable: false),
                    strRoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strDescription = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    intLevel = table.Column<int>(type: "int", nullable: false),
                    intRoleUser = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbRole", x => x.intRoleID);
                });

            migrationBuilder.CreateTable(
                name: "tbShift",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StrNameShift = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dtStartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    dtEndTime = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbShift", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbTVDisplay",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strTVName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strProductionID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strCharacteristicID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strPlanTypeID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strMoldID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strCavityID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbTVDisplay", x => x.intID);
                });

            migrationBuilder.CreateTable(
                name: "tbUser",
                columns: table => new
                {
                    intUserID = table.Column<int>(type: "int", nullable: false),
                    strRoleID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    strPassword = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    strEmailAddress = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strDepartment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strStaffID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    dtLastActivityTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    intEnable = table.Column<int>(type: "int", nullable: true),
                    strSelectedAreaID = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbUser", x => x.intUserID);
                });

            migrationBuilder.CreateTable(
                name: "tbWebSession",
                columns: table => new
                {
                    intID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    strSessionID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    strIpAddress = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    dtStartTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    dtEndTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbWebSession", x => x.intID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbCharacteristic");

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
                name: "tbMeasData3_01");

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
                name: "tbRole");

            migrationBuilder.DropTable(
                name: "tbShift");

            migrationBuilder.DropTable(
                name: "tbTVDisplay");

            migrationBuilder.DropTable(
                name: "tbUser");

            migrationBuilder.DropTable(
                name: "tbWebSession");

            migrationBuilder.DropSequence(
                name: "KnowledgeBaseSepence");
        }
    }
}
