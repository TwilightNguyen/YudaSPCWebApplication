namespace YudaSPCWebApplication.ViewModels.System.InspectionPlanData
{
    public class InspectionPlanDataVm
    {
        public int Id { get; set; }

        public int? InspPlanSubId { get; set; }

        public int? CharacteristicId { get; set; }

        public double? LSL { get; set; }

        public double? USL { get; set; }

        public double? LCL { get; set; }

        public double? UCL { get; set; }

        public bool? SPCChart { get; set; }

        public bool? DataEntry { get; set; }

        public int? PlanState { get; set; }

        public double? CpkMax { get; set; }

        public double? CpkMin { get; set; }

        public bool? SpkControl { get; set; }

        public string? SampleSize { get; set; }

        public double? PercentControlLimit { get; set; }


        // Additional Properties
        public string? CharacteristicName { get; set; }
    }
}
