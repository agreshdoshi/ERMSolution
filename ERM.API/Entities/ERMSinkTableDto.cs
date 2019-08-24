namespace ERM.API.Entities
{
    public class ERMSinkTableDto
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string MeterCode { get; set; }
        public string DataType { get; set; }
        public double? MinimumValue { get; set; }
        public double? MaximumValue { get; set; }
        public double? MedianValue { get; set; }
    }
}