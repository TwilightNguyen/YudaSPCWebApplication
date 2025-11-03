namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class CapabilityAnalysis
    {
        public int IntJobID { get; set; }
        public int IntPlanTypeID { get; set; }
        public int IntCharacteristicID { get; set; }
        public int? IntProductedQty { get; set; }
        public double? FtAverage { get; set; }
        public double? FtStDevWithin { get; set; }
        public double? FtStDevOverall { get; set; }
    }
}
