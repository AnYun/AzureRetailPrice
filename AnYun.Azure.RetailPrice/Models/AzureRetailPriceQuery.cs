using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnYun.Azure.RetailPrice.Models
{
    public class AzureRetailPriceQuery
    {
        public string ArmRegionName { get; set; }
        public string Location { get; set; }
        public string MeterId { get; set; }
        public string MeterName { get; set; }
        public string ProductId { get; set; }
        public string SkuId { get; set; }
        public string ProductName { get; set; }
        public string SkuName { get; set; }
        public string ServiceName { get; set; }
        public string ServiceId { get; set; }
        public string ServiceFamily { get; set; }
        public string Type { get; set; }
        public string ArmSkuName { get; set; }
    }
}
