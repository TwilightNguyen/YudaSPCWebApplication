namespace YudaSPCWebApplication.ViewModels.System.InspectionPlanData
{
    public class InspectionPlanDataCreateRequest
    {
        public required int InspPlanSubId { get; set; }

        public required int CharacteristicId { get; set; }

        public double? LSL { get; set; }

        public double? USL { get; set; }

        public required bool SPCChart { get; set; }

        public required bool DataEntry { get; set; }

        public required int PlanState { get; set; }

        public double CpkMax { get; set; }

        public double CpkMin { get; set; }

        public required bool SpkControl { get; set; }

        public required string SampleSize { get; set; }

        public double? PercentControlLimit { get; set; }
    }
}
