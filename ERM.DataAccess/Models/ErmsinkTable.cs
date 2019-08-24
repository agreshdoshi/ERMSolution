using System;
using System.Collections.Generic;

namespace ERM.DataAccess.Models
{
    public class ErmsinkTable
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string MeterCode { get; set; }
        public string DataType { get; set; }
        public double? MinimumValue { get; set; }
        public double? MaximumValue { get; set; }
        public double? MedianValue { get; set; }
    }
}
