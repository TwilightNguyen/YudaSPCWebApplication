using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using YudaSPCWebApplication.BackendServer.Data;
using YudaSPCWebApplication.BackendServer.Data.Entities;
using YudaSPCWebApplication.ViewModels;
using YudaSPCWebApplication.ViewModels.System;

namespace YudaSPCWebApplication.BackendServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Policy = "Bearer")]
    public class InspectionPlanDatasController(ApplicationDbContext context): ControllerBase
    {
        // Controller methods would go here
        private readonly ApplicationDbContext _context = context;

        /// <summary>
        ///  URL: /api/inspectionplandatas/
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateInspectionPlanData([FromBody] InspectionPlanDataCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var inspPlanSub = _context.InspectionPlanSubs.FirstOrDefault(p => p.IntID == request.InspPlanSubId && p.BoolDeleted == false);

            if (inspPlanSub == null)
            {
                return BadRequest("Invalid Inspection Plan Sub.");
            }

            var characteristic = _context.Characteristics.FirstOrDefault(p => p.IntID == request.CharacteristicId);

            if (characteristic == null)
            {
                return BadRequest("Invalid Characteristic.");
            }

            var inspectionPlanData = _context.InspectionPlanDatas.FirstOrDefault(x =>
                x.IntInspPlanSubID == request.InspPlanSubId &&
                x.IntCharacteristicID == request.CharacteristicId &&
                x.BoolDeleted == false);

            bool HasSpecLimits = request.USL != null && request.LSL != null && request.PercentControlLimit != null;
            double SpecLimitRange = (double)((request.USL != null && request.LSL != null) ? (request.USL - request.LSL) : 0);
            
            bool IsCreate = inspectionPlanData == null;

            if (inspectionPlanData != null)
            {
                inspectionPlanData.FtLSL = request.LSL;
                inspectionPlanData.FtUSL = request.USL;
                inspectionPlanData.FtLCL = HasSpecLimits ? request.LSL + (request.PercentControlLimit / 100.0) * SpecLimitRange : (double?)null;
                inspectionPlanData.FtUCL = HasSpecLimits ? request.USL - (request.PercentControlLimit / 100.0) * SpecLimitRange : (double?)null;
                inspectionPlanData.BoolSPCChart = request.SPCChart;
                inspectionPlanData.BoolDataEntry = request.DataEntry;
                inspectionPlanData.IntPlanState = request.PlanState;
                inspectionPlanData.FtCpkMax = request.CpkMax;
                inspectionPlanData.FtCpkMin = request.CpkMin;
                inspectionPlanData.BoolSpkControl = request.SpkControl;
                inspectionPlanData.StrSampleSize = request.SampleSize;
                inspectionPlanData.FtPercentControlLimit = request.PercentControlLimit;

                _context.InspectionPlanDatas.Update(inspectionPlanData);
            }
            else
            {
                inspectionPlanData = new InspectionPlanData
                {
                    IntInspPlanSubID = request.InspPlanSubId,
                    IntCharacteristicID = request.CharacteristicId,
                    FtLSL = request.LSL,
                    FtUSL = request.USL,
                    FtLCL = HasSpecLimits ? request.LSL + (request.PercentControlLimit / 100.0) * SpecLimitRange : (double?)null,
                    FtUCL = HasSpecLimits ? request.USL - (request.PercentControlLimit / 100.0) * SpecLimitRange : (double?)null,
                    BoolSPCChart = request.SPCChart,
                    BoolDataEntry = request.DataEntry,
                    IntPlanState = request.PlanState,
                    FtCpkMax = request.CpkMax,
                    FtCpkMin = request.CpkMin,
                    BoolSpkControl = request.SpkControl,
                    StrSampleSize = request.SampleSize,
                    FtPercentControlLimit = request.PercentControlLimit,
                    BoolDeleted = false,
                };

                _context.InspectionPlanDatas.Add(inspectionPlanData);
            }

            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return CreatedAtAction(
                    nameof(GetById),
                    new { Id = inspectionPlanData.IntID, IsCreate },
                    new InspectionPlanDataVm
                    {
                        Id = inspectionPlanData.IntID,
                        InspPlanSubId = inspectionPlanData.IntInspPlanSubID,
                        CharacteristicId = inspectionPlanData.IntCharacteristicID,
                        LSL = inspectionPlanData.FtLSL,
                        USL = inspectionPlanData.FtUSL,
                        LCL = inspectionPlanData.FtLCL,
                        UCL = inspectionPlanData.FtUCL,
                        SPCChart = inspectionPlanData.BoolSPCChart,
                        DataEntry = inspectionPlanData.BoolDataEntry,
                        PlanState = inspectionPlanData.IntPlanState,
                        CpkMax = inspectionPlanData.FtCpkMax,
                        CpkMin = inspectionPlanData.FtCpkMin,
                        SpkControl = inspectionPlanData.BoolSpkControl,
                        SampleSize = inspectionPlanData.StrSampleSize,
                        PercentControlLimit = inspectionPlanData.FtPercentControlLimit,
                        CharacteristicName = characteristic.StrCharacteristicName,
                    }
                );
            }
            else
            {
                return BadRequest("Failed to create Inspection Plan Sub.");
            }
        }

        /// <summary>
        /// Url: /api/inspectionplandatas/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var inspectionPlanData = _context.InspectionPlanDatas.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlanData == null)
            {
                return NotFound("Inspection Plan Data not found.");
            }

            var inspectionPlanDataVm = new InspectionPlanDataVm
            {
                Id = inspectionPlanData.IntID,
                InspPlanSubId = inspectionPlanData.IntInspPlanSubID,
                CharacteristicId = inspectionPlanData.IntCharacteristicID,
                LSL = inspectionPlanData.FtLSL,
                USL = inspectionPlanData.FtUSL,
                LCL = inspectionPlanData.FtLCL,
                UCL = inspectionPlanData.FtUCL,
                SPCChart = inspectionPlanData.BoolSPCChart,
                DataEntry = inspectionPlanData.BoolDataEntry,
                PlanState = inspectionPlanData.IntPlanState,
                CpkMax = inspectionPlanData.FtCpkMax,
                CpkMin = inspectionPlanData.FtCpkMin,
                SpkControl = inspectionPlanData.BoolSpkControl,
                SampleSize = inspectionPlanData.StrSampleSize,
                PercentControlLimit = inspectionPlanData.FtPercentControlLimit,
            };

            return Ok(inspectionPlanDataVm);
        }


        /// <summary>
        /// Url: /api/inspectionplandatas/GetByInsPlanSubId/{Id}
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet("/GetByInsPlanSubIdAndPlanState/{InsPlanSubId:int,PlanState:int}")]
        public async Task<IActionResult> GetByInsPlanSubIdAndPlanState(int InsPlanSubId, int? PlanState)
        {
            var inspectionPlanSubs = _context.InspectionPlanSubs.Where(r => r.IntID == InsPlanSubId && r.BoolDeleted == false);

            if (inspectionPlanSubs == null || inspectionPlanSubs?.Count() == 0)
            {
                return NotFound("Inspection Plan Sub not found.");
            }

            var InspectionPlanDataVms = _context.InspectionPlanDatas
                .Where(r => 
                    r.IntInspPlanSubID == InsPlanSubId && 
                    (r.IntPlanState == PlanState || PlanState == -1 || PlanState == null) &&
                    r.BoolDeleted == false
                )
                .Select(inspectionPlanData => new InspectionPlanDataVm
                {
                    Id = inspectionPlanData.IntID,
                    InspPlanSubId = inspectionPlanData.IntInspPlanSubID,
                    CharacteristicId = inspectionPlanData.IntCharacteristicID,
                    LSL = inspectionPlanData.FtLSL,
                    USL = inspectionPlanData.FtUSL,
                    LCL = inspectionPlanData.FtLCL,
                    UCL = inspectionPlanData.FtUCL,
                    SPCChart = inspectionPlanData.BoolSPCChart,
                    DataEntry = inspectionPlanData.BoolDataEntry,
                    PlanState = inspectionPlanData.IntPlanState,
                    CpkMax = inspectionPlanData.FtCpkMax,
                    CpkMin = inspectionPlanData.FtCpkMin,
                    SpkControl = inspectionPlanData.BoolSpkControl,
                    SampleSize = inspectionPlanData.StrSampleSize,
                    PercentControlLimit = inspectionPlanData.FtPercentControlLimit,
                }).ToList();

            return Ok(InspectionPlanDataVms);
        }

        /// <summary>
        /// Url: /api/inspectionplandatas/{Id}
        /// </summary>
        /// <returns></returns>
        /// 

        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> DeleteInspectionPlanData(int Id)
        {
            var inspectionPlanData = _context.InspectionPlanDatas.FirstOrDefault(r => r.IntID == Id && r.BoolDeleted == false);

            if (inspectionPlanData == null)
            {
                return NotFound("Inspection Plan Data not found.");
            }

            inspectionPlanData.BoolDeleted = true;
            
            var result = await _context.SaveChangesAsync();
            
            if (result > 0)
            {
                var characteristic = _context.Characteristics.FirstOrDefault(p => p.IntID == inspectionPlanData.IntCharacteristicID);

                return Ok(new InspectionPlanDataVm
                {
                    Id = inspectionPlanData.IntID,
                    InspPlanSubId = inspectionPlanData.IntInspPlanSubID,
                    CharacteristicId = inspectionPlanData.IntCharacteristicID,
                    LSL = inspectionPlanData.FtLSL,
                    USL = inspectionPlanData.FtUSL,
                    LCL = inspectionPlanData.FtLCL,
                    UCL = inspectionPlanData.FtUCL,
                    SPCChart = inspectionPlanData.BoolSPCChart,
                    DataEntry = inspectionPlanData.BoolDataEntry,
                    PlanState = inspectionPlanData.IntPlanState,
                    CpkMax = inspectionPlanData.FtCpkMax,
                    CpkMin = inspectionPlanData.FtCpkMin,
                    SpkControl = inspectionPlanData.BoolSpkControl,
                    SampleSize = inspectionPlanData.StrSampleSize,
                    PercentControlLimit = inspectionPlanData.FtPercentControlLimit,

                    CharacteristicName = characteristic?.StrCharacteristicName,
                });
            }
            else
            {
                return BadRequest("Failed to delete Inspection Plan Data.");
            }
        }
    }
}
